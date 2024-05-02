using CodeBase.Data;
using CodeBase.EventBus.Signals.LevelSignals;
using CodeBase.Infrastructure.States;
using CodeBase.UI.Elements;
using UnityEngine;
using UnityEngine.UI;

namespace CodeBase.UI.Windows
{
    public class FailWindow : WindowBase
    {
        [SerializeField]
        private Button _restartButton;
        [SerializeField]
        private Button _continueButton;
        [SerializeField]
        private LevelProgressPanel _levelProgress;
        
        protected override void Initialize()
        {
            _levelProgress.RefreshData();
            _restartButton.onClick.AddListener(Restart);
            _continueButton.onClick.AddListener(Continue);
        }

        protected override void Cleanup()
        {
            base.Cleanup();
            _restartButton.onClick.RemoveListener(Restart);
        }
        
        private void Restart() => 
            _gameStateMachine.Enter<BootstrapState>();
        
        private void Continue() => 
            _eventBus.Invoke(new LevelContinueSignal());
    }
}
