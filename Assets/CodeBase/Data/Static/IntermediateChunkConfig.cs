using System;
using CodeBase.Level.Road.Chunks;
using Sirenix.OdinInspector;
using UnityEngine;

namespace CodeBase.Data.Static
{
    [Serializable]
    public class IntermediateChunkConfig
    {
        [SerializeField, AssetsOnly, Required]
        private IntermediateChunk _prefab;
        
        [SerializeField, Range(0f, 5f)]
        private float _rollChance;

        public IntermediateChunk Prefab => _prefab;
        public float RollChance => _rollChance;
    }
}
