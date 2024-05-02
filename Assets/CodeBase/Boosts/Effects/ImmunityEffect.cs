using CodeBase.Hero;
using UnityEngine;

namespace CodeBase.Boosts.Effects
{
    public class ImmunityEffect : IBoostEffect
    {
        private readonly float _duration;

        public ImmunityEffect(float duration)
        {
            _duration = duration;
        }

        public void Apply(GameObject hero) => 
            hero.GetComponent<HeroImmunity>().Activate(_duration);
    }
}