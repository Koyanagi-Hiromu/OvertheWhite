using UnityEngine;

namespace PPD
{
    public class MoveCharacter : PPD_MonoBehaviour
    {
        public enum Mode
        {
            Force, Velocity
        }
        public Mode mode;
        public new Rigidbody rigidbody;
        private void FixedUpdate()
        {
            var input = Vector3.zero;
            if (Input.GetKey(KeyCode.A))
            {
                input += Vector3.left;
            }
            if (Input.GetKey(KeyCode.S))
            {
                input += Vector3.back;
            }
            if (Input.GetKey(KeyCode.D))
            {
                input += Vector3.right;
            }
            if (Input.GetKey(KeyCode.W))
            {
                input += Vector3.forward;
            }

            PlayerInput(input);
        }

        void PlayerInput(Vector3 v)
        {
            if (mode == Mode.Force)
            {
                rigidbody.AddForce(v * Data.Ins.walkForce);
            }
            else
            {
                if (v == Vector3.zero)
                {
                    rigidbody.velocity = Vector3.zero;
                }
                else
                {
                    rigidbody.velocity = v * Data.Ins.walkSpeed;
                }
            }
        }
    }
}
