using System;
using System.Collections.Generic;
using System.Linq;
using CodeBase.Data.Static;
using CodeBase.EventBus;
using CodeBase.Infrastructure.Factory;
using CodeBase.Level.Road.Chunks;
using CodeBase.Services.Random;
using UnityEngine;

namespace CodeBase.Level.Road
{
    public class RandomRoadGenerator : IRoadGenerator
    {
        private readonly IChunkPlacer _chunkPlacer;
        private readonly List<RoadChunk> _spawnedChunks = new List<RoadChunk>();
        
        private Directions _previousDirection;
        private Directions _resultDirection;
        
        private readonly LevelStaticData _levelStaticData;
        private readonly IGameFactory _gameFactory;
        private readonly IEventBus _eventBus;

        public RandomRoadGenerator(LevelStaticData levelStaticData, IGameFactory gameFactory, IEventBus eventBus)
        {
            _chunkPlacer = new ChunkPlacer();
            _levelStaticData = levelStaticData;
            _gameFactory = gameFactory;
            _eventBus = eventBus;
        }

        public void Generate()
        {
            SpawnChunk(_levelStaticData.StartingChunk);
            for (int i = 0; i < _levelStaticData.RoadChunksCount; i++)
            {
                while (!TrySpawn(GetRandomChunk())) 
                    TrySpawn(GetRandomChunk());
            }      
            SpawnChunk(_levelStaticData.FinalChunk);
        }

        public Vector3 GetStartingPosition()
        {
            if (_spawnedChunks != null)
                if (_spawnedChunks[0] is StartingChunk) 
                    return _spawnedChunks[0].Center.position;
            
            throw new NullReferenceException();
        }

        public List<RoadChunk> GetRoad() => 
            _spawnedChunks;

        private IntermediateChunk GetRandomChunk()
        {
            IntermediateChunk rolledChunk = null;
            List<float> rollChances = new List<float>(_levelStaticData.IntermediateChunks.Count);
            rollChances.AddRange(_levelStaticData.IntermediateChunks.Select(chunk => chunk.RollChance));

            float rolledChance = RandomService.Range(0f, rollChances.Sum());
            float checkedRatesSum  = 0f;
            
            for (int i = 0; i < rollChances.Count; i++)
            {
                checkedRatesSum += rollChances[i];
                if (rolledChance <= checkedRatesSum)
                {
                    rolledChunk = _levelStaticData.IntermediateChunks[i].Prefab;
                    break;
                }
            }

            if (rolledChunk == null)
                throw new NullReferenceException();

            return rolledChunk;
        }
        
        private bool TrySpawn(RoadChunk roadChunk)
        {
            if (!CheckSpawnAvailability(roadChunk))
                return false;
            SpawnChunk(roadChunk);        
            return true;
        }

        private bool CheckSpawnAvailability(RoadChunk chunk)
        {
            if (chunk.OutputDirections == _previousDirection && (chunk.OutputDirections == Directions.ToLeft || chunk.OutputDirections == Directions.ToRight))
                return false;
            if (_resultDirection == Directions.ToLeft && chunk.OutputDirections == Directions.ToLeft)
                return false;
            if (_resultDirection == Directions.ToRight && chunk.OutputDirections == Directions.ToRight)
                return false;
            if (chunk is ObstacleChunk)
            {
                if (_spawnedChunks.Count <= _levelStaticData.MinChunksBetweenObstacles)
                    return false;
                
                int lastChunkID = _spawnedChunks.Count - 1;
                for (int i = lastChunkID; i > (lastChunkID - _levelStaticData.MinChunksBetweenObstacles); i--)
                    if (_spawnedChunks[i] is ObstacleChunk)
                        return false;
            }
            
            return true;
        }

        private void SpawnChunk(RoadChunk roadChunk)
        {
            RoadChunk chunk = _gameFactory.CreateChunk(roadChunk);
            _chunkPlacer.Place(ref chunk);
            _previousDirection = roadChunk.OutputDirections;
            _resultDirection += (int) roadChunk.OutputDirections;
            _spawnedChunks.Add(chunk);
        }
    }
}
