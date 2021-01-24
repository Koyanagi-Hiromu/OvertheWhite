using UnityEngine;

namespace SR
{
    public class AttachTransform : MonoBehaviour
    {
        public Transform attachTo;
        private void LateUpdate()
        {
            this.transform.position = attachTo.position;
        }
    }
}