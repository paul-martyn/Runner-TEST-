using CodeBase.Boosts.Effects;
using CodeBase.Data.Static;
using CodeBase.Hero;
using Sirenix.OdinInspector;
using UnityEngine;

namespace CodeBase.Boosts
{
    public abstract class Boost : MonoBehaviour
    {
        [SerializeField, Required]
        protected TriggerObserver _triggerObserver;
        [SerializeField, Required]
        protected GameObject _model;
        [SerializeField, Required]
        protected ParticleSystem _pickUpFX;
        
        protected IBoostEffect Effect;

        public virtual void Construct(BoostsStaticData boostsStaticData)
        {
            
        }
        
        protected void OnEnable() => 
            _triggerObserver.TriggerEnter += OnTrigger;

        protected void OnDisable() => 
            _triggerObserver.TriggerEnter -= OnTrigger;

        private void Disable()
        {
            _triggerObserver.enabled = false;
            _model.SetActive(false);
        }

        private void OnTrigger(Collider other)
        {
            if (other.GetComponent<HeroHealth>())
            {
                Disable();
                Effect.Apply(other.gameObject);
                _pickUpFX.Play();
            }
        }
    }
}