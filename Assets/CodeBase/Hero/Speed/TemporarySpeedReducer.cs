using System.Collections;
using CodeBase.Infrastructure;
using UnityEngine;

namespace CodeBase.Hero.Speed
{
    public class TemporarySpeedReducer : TemporarySpeedModifier
    {
        private readonly float _reductionFactor;

        public TemporarySpeedReducer(ICoroutineRunner coroutineRunner, float duration, float reductionFactor) : base(coroutineRunner, duration)
        {
            _reductionFactor = reductionFactor;
        }

        protected override IEnumerator ModifyCoroutine(Speed speed)
        {
            speed.Value *= _reductionFactor;
            yield return new WaitForSeconds(Duration);
            speed.Value /= _reductionFactor;
        }
    }
}