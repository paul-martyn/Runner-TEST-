using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

namespace CodeBase.Hero.HUD
{
    public class HealthBar : MonoBehaviour
    {
        [SerializeField, AssetsOnly] private HealthBarUnit _unitPrefab;

        private List<HealthBarUnit> _healthBarUnits;
        private HeroHealth _heroHealth;

        public void Initialize(HeroHealth heroHealth)
        {
            _heroHealth = heroHealth;
            foreach (Transform child in transform)
                Destroy(child.gameObject);

            _healthBarUnits = new List<HealthBarUnit>(_heroHealth.MaxHealth);
            
            for (int i = 0; i < _heroHealth.MaxHealth; i++) 
                _healthBarUnits.Add(Instantiate(_unitPrefab, transform));

            _heroHealth.HealthChanged += Refresh;
        }

        private void Refresh(int currentHealth)
        {
            for (int i = 0; i < _healthBarUnits.Count; i++) 
                _healthBarUnits[i].SetState(i < currentHealth);
        }
    }
}