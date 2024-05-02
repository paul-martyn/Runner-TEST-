using System;
using CodeBase.Level.Road;
using UnityEngine;

namespace CodeBase.Hero
{
    public class HeroHealth: MonoBehaviour
    {
        [SerializeField]
        private ObstacleObserver _obstacleObserver;
        [SerializeField]
        private ParticleSystem _damageFX;

        public event Action<int> HealthChanged;
        
        private int _health;
        private int _maxHealth;
        
        public int MaxHealth => _maxHealth;

        public void Construct(int maxHealth)
        {
            if (maxHealth <= 0)
                throw new InvalidOperationException();
            
            _health = maxHealth;
            _maxHealth = maxHealth;
        }

        private void OnEnable() => 
            _obstacleObserver.CollideWithObstacle += TakeDamage;

        private void OnDisable() => 
            _obstacleObserver.CollideWithObstacle -= TakeDamage;

        private void TakeDamage(Obstacle obstacle)
        {
            if (_health <= 0)
                return;

            _health = Mathf.Clamp(_health - obstacle.Damage, 0, _maxHealth);
            _damageFX.Play();
            obstacle.Disable();
            HealthChanged?.Invoke(_health);
        }
        
        public void ApplyHealth(int value)
        {
            _health = Mathf.Clamp(_health + value, 0, _maxHealth);
            HealthChanged?.Invoke(_health);
        }
        
    }
}