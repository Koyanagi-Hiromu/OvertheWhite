using System;
using UnityEngine;

namespace PPD
{
    [RequireComponent(typeof(InputController))]
    public class InputController_3D_Plain : PPD_MonoBehaviour
    {
        public HeadTower headTower;
        Rigidbody rb;
        InputController inputController;
        private void Awake()
        {
            rb = GetComponent<Rigidbody>();
            inputController = GetComponent<InputController>();
        }

        private void FixedUpdate()
        {
            Walk();
            Jump();
        }

        private void Jump()
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                //TODO:
            }
        }

        public void Walk()
        {
            var dir3D = inputController.GetInputDirection();
            var velocity = dir3D * Data.Ins.characterWalkSpeed;
            velocity.y = rb.velocity.y;
            rb.velocity = velocity;
        }
    }
}

