using System;
using UnityEngine;

namespace CodeBase.Data.Static
{
    [Serializable]
    public class PlayerStats
    {
        [SerializeField, Range(0, 5)]
        private int _playerHealth = 3;
        
        [SerializeField, Range(5f, 20f)]
        private float _playerSpeed = 10f;

        public int PlayerHealth => _playerHealth;
        public float PlayerSpeed => _playerSpeed;
    }
}