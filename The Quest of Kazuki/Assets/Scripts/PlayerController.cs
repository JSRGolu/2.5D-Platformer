using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TheQuestOfKazuki
{
    public class PlayerController : MonoBehaviour
    {
        public float moveSpeed = 4.5f;
        public float sprintSpeed = 7f;
        public float jumpHeight = 1.5f;
        public float gravity = -9.80665f;

        private Vector3 moveDirection;
        private Vector3 velocity;

        private CharacterController characterController;

        private bool onGround;
        public Transform groundCheckObj;
        public float groundBuffer = 0.2f;
        public LayerMask groundLayer;

        private bool xAxis = true;

        void Start()
        {
            characterController = GetComponent<CharacterController>();
        }

        void Update()
        {
            onGround = Physics.CheckSphere(groundCheckObj.position, groundBuffer, groundLayer);

            if(onGround && velocity.y < 0)
            {
                velocity.y = -2f;
            }

            float move = Input.GetAxis("Horizontal");

            float currentSpeed = Input.GetButton("Sprint") ? sprintSpeed : moveSpeed;

            moveDirection = new Vector3(move * currentSpeed, 0f, 0f);

            if (move > 0 && !xAxis)
            {
                xAxis = !xAxis;
                transform.Rotate(0f, 180f, 0f);
            }
            else if (move < 0 && xAxis)
            {
                xAxis = !xAxis;
                transform.Rotate(0f, 180f, 0f);
            }

            characterController.Move(moveDirection * Time.deltaTime);

            if(Input.GetButtonDown("Jump") && onGround)
            {
                velocity.y = Mathf.Sqrt(-2f * gravity * jumpHeight);
            }

            velocity.y += gravity * Time.deltaTime;

            characterController.Move(velocity * Time.deltaTime);

        }
    }
}

