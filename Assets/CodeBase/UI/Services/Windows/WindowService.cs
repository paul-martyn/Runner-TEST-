using CodeBase.EventBus;
using CodeBase.EventBus.Signals.LevelSignals;
using CodeBase.UI.Services.Factory;

namespace CodeBase.UI.Services.Windows
{
    public class WindowService : IWindowService
    {
        private readonly IUIFactory _uiFactory;
        private readonly IEventBus _eventBus;
        
        public WindowService(IUIFactory uiFactory, IEventBus eventBus)
        {
            _uiFactory = uiFactory;
            _eventBus = eventBus;
            
            _eventBus.Subscribe<LevelCompleteSignal>(OnLevelComplete);
            _eventBus.Subscribe<LevelFailSignal>(OnLevelFail);
        }

        private void OnLevelComplete(LevelCompleteSignal signal) => 
            Open(WindowId.Complete);
        
        private void OnLevelFail(LevelFailSignal signal) => 
            Open(WindowId.Fail);

        public void Open(WindowId windowId)
        {
            switch (windowId)
            {
                case WindowId.MainMenu:
                    _uiFactory.CreateMainMenu();
                    break;
                case WindowId.Complete:
                    _uiFactory.CreateCompleteScreen();
                    break;
                case WindowId.Fail:
                    _uiFactory.CreateFailScreen();
                    break;
            }
        }
    }
}