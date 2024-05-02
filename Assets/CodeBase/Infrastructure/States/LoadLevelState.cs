using System.Collections.Generic;
using CodeBase.CameraLogic;
using CodeBase.EventBus;
using CodeBase.Hero;
using CodeBase.Hero.HUD;
using CodeBase.Infrastructure.Factory;
using CodeBase.Level.Road;
using CodeBase.Level.Road.Chunks;
using CodeBase.Services.Progress;
using CodeBase.Services.StaticData;
using CodeBase.UI.Services.Factory;
using CodeBase.UI.Services.Windows;
using UnityEngine;

namespace CodeBase.Infrastructure.States
{
    public class LoadLevelState : IState
    {
        private const string MainSceneName = "Main";

        private readonly IGameStateMachine _gameStateMachine;
        private readonly IStaticDataService _staticData;
        private readonly IGameFactory _gameFactory;
        private readonly SceneLoader _sceneLoader;
        private readonly LoadingCurtain _loadingCurtain;
        private readonly IEventBus _eventBus;
        private readonly IUIFactory _uiFactory;
        private readonly IWindowService _windowService;
        private readonly ILevelProgressService _levelProgressService;

        public LoadLevelState(IGameStateMachine gameStateMachine, SceneLoader sceneLoader,
            LoadingCurtain loadingCurtain, IGameFactory gameFactory, IStaticDataService staticData, 
            IEventBus eventBus, IUIFactory uiFactory, IWindowService windowService, ILevelProgressService levelProgressService)
        {
            _gameStateMachine = gameStateMachine;
            _sceneLoader = sceneLoader;
            _loadingCurtain = loadingCurtain;
            _gameFactory = gameFactory;
            _staticData = staticData;
            _eventBus = eventBus;
            _uiFactory = uiFactory;
            _windowService = windowService;
            _levelProgressService = levelProgressService;
        }

        public void Enter()
        {
            _loadingCurtain.Show();
            _sceneLoader.Load(MainSceneName, OnLoaded);
        }

        public void Exit()
        {
            _loadingCurtain.Hide();
        }

        private void OnLoaded()
        { 
            InitializeUI();
             InitializeLevel();
            _gameStateMachine.Enter<GameLoopState>();
        }

        private void InitializeUI()   
        {
            _uiFactory.CreateUIRoot();
            _windowService.Open(WindowId.MainMenu);
        }

        private void InitializeLevel()
        {
            _levelProgressService.CreateNewProgress();
            IRoadGenerator roadGenerator = new RandomRoadGenerator(_staticData.LevelStaticData(), _gameFactory, _eventBus);
            roadGenerator.Generate();
            GameObject hero = InitHero(roadGenerator.GetStartingPosition(), roadGenerator.GetRoad());
            InitializeCamera(hero);
            GameObject hud = _gameFactory.CreateHud();
            hud.GetComponentInChildren<HealthBar>().Initialize(hero.GetComponent<HeroHealth>());
        }
        
        private GameObject InitHero(Vector3 position, List<RoadChunk> road) => 
            _gameFactory.CreateHero(position, road);

        private void InitializeCamera(GameObject hero)
        {
            if (Camera.main != null)
                Camera.main.GetComponent<CameraFollow>().SetTarget(hero.GetComponent<HeroMovement>().Model);
        }
    }
}