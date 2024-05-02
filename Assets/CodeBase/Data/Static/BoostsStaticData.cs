using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

namespace CodeBase.Data.Static
{
    [CreateAssetMenu(fileName = "BoostsStaticData", menuName = "StaticData/Boosts static data")]
    public class BoostsStaticData : ScriptableObject
    {
        [SerializeField, Range(0f, 100f)]
        private float _spawnСhance = 10f;
        
        [SerializeField, AssetsOnly, Space(10f), TableList(AlwaysExpanded = true, DrawScrollView = false)]
        private List<BoostConfig> _boosts;
        
        [SerializeField, Range(1, 5), Space(20f)]
        private int _healthBoostValue = 1;

        [SerializeField, Range(0f, 10f), Space(10f)]
        private float _speedBoostValue = 3f;
        [SerializeField, Range(0f, 10f)]
        private float _speedBoosDuration = 3f;
        
        [SerializeField, Range(0f, 10f), Space(10f)]
        private float _immunityBoostDuration = 3f;

        public float SpawnСhance => _spawnСhance;
        public IReadOnlyList<BoostConfig> Boosts => _boosts;
        public int HealthBoostValue => _healthBoostValue;
        public float SpeedBoostValue => _speedBoostValue;
        public float SpeedBoosDuration => _speedBoosDuration;
        public float ImmunityBoostDuration => _immunityBoostDuration;
    }
}