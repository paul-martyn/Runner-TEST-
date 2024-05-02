using CodeBase.Level.Road.Connection;
using UnityEngine;

namespace CodeBase.Level.Road.Chunks
{
    public class IntermediateChunk : RoadChunk
    {
        public InputPoint Input => _input;
        [Space(10f)]
        [SerializeField] protected InputPoint _input;
        public OutputPoint Output => _output;
        [SerializeField] protected OutputPoint _output;
    }
}