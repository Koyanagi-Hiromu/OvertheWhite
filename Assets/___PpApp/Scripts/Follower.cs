using UnityEngine;

namespace PPD
{
    public class Follower : PPD_MonoBehaviour, IFollower
    {
        public bool autoAttachTo1stController;

        public void MoveTo(Vector3 next)
        {
            transform.localPosition = next;
        }

        private void Awake()
        {
            if (autoAttachTo1stController)
            {
                FindObjectOfType<FollowController>().Add(this);
            }
        }
    }
}

