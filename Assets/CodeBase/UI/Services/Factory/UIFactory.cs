using CodeBase.Data.Static;
using CodeBase.EventBus;
using CodeBase.Infrastructure.AssetManagement;
using CodeBase.Infrastructure.States;
using CodeBase.Services.StaticData;
using CodeBase.UI.Services.Windows;
using CodeBase.UI.Windows;
using UnityEngine;

namespace CodeBase.UI.Services.Factory
{
    public class UIFactory : IUIFactory
    {
        private Transform _uiRoot;
        
        private readonly IStaticDataService _staticData;
        private readonly IAssetProvider _assetProvider;
        private readonly IEventBus _eventBus;
        private readonly IGameStateMachine _gameStateMachine;

        public UIFactory(IStaticDataService staticData, IAssetProvider assetProvider, IEventBus eventBus, IGameStateMachine gameStateMachine)
        {
            _staticData = staticData;
            _assetProvider = assetProvider;
            _eventBus = eventBus;
            _gameStateMachine = gameStateMachine;
        }

        public void CreateUIRoot()
        {
            GameObject root = _assetProvider.Instantiate(AssetsPath.UIRootPath);
            _uiRoot = root.transform;
        }

        public void CreateMainMenu() => 
            InstantiateScreen(WindowId.MainMenu);

        public void CreateCompleteScreen() => 
            InstantiateScreen(WindowId.Complete);

        public void CreateFailScreen() => 
            InstantiateScreen(WindowId.Fail);

        private void InstantiateScreen(WindowId windowId)
        {
            WindowConfig config = _staticData.ForWindow(windowId);
            WindowBase screen = Object.Instantiate(config.Prefab, _uiRoot);
            screen.Construct(_eventBus, _gameStateMachine);
        }
    }
}