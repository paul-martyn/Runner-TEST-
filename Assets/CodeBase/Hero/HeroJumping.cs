using CodeBase.Hero.Speed;
using CodeBase.Level.Road;
using CodeBase.Level.Road.Chunks;
using UnityEngine;

namespace CodeBase.Hero
{
    public class HeroJumping : MonoBehaviour
    {
        [SerializeField] private ObstacleObserver _obstacleObserver;
        [SerializeField] private CharacterController _characterController;
        [SerializeField] private HeroAnimator _animator;

        private Vector3 _verticalMovement;
        private int _jumpCounts;
        private float _gravity;
        private float _initialVelocity;

        private HeroSpeed _speed;
        private float _jumpLength;
        private float _jumpHeight;
        private float _jumpDuration;
        private float _timeToApex;

        private const int MaxJumpCounts = 2;
        private const float GroundedGravity = -1f;

        public void Construct(HeroSpeed speed, float jumpHeight, float jumpLength)
        {
            _speed = speed;
            _jumpLength = jumpLength;
            _jumpHeight = jumpHeight;
        }

        private void Start()
        {
            SetVariables();
        }

        private void OnEnable() =>
            _obstacleObserver.CollideWithObstacle += GetImpact;

        private void OnDisable() =>
            _obstacleObserver.CollideWithObstacle -= GetImpact;

        private void Update()
        {
            SetVariables();
            _characterController.Move(_verticalMovement * Time.deltaTime);
            HandleGravity();
            HandleJump();
        }

        private void ResetJumpCounts()
        {
            if (_characterController.isGrounded)
                _jumpCounts = 0;
        }

        private void SetVariables()
        {
            _jumpDuration = _jumpLength / _speed.Value;
            _timeToApex = _jumpDuration / 2f;
            _gravity = (-2 * _jumpHeight) / Mathf.Pow(_timeToApex, 2);
            _initialVelocity = (2 * _jumpHeight) / _timeToApex;
        }

        private void HandleGravity()
        {
            if (_characterController.isGrounded)
                _verticalMovement.y += GroundedGravity * Time.deltaTime;
            else
                _verticalMovement.y += _gravity * Time.deltaTime;
        }

        private void OnControllerColliderHit(ControllerColliderHit hit)
        {
            if (hit.gameObject.GetComponentInParent<RoadChunk>())
                ResetJumpCounts();
        }

        private void HandleJump()
        {
            if (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0))
            {
                if (_characterController.isGrounded || _jumpCounts < MaxJumpCounts)
                    Jump();
            }
        }

        private void Jump()
        {
            _jumpCounts += 1;
            _animator.Jump();
            _verticalMovement.y = _initialVelocity;
        }

        private void GetImpact(Obstacle obstacle)
        {
            if (obstacle.WithImpact)
                Jump();
        }
        
        public void Activate()
        {
            enabled = true;
            _verticalMovement.y = 0f;
        }
    }
}