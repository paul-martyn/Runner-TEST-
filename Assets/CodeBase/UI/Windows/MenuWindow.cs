using CodeBase.EventBus.Signals.LevelSignals;
using UnityEngine;
using UnityEngine.UI;

namespace CodeBase.UI.Windows
{
    public class MenuWindow :  WindowBase
    {
        [SerializeField] private Button _playButton;
        
        protected override void Initialize() => 
            _playButton.onClick.AddListener(Restart);

        protected override void Cleanup()
        {
            base.Cleanup();
            _playButton.onClick.RemoveListener(Restart);
        }

        private void Restart() => 
            _eventBus.Invoke(new LevelStartSignal());
    }
}