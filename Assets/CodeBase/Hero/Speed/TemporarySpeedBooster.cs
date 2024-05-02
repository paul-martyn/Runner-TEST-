using System.Collections;
using CodeBase.Infrastructure;
using UnityEngine;

namespace CodeBase.Hero.Speed
{
    public class TemporarySpeedBooster : TemporarySpeedModifier
    {
        private readonly float _boostValue;

        public TemporarySpeedBooster(ICoroutineRunner coroutineRunner, float duration, float boostValue) : base(coroutineRunner, duration)
        {
            _boostValue = boostValue;
        }

        protected override IEnumerator ModifyCoroutine(Speed speed)
        {
            speed.Value += _boostValue;
            yield return new WaitForSeconds(Duration);
            speed.Value -= _boostValue;
        }
    }
}