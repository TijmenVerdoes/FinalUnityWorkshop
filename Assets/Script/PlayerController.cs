using UnityEngine;
using UnityEngine.InputSystem;

namespace Script
{
    public class PlayerController : MonoBehaviour
    {
        public float speed = 5.0f;
        private float _movementX;
        public float gravity = -9.81f;

        private bool _isGrounded;
        public float jumpHeight = 0.5f;

        private Vector3 _playerVelocity;
        
        private CharacterController _controller;
        public AudioSource jumpingSound;

        private void Start()
        {
            _controller = GetComponent<CharacterController>();
        }

        private void Update()
        {
            ExecuteMovement();
            CheckFloor();
            ExecuteJump();
            ExecuteGravity();
        }

        private void ExecuteMovement()
        {
            _playerVelocity.x = _movementX * speed * Time.deltaTime;
            _controller.Move(_playerVelocity);
        }

        private void CheckFloor()
        {
            _isGrounded = _controller.isGrounded;

            if (_isGrounded && _playerVelocity.y < 0f)
            {
                _playerVelocity.y = 0f;
            }
        }

        private void OnMove(InputValue movementValue)
        {
            var movementVector = movementValue.Get<Vector2>();

            _movementX = movementVector.x;
        }

        private void ExecuteJump()
        {
            if (Input.GetAxisRaw("Jump") != 0 && _isGrounded)
            {
                jumpingSound.Play(0);
                _playerVelocity.y += Mathf.Sqrt(jumpHeight * -2f * gravity);
            }
        }

        private void ExecuteGravity()
        {
            _playerVelocity.y += gravity * Time.deltaTime;

            _controller.Move(_playerVelocity * Time.deltaTime);
        }
    }
}
