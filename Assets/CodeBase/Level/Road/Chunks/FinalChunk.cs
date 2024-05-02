using CodeBase.Level.Road.Connection;
using UnityEngine;

namespace CodeBase.Level.Road.Chunks
{
    public class FinalChunk : RoadChunk
    {
        public InputPoint Input => _input;
        [SerializeField] protected InputPoint _input;
    }
}