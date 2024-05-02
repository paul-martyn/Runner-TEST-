using System.Collections;
using CodeBase.Infrastructure;

namespace CodeBase.Hero.Speed
{
    public abstract class TemporarySpeedModifier : ISpeedModifier
    {
        private readonly ICoroutineRunner _coroutineRunner;
        protected readonly float Duration;

        protected TemporarySpeedModifier(ICoroutineRunner coroutineRunner, float duration)
        {
            _coroutineRunner = coroutineRunner;
            Duration = duration;
        }

        public void Modify(Speed speed) => 
            _coroutineRunner.StartCoroutine(ModifyCoroutine(speed));

        protected abstract IEnumerator ModifyCoroutine(Speed speed);
    }
}