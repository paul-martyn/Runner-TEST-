using CodeBase.Level.Road.Chunks;

namespace CodeBase.Level.Road
{
    public interface IChunkPlacer
    {
        public void Place(ref RoadChunk roadChunk);
    }
}