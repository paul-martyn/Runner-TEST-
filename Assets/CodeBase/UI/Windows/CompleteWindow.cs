using CodeBase.Infrastructure.States;
using CodeBase.Services;
using CodeBase.UI.Elements;
using UnityEngine;
using UnityEngine.UI;

namespace CodeBase.UI.Windows
{
    public class CompleteWindow : WindowBase
    {
        [SerializeField]
        private Button _restartButton;
        [SerializeField]
        private LevelProgressPanel _levelProgress;
        
        protected override void Initialize()
        {
            _levelProgress.RefreshData();
            _restartButton.onClick.AddListener(Restart);
        }

        protected override void Cleanup()
        {
            base.Cleanup();
            _restartButton.onClick.RemoveListener(Restart);
        }

        private void Restart() => 
            AllServices.Container.Single<IGameStateMachine>().Enter<BootstrapState>();
    }
}