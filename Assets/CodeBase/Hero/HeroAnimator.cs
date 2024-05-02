using CodeBase.Hero.Speed;
using UnityEngine;

namespace CodeBase.Hero
{
    public class HeroAnimator : MonoBehaviour
    {
        [SerializeField]
        private Animator _animator;
        
        private readonly int _runningHash = Animator.StringToHash("IsRunning");
        private readonly int _jumpHash = Animator.StringToHash("Jump");
        private readonly int _victoryHash = Animator.StringToHash("Victory");
        private readonly int _deadHash = Animator.StringToHash("IsDead");

        private float _initialSpeedValue;
        private HeroSpeed _speed;
        
        public void Construct(HeroSpeed heroSpeed)
        {
            _initialSpeedValue = heroSpeed.Value;
            _speed = heroSpeed;
        }

        private void Update() => 
            RefreshAnimationSpeed();

        private void RefreshAnimationSpeed() => 
            _animator.speed = _speed.Value / _initialSpeedValue;

        public void RefreshRunningState(bool isRunning) =>
            _animator.SetBool(_runningHash, isRunning);

        public void Jump()
        {
            _animator.SetTrigger(_jumpHash);
        }

        public void Victory() =>
            _animator.SetTrigger(_victoryHash);
        
        public void Die() => 
            _animator.SetBool(_deadHash, true);
        
        public void Respawn() => 
            _animator.SetBool(_deadHash, false);
    }
}