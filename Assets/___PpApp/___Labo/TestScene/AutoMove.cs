using UnityEngine;

namespace PPD
{
    public class AutoMove : PPD_MonoBehaviour
    {
        public Vector3 speed;
        private void Update()
        {
            this.transform.Translate(speed * Time.deltaTime, Space.World);
        }
    }
}
