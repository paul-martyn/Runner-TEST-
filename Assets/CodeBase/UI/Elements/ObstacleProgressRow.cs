using CodeBase.Services.Progress;
using TMPro;
using UnityEngine;

namespace CodeBase.UI.Elements
{
    public class ObstacleProgressRow : MonoBehaviour
    {
        [SerializeField]
        private TMP_Text _title;
        
        [SerializeField]
        private TMP_Text _count;
        
        public void RefreshData(ObstacleSaveData obstacleSaveData)
        {
            _title.SetText(obstacleSaveData.ObstacleType.ToString());
            _count.SetText(obstacleSaveData.Count.ToString());
        }
    }
}
