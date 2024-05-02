using CodeBase.Level.Road.Chunks;
using CodeBase.Level.Road.Connection;
using UnityEngine;

namespace CodeBase.Level.Road
{
    public class ChunkPlacer : IChunkPlacer
    {
        private OutputPoint _lastOutputPoint;

        public void Place(ref RoadChunk roadChunk)
        {
            switch (roadChunk)
            {
                case StartingChunk startingChunk:
                    PlaceChunk(startingChunk);
                    break;
                case IntermediateChunk intermediateChunk:
                    PlaceChunk(intermediateChunk);
                    break;
                case FinalChunk finalChunk:
                    PlaceChunk(finalChunk);
                    break;
            }
        }
        
        private void PlaceChunk(StartingChunk startingChunk)
        {
            startingChunk.transform.position = Vector3.zero;
            UpdateOutput(startingChunk.Output);
        }

        private void PlaceChunk(IntermediateChunk intermediateChunk)
        {
            PlaceChunk(intermediateChunk, intermediateChunk.Input);
            UpdateOutput(intermediateChunk.Output);
        }

        private void PlaceChunk(FinalChunk finalChunk) => 
            PlaceChunk(finalChunk, finalChunk.Input);

        private void PlaceChunk(RoadChunk roadChunk, InputPoint input)
        {
            Transform chunkTransform = roadChunk.transform;
            chunkTransform.rotation = _lastOutputPoint.Rotation;
            Vector3 unroundedPosition = _lastOutputPoint.transform.position - input.LocalPosition;
            Vector3Int roundedPosition = Vector3Int.RoundToInt(unroundedPosition);
            chunkTransform.transform.position = roundedPosition;
        }

        private void UpdateOutput(OutputPoint outputPoint) => 
            _lastOutputPoint = outputPoint;
    }
}