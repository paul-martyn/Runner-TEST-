using System;
using CodeBase.Boosts;
using Sirenix.OdinInspector;
using UnityEngine;

namespace CodeBase.Data.Static
{
    [Serializable]
    public class BoostConfig
    {
        [SerializeField, AssetsOnly, Required]
        private Boost _prefab;
        
        [SerializeField, Range(0f, 5f)]
        private float _rollChance;
        
        public Boost Prefab => _prefab;
        public float RollChance => _rollChance;
    }
}