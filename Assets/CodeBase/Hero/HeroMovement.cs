using System.Collections.Generic;
using System.Linq;
using CodeBase.Data;
using CodeBase.EventBus;
using CodeBase.EventBus.Signals.LevelSignals;
using UnityEngine;
using CodeBase.Hero.Speed;
using CodeBase.Infrastructure;
using CodeBase.Level.Road.Chunks;

namespace CodeBase.Hero
{
    public class HeroMovement : MonoBehaviour, ICoroutineRunner
    {
        [SerializeField]
        private CharacterController _characterController;
        [SerializeField,  Space(10f)]
        private float _rotationSpeed = 5f;
        [SerializeField]
        private Transform _model;
        private List<Transform> _path;
        private HeroSpeed _speed;
        private IEventBus _eventBus;
        private RoadTracker _roadTracker;
        private Vector3 _targetPosition;

        private const float Threshold = 0.5f;
        
        public HeroSpeed Speed => _speed;
        public Transform Model => _model;
        private bool CanMove => _path != null && _path.Count > 0;

        public void Construct(List<RoadChunk> road, HeroSpeed speed, IEventBus eventBus, LevelProgress levelProgress)
        {
            _path = road.Select(chunk => chunk.Center).ToList();
            _speed = speed;
            _eventBus = eventBus;
            _roadTracker = new RoadTracker(road, levelProgress);
        }

        private void Update()
        {
            if (!CanMove)
                return;

            Vector3 currentPosition = transform.position;
            UpdateTargetPosition();
            Vector3 moveDirection = new Vector3(_targetPosition.x - currentPosition.x, 0f, _targetPosition.z - currentPosition.z).normalized;
            Quaternion targetRotation = _path[0].rotation;

            Move(moveDirection);
            RotateModel(targetRotation);
        }

        private void UpdateTargetPosition()
        {
            Vector3 currentPosition = transform.position;
            Vector3 targetPosition = _path[0].position;

            Vector2 currentProjection = new Vector2(currentPosition.x, currentPosition.z);
            Vector2 targetProjection = new Vector2(targetPosition.x, targetPosition.z);

            if (Vector2.Distance(currentProjection, targetProjection) < Threshold)
            {
                if (_path.Count > 1)
                {
                    _path.Remove(_path[0]);
                    _roadTracker.RoadChunkPassed();
                    targetPosition = _path[0].position;
                }
                else
                {
                    _eventBus.Invoke(new LevelCompleteSignal());        
                }
            }
            _targetPosition = targetPosition;
        }
        
        private void Move(Vector3 direction) => 
            _characterController.Move(direction * _speed.Value * Time.deltaTime);

        private void RotateModel(Quaternion targetRotation) => 
            _model.rotation = Quaternion.Lerp(_model.rotation, targetRotation, _rotationSpeed * Time.deltaTime);

        public void SkipToNearest()
        {
            _characterController.enabled = false;
            int skippedCount = _roadTracker.GeSkippedChunkCount();
            for (int i = 0; i < skippedCount; i++) 
                SkipChunk();
            transform.position = _targetPosition;
            _characterController.enabled = true;
        }

        private void SkipChunk()
        {
            if (_path.Count <= 1)
                return;
            
            _path.Remove(_path[0]);
            _roadTracker.RoadChunkPassed();
            _targetPosition = _path[0].position;
        }
    }
}