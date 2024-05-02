using UnityEngine;

namespace CodeBase.Hero.HUD
{
    public class HealthBarUnit : MonoBehaviour
    {
        [SerializeField]
        private GameObject _activeState;
        [SerializeField]
        private GameObject _inactiveState;

        public void SetState(bool isActive)
        {
            _activeState.SetActive(isActive);
            _inactiveState.SetActive(!isActive);
        }
    }
}