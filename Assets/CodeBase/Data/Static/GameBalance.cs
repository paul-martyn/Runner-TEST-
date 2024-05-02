using Sirenix.OdinInspector;
using UnityEngine;

namespace CodeBase.Data.Static
{
    [CreateAssetMenu(fileName = "GameBalance", menuName = "StaticData/Game Balance")]
    public class GameBalance : ScriptableObject
    {
        [SerializeField]
        private PlayerStats _playerStats;

        public PlayerStats PlayerStats => _playerStats;

        [ShowInInspector, Space(20f)]
        public const float ObstacleHeightUnit = 1.5f;
        
        [ShowInInspector]
        public const float ObstacleLengthUnit = 5f;
    }
}
