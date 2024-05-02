using System;
using System.Collections.Generic;
using System.Linq;
using CodeBase.Data.Static;
using CodeBase.UI.Services.Windows;
using UnityEngine;

namespace CodeBase.Services.StaticData
{
    public class StaticDataService : IStaticDataService
    {
        private const string GameBalancePath = "StaticData/GameBalance";
        private const string LevelStaticDataPath = "StaticData/LevelStaticData";
        private const string BoostsStaticDataPath = "StaticData/BoostsStaticData";
        private const string WindowConfigsPath = "StaticData/WindowStaticData";

        private GameBalance _gameBalance;
        private LevelStaticData _levelStaticData;
        private BoostsStaticData _boostsStaticData;
        private Dictionary<WindowId, WindowConfig> _windowConfigs;

        public void Load()
        {
            _gameBalance = Resources
                .Load<GameBalance>(path: GameBalancePath);
            
            _levelStaticData = Resources
                .Load<LevelStaticData>(path: LevelStaticDataPath);
            
            _boostsStaticData = Resources
                .Load<BoostsStaticData>(path: BoostsStaticDataPath);
            
            _windowConfigs = Resources
                .Load<WindowStaticData>(path: WindowConfigsPath)
                .Configs
                .ToDictionary(x => x.WindowId, x => x);
        }

        public GameBalance GameBalance() => 
            _gameBalance;

        public LevelStaticData LevelStaticData()
            => _levelStaticData;

        public BoostsStaticData BoostsStaticData()
            => _boostsStaticData;

        public WindowConfig ForWindow(WindowId windowId)
        {
            foreach (KeyValuePair<WindowId, WindowConfig> pair in _windowConfigs.Where(pair => pair.Key == windowId))
                return pair.Value;
            throw new ArgumentNullException();
        }
    }
}