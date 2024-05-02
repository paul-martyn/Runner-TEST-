using CodeBase.EventBus;
using CodeBase.EventBus.Signals.LevelSignals;
using CodeBase.Hero.Speed;
using CodeBase.Infrastructure;
using CodeBase.Services;
using UnityEngine;

namespace CodeBase.Hero
{
    public class HeroBehavior : MonoBehaviour, ICoroutineRunner
    {
        [SerializeField] private HeroHealth _health;
        [SerializeField] private HeroMovement _movement;
        [SerializeField] private HeroJumping _jumping;
        [SerializeField] private HeroAnimator _animator;
        [SerializeField] private HeroDeath _death;
        [SerializeField] private HeroImmunity _immunity;

        private void OnEnable()
        {
            AllServices.Container.Single<IEventBus>().Subscribe<LevelStartSignal>(SetRunningState);
            AllServices.Container.Single<IEventBus>().Subscribe<LevelFailSignal>(SetDeathState);
            AllServices.Container.Single<IEventBus>().Subscribe<LevelCompleteSignal>(SetVictoryState);
            AllServices.Container.Single<IEventBus>().Subscribe<LevelContinueSignal>(Respawn);
        }
        
        private void OnDisable()
        {
            AllServices.Container.Single<IEventBus>().Unsubscribe<LevelStartSignal>(SetRunningState);
            AllServices.Container.Single<IEventBus>().Unsubscribe<LevelFailSignal>(SetDeathState);
            AllServices.Container.Single<IEventBus>().Unsubscribe<LevelCompleteSignal>(SetVictoryState);
            AllServices.Container.Single<IEventBus>().Unsubscribe<LevelContinueSignal>(Respawn);
        }

        private void Start()
        {
            SetIdleState();
        }

        private void SetIdleState()
        {
            _movement.enabled = false;
            _jumping.enabled = false;
            _animator.RefreshRunningState(false);
        }

        private void Respawn(LevelContinueSignal signal)
        {
            _immunity.Activate(1f);
            _health.ApplyHealth(1);
            _movement.Speed.AddModifier(new TemporarySpeedReducer(this, 3f, 0.5f));
            _movement.enabled = true;
            _jumping.Activate();
            _animator.Respawn();
            _death.Respawn();
            _animator.RefreshRunningState(true);
            _movement.SkipToNearest();
        }
        
        private void SetRunningState(LevelStartSignal signal)
        {
            _movement.enabled = true;
            _jumping.enabled = true;
            _animator.RefreshRunningState(true);
        }
        
        private void SetDeathState(LevelFailSignal signal)
        {
            _movement.enabled = false;
            _jumping.enabled = false;
            _animator.Die();
        }
        
        private void SetVictoryState(LevelCompleteSignal signal)
        {
            _movement.enabled = false;
            _jumping.enabled = false;
            _animator.Victory();
        }
            
    }
}