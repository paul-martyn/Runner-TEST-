using System;
using UnityEngine;

namespace CodeBase.Boosts
{
    public class TriggerObserver : MonoBehaviour
    {
        public event Action<Collider> TriggerEnter; 
        private void OnTriggerEnter(Collider other) => 
            TriggerEnter?.Invoke(other);
    }
}