using CodeBase.EventBus;
using CodeBase.Infrastructure.AssetManagement;
using CodeBase.Infrastructure.Factory;
using CodeBase.Services;
using CodeBase.Services.Progress;
using CodeBase.Services.StaticData;
using CodeBase.UI.Services.Factory;
using CodeBase.UI.Services.Windows;

namespace CodeBase.Infrastructure.States
{
    public class BootstrapState: IState
    {
        private readonly GameStateMachine _gameStateMachine;
        private readonly SceneLoader _sceneLoader;
        private readonly AllServices _services;

        private const string BootstrapSceneName = "Bootstrap";
        
        public BootstrapState(GameStateMachine gameStateMachine, SceneLoader sceneLoader, AllServices services)
        {
            _gameStateMachine = gameStateMachine;
            _sceneLoader = sceneLoader;
            _services = services;
            RegisterServices();
        }
        
        public void Enter()
        {
            _sceneLoader.Load(name: BootstrapSceneName, onLoaded: EnterLoadLevelState);
        }

        public void Exit() { }

        private void EnterLoadLevelState() => 
            _gameStateMachine.Enter<LoadProgressState>();

        private void RegisterServices()
        {
            RegisterGameStateMachine(_gameStateMachine);
            ILevelProgressService levelProgressService = LevelProgressService();
            IEventBus eventBus = RegisterEventBus();
            IAssetProvider assetProvider = RegisterAssetProvider();
            IStaticDataService staticData = RegisterStaticData();
            IGameFactory gameFactory = RegisterGameFactory(assetProvider, eventBus, staticData, levelProgressService);
            IUIFactory uiFactory = RegisterUiFactory(staticData, assetProvider, eventBus, _gameStateMachine);
            IWindowService windowService = RegisterWindowService(uiFactory, eventBus);
        }

        private void RegisterGameStateMachine(GameStateMachine gameStateMachine)
        {
            _services.RegisterSingle<IGameStateMachine>(gameStateMachine);
            _services.Single<IGameStateMachine>();
        }

        private IEventBus RegisterEventBus()
        {
            _services.RegisterSingle<IEventBus>(new EventBus.EventBus());
            return _services.Single<IEventBus>();        
        }

        private IAssetProvider RegisterAssetProvider()
        {
            _services.RegisterSingle<IAssetProvider>(new AssetProvider());
            return _services.Single<IAssetProvider>();
        }

        private IGameFactory RegisterGameFactory(IAssetProvider assetProvider, IEventBus eventBus,
            IStaticDataService staticDataService, ILevelProgressService levelProgressService)
        { 
            _services.RegisterSingle<IGameFactory>(new GameFactory(assetProvider, eventBus, staticDataService, levelProgressService));
            return _services.Single<IGameFactory>();
        }

        private IStaticDataService RegisterStaticData()
        {
            StaticDataService staticData = new StaticDataService();
            staticData.Load();
            _services.RegisterSingle<IStaticDataService>(staticData);
            return _services.Single<IStaticDataService>();
        }

        private IUIFactory RegisterUiFactory(IStaticDataService staticData, IAssetProvider assetProvider, IEventBus eventBus, IGameStateMachine gameStateMachine)
        {
            _services.RegisterSingle<IUIFactory>(new UIFactory( staticData, assetProvider, eventBus, gameStateMachine));
            return _services.Single<IUIFactory>();
        }
        
        private IWindowService RegisterWindowService(IUIFactory uiFactory, IEventBus eventBus)
        { 
            _services.RegisterSingle<IWindowService>(new WindowService(uiFactory, eventBus));
            return _services.Single<IWindowService>();
        }
        
        private ILevelProgressService LevelProgressService()
        { 
            _services.RegisterSingle<ILevelProgressService>(new LevelProgressService());
            return _services.Single<ILevelProgressService>();
        }
    }
}