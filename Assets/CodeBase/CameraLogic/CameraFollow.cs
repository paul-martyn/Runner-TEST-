using System;
using Cinemachine;
using CodeBase.Services;
using CodeBase.UI.Services.Windows;
using UnityEngine;

namespace CodeBase.CameraLogic
{
    public class CameraFollow : MonoBehaviour
    {
        [SerializeField] private CinemachineVirtualCamera _gameplayVC;

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                Debug.LogError(AllServices.Container.Single<IWindowService>());
                AllServices.Container.Single<IWindowService>().Open(WindowId.MainMenu);
            }
            if (Input.GetKeyDown(KeyCode.Alpha2))
            {
                AllServices.Container.Single<IWindowService>().Open(WindowId.Complete);
            }
            if (Input.GetKeyDown(KeyCode.Alpha3))
            {
                AllServices.Container.Single<IWindowService>().Open(WindowId.Fail);
            }
        }

        public void SetTarget(Transform target)
        {
            _gameplayVC.Follow = target;
            _gameplayVC.LookAt = target;
        }
    }
}