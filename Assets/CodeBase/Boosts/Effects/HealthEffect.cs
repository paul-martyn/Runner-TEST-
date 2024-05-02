using CodeBase.Hero;
using UnityEngine;

namespace CodeBase.Boosts.Effects
{
    public class HealthEffect : IBoostEffect
    {
        private readonly int _value;

        public HealthEffect(int value)
        {
            _value = value;
        }

        public void Apply(GameObject hero) => 
            hero.GetComponent<HeroHealth>().ApplyHealth(_value);
    }
}