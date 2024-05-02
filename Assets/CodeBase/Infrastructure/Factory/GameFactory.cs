using System.Collections.Generic;
using CodeBase.Boosts;
using CodeBase.Data.Static;
using CodeBase.EventBus;
using CodeBase.Hero;
using CodeBase.Hero.Speed;
using CodeBase.Infrastructure.AssetManagement;
using CodeBase.Level.Road.Chunks;
using CodeBase.Services.Progress;
using CodeBase.Services.StaticData;
using UnityEngine;

namespace CodeBase.Infrastructure.Factory
{
    public class GameFactory : IGameFactory
    {
        private readonly IAssetProvider _assetProvider;
        private readonly IEventBus _eventBus;
        private readonly IStaticDataService _staticDataService;
        private readonly ILevelProgressService _levelProgressService;

        public GameFactory(IAssetProvider assetProvider, IEventBus eventBus,
            IStaticDataService staticDataService, ILevelProgressService levelProgressService)
        {
            _assetProvider = assetProvider;
            _eventBus = eventBus;
            _staticDataService = staticDataService;
            _levelProgressService = levelProgressService;
        }

        public GameObject CreateHud()
        {
            GameObject hud = Instantiate(AssetsPath.HudPath);
            return hud;
        }

        public GameObject CreateHero(Vector3 position, List<RoadChunk> road)
        {
            GameObject hero = Instantiate(AssetsPath.HeroPath, position);
            GameBalance gameBalance = _staticDataService.GameBalance();
            HeroSpeed heroSpeed = new HeroSpeed(gameBalance.PlayerStats.PlayerSpeed);
            hero.GetComponent<HeroMovement>().Construct(road, heroSpeed, _eventBus, _levelProgressService.Progress);
            hero.GetComponent<HeroJumping>()
                .Construct(heroSpeed, GameBalance.ObstacleHeightUnit, GameBalance.ObstacleLengthUnit);
            hero.GetComponent<HeroHealth>().Construct(gameBalance.PlayerStats.PlayerHealth);
            hero.GetComponent<HeroAnimator>().Construct(heroSpeed);
            return hero;
        }

        public Boost CreateBoost(Boost prefab, Transform parent)
        {
            Boost boost = Object.Instantiate(prefab, parent);
            boost.transform.localPosition = Vector3.zero;
            boost.Construct(_staticDataService.BoostsStaticData());
            return boost;
        }

        public RoadChunk CreateChunk(RoadChunk roadChunk)
        {
            RoadChunk chunk = Object.Instantiate(roadChunk);
            BoostSpawner boostSpawner = chunk.GetComponent<BoostSpawner>();
            if (boostSpawner != null)
                boostSpawner.Construct(this, _staticDataService.BoostsStaticData());
            
            return chunk;
        }


        private GameObject Instantiate(string prefabPath)
        {
            GameObject gameObject = _assetProvider.Instantiate(path: prefabPath);
            return gameObject;
        }

        private GameObject Instantiate(string prefabPath, Vector3 at)
        {
            GameObject gameObject = _assetProvider.Instantiate(path: prefabPath, at: at);
            return gameObject;
        }
    }
}