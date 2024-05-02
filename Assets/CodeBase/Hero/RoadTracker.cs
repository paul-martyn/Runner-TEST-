using System.Collections.Generic;
using CodeBase.Data;
using CodeBase.Level.Road.Chunks;

namespace CodeBase.Hero
{
    public class RoadTracker
    {
        private List<RoadChunk> _roadChunks;
        private LevelProgress _levelProgress;
        
        public RoadTracker(List<RoadChunk> roadChunks, LevelProgress levelProgress)
        {
            _roadChunks = roadChunks;
            _levelProgress = levelProgress;
        }

        public void RoadChunkPassed()
        {
            if (_roadChunks[0] is ObstacleChunk obstacleChunk)
                _levelProgress.IncreaseByType(obstacleChunk.ObstacleType);
            _roadChunks.Remove(_roadChunks[0]);
        }

        public int GeSkippedChunkCount()
        {
            int skippedChunksCount = 0;
            foreach (RoadChunk roadChunk in _roadChunks)
                if (roadChunk is ObstacleChunk)
                    skippedChunksCount += 1;
                else
                    return skippedChunksCount;
            return skippedChunksCount;
        }
        
    }
}