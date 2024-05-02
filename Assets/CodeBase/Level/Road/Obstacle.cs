using UnityEngine;

namespace CodeBase.Level.Road
{
    public class Obstacle : MonoBehaviour
    {
        [SerializeField] private Collider _collider;
        [SerializeField] private int _damage = 1;
        [SerializeField] private bool _withImpact = false;

        public int Damage => _damage;
        public bool WithImpact => _withImpact;

        public void Disable() => 
            _collider.enabled = false;
    }
}