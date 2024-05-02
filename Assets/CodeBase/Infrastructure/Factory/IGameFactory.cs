using System.Collections.Generic;
using CodeBase.Boosts;
using CodeBase.Level.Road.Chunks;
using CodeBase.Services;
using UnityEngine;

namespace CodeBase.Infrastructure.Factory
{
    public interface IGameFactory : IService
    {
        public Boost CreateBoost(Boost prefab, Transform parent);
        public RoadChunk CreateChunk(RoadChunk prefab);
        public GameObject CreateHero(Vector3 position, List<RoadChunk> road);
        public GameObject CreateHud();
    }
}