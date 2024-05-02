using System.Collections.Generic;
using CodeBase.Level.Road.Chunks;
using UnityEngine;

namespace CodeBase.Level.Road
{
    public interface IRoadGenerator
    {
        public void Generate();
        public Vector3 GetStartingPosition();
        List<RoadChunk> GetRoad();
    }
}