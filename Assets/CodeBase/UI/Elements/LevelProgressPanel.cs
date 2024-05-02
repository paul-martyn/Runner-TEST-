using CodeBase.Data;
using CodeBase.Services;
using CodeBase.Services.Progress;
using UnityEngine;

namespace CodeBase.UI.Elements
{
    public class LevelProgressPanel : MonoBehaviour
    {
        [SerializeField]
        private ObstacleProgressRow[] _obstacleProgressRows;

        private LevelProgress _levelProgress;

        public void RefreshData()
        {
            _levelProgress = AllServices.Container.Single<ILevelProgressService>().Progress;
            for (int i = 0; i < _levelProgress._obstacleSaveData.Length; i++)
            {
                _obstacleProgressRows[i].RefreshData(_levelProgress._obstacleSaveData[i]);
            }
        }
    }
}
