using System;
using CodeBase.Level.Road;
using UnityEngine;

namespace CodeBase.Hero
{
    public class ObstacleObserver : MonoBehaviour
    {
        public event Action<Obstacle> CollideWithObstacle;

        private void OnControllerColliderHit(ControllerColliderHit hit)
        {
            Obstacle obstacle = hit.gameObject.GetComponent<Obstacle>();
            if (obstacle != null) 
                CollideWithObstacle?.Invoke(obstacle);
        }
    }
}