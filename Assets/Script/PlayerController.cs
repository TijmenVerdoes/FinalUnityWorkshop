using UnityEngine;
using UnityEngine.InputSystem;

namespace Script
{
    public class PlayerController : MonoBehaviour
    {
        public float speed = 5.0f;
        private float movementX;
        public float gravity = -9.81f;

        private bool _isGrounded;
        public float jumpHeight = 0.5f;

        private Vector3 playerVelocity;
        
        private CharacterController controller;

        private void Start()
        {
            controller = GetComponent<CharacterController>();
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
            playerVelocity.x = movementX * speed * Time.deltaTime;
            controller.Move(playerVelocity);
        }

        private void CheckFloor()
        {
            _isGrounded = controller.isGrounded;

            if (_isGrounded && playerVelocity.y < 0f)
            {
                playerVelocity.y = 0f;
            }
        }

        private void OnMove(InputValue movementValue)
        {
            var movementVector = movementValue.Get<Vector2>();

            movementX = movementVector.x;
        }

        private void ExecuteJump()
        {
            if (Input.GetAxisRaw("Jump") != 0 && _isGrounded)
            {
                playerVelocity.y += Mathf.Sqrt(jumpHeight * -2f * gravity);
            }
        }

        private void ExecuteGravity()
        {
            playerVelocity.y += gravity * Time.deltaTime;

            controller.Move(playerVelocity * Time.deltaTime);
        }
    }
}
