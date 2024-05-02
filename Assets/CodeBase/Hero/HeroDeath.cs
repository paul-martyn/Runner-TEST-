using System;
using CodeBase.EventBus;
using CodeBase.EventBus.Signals.LevelSignals;
using CodeBase.Services;
using UnityEngine;

namespace CodeBase.Hero
{
    public class HeroDeath : MonoBehaviour
    {
        [SerializeField] private HeroHealth _heroHealth;
        
        private bool _isDead;

        private void OnEnable() => 
            _heroHealth.HealthChanged += OnHealthChanged;

        private void OnDisable() => 
            _heroHealth.HealthChanged -= OnHealthChanged;

        private void OnHealthChanged(int value)
        {
            if (value < 0)
                throw new InvalidOperationException();
                
            if (value == 0 && !_isDead) 
                Die();
        }

        private void Die()
        {
            _isDead = true;
            AllServices.Container.Single<IEventBus>().Invoke(new LevelFailSignal());
        }

        public void Respawn() => 
            _isDead = false;
    }
}