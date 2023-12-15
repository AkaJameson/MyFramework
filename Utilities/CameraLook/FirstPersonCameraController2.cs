/*using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;
namespace CameraLook
{
    public class RoamingCam : MonoBehaviour
    {
        public CharacterController characterController;
        public Transform body;
        public Transform eye;

        public float moveSpeed = 5.0f;
        public float mouseSensitivity = 2.0f;
        public float verticalRotationLimit = 80.0f;
        public float jumpForce = 8.0f;
        public float gravity = 20.0f;

        private float verticalRotation = 0;
        private Vector3 moveDirection = Vector3.zero;

        void Start()
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }

        void Update()
        {
            // Character Movement
            float horizontalMovement = Input.GetAxis("Horizontal");
            float verticalMovement = Input.GetAxis("Vertical");

            Vector3 movement = transform.forward * verticalMovement + transform.right * horizontalMovement;
            moveDirection.x = movement.x * moveSpeed;
            moveDirection.z = movement.z * moveSpeed;

            // Apply gravity
            if (characterController.isGrounded)
            {
                moveDirection.y = 0f;
                if (Input.GetButtonDown("Jump"))
                {
                    moveDirection.y = jumpForce;
                }
            }
            else
            {
                moveDirection.y -= gravity * Time.deltaTime;
            }

            characterController.Move(moveDirection * Time.deltaTime);

            // Camera Rotation
            if (Input.GetMouseButton(1)) // Right mouse button is held down
            {
                float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity;
                float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity;

                verticalRotation -= mouseY;
                verticalRotation = Mathf.Clamp(verticalRotation, -verticalRotationLimit, verticalRotationLimit);

                eye.localRotation = Quaternion.Euler(verticalRotation, 0, 0);
                body.Rotate(Vector3.up * mouseX);
            }
        }
    }
*/