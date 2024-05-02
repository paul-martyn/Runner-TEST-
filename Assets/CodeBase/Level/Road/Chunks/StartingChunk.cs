using CodeBase.Level.Road.Connection;
using UnityEngine;

namespace CodeBase.Level.Road.Chunks
{
    public class StartingChunk : RoadChunk
    {
        public OutputPoint Output => _output;
        [SerializeField] protected OutputPoint _output;
    }
}