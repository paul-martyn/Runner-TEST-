using CodeBase.Hero;
using CodeBase.Hero.Speed;
using CodeBase.Infrastructure;
using UnityEngine;

namespace CodeBase.Boosts.Effects
{
    public class SpeedEffect : IBoostEffect
    {
        private readonly float _value;
        private readonly float _duration;
        private readonly ICoroutineRunner _coroutineRunner;

        public SpeedEffect(float value, float duration, ICoroutineRunner coroutineRunner)
        {
            _value = value;
            _duration = duration;
            _coroutineRunner = coroutineRunner;
        }

        public void Apply(GameObject hero) => 
            hero.GetComponent<HeroMovement>().Speed.AddModifier(new TemporarySpeedBooster(_coroutineRunner, _duration, _value));
    }
}