using System;
using System.Collections.Generic;
using System.Linq;
using CodeBase.Data.Static;
using CodeBase.Infrastructure.Factory;
using CodeBase.Services.Random;
using UnityEngine;

namespace CodeBase.Boosts
{
    public class BoostSpawner : MonoBehaviour
    {
        public Transform _parent;
        
        private IGameFactory _gameFactory;
        private BoostsStaticData _boostsStaticData;
        
        public void Construct(IGameFactory gameFactory, BoostsStaticData boostsStaticData)
        {
            _gameFactory = gameFactory;
            _boostsStaticData = boostsStaticData;

            TrySpawn();
        }
        
        private void TrySpawn()
        {
            if (IsSpawn(_boostsStaticData.SpawnСhance)) 
                Spawn(GetRandomBoost());
        }

        private void Spawn(Boost prefab)
        {
            _gameFactory.CreateBoost(prefab, _parent);
        }

        private Boost GetRandomBoost()
        {
            Boost rolledBoost = null;
            List<float> rollChances = new List<float>(_boostsStaticData.Boosts.Count);
            rollChances.AddRange(_boostsStaticData.Boosts.Select(boostConfig => boostConfig.RollChance));

            float rolledChance = RandomService.Range(0f, rollChances.Sum());
            float checkedRatesSum  = 0f;
            
            for (int i = 0; i < rollChances.Count; i++)
            {
                checkedRatesSum += rollChances[i];
                if (rolledChance <= checkedRatesSum)
                {
                    rolledBoost = _boostsStaticData.Boosts[i].Prefab;
                    break;
                }
            }

            if (rolledBoost == null)
                throw new NullReferenceException();

            return rolledBoost;
        }

        private bool IsSpawn(float chances) => 
            chances >= RandomService.Range(0f, 100f);
    }
}