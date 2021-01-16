using UnityEngine;

namespace PPD
{
    public class AutoRotate : MonoBehaviour
    {
        private new Transform transform;
        public Vector3 speed;

        private void Awake()
        {
            this.transform = base.transform;
        }

        private void Update()
        {
            this.transform.localRotation *= Quaternion.Euler(speed * Time.deltaTime);
        }
    }
}
