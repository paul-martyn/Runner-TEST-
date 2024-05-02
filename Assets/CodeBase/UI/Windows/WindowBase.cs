using CodeBase.EventBus;
using CodeBase.Infrastructure.States;
using UnityEngine;
using UnityEngine.UI;

namespace CodeBase.UI.Windows
{
    public abstract class WindowBase : MonoBehaviour
    {
        public Button CloseButton;
        
        protected IEventBus _eventBus;
        protected IGameStateMachine _gameStateMachine;
        
        public void Construct(IEventBus eventBus, IGameStateMachine gameStateMachine)
        {
            _eventBus = eventBus;
            _gameStateMachine = gameStateMachine;
        }

        private void Awake() => 
            OnAwake();

        private void Start()
        {
            Initialize();
            SubscribeUpdates();
        }

        private void OnDestroy() => 
            Cleanup();

        protected virtual void OnAwake() => 
            CloseButton.onClick.AddListener(() => Destroy(gameObject));

        protected virtual void Initialize(){}
        protected virtual void SubscribeUpdates(){}
        protected virtual void Cleanup(){}
    }
}