using System;
using CodeBase.Level.Road;
using CodeBase.Services.Progress;

namespace CodeBase.Data
{
    [Serializable]
    public class LevelProgress
    {
        public ObstacleSaveData[] _obstacleSaveData =
        {
            new ObstacleSaveData { ObstacleType = ObstacleType.Pit, Count = 0 },
            new ObstacleSaveData { ObstacleType = ObstacleType.DoublePit, Count = 0 },
            new ObstacleSaveData { ObstacleType = ObstacleType.Fancy, Count = 0 },
            new ObstacleSaveData { ObstacleType = ObstacleType.Saw, Count = 0 }
        };

        public void IncreaseByType(ObstacleType type)
        {
            foreach (ObstacleSaveData obstacleSaveData in _obstacleSaveData)
            {
                if (type == obstacleSaveData.ObstacleType)
                {
                    obstacleSaveData.Count += 1;
                    return;
                }
            }
        }
    }
}