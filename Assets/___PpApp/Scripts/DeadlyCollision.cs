using UnityEngine;

namespace PPD
{
    public class DeadlyCollision : PPD_MonoBehaviour
    {
        public bool ignoreTop;
        [Range(0, 1)] public float topValue;

        private void OnCollisionEnter(Collision other)
        {
            var tower = other.collider.GetComponent<HeadTower>();
            if (tower != null)
            {
                DeadCheck(tower, other);
            }
        }

        private void OnCollisionStay(Collision other)
        {
            var tower = other.collider.GetComponent<HeadTower>();
            if (tower != null)
            {
                DeadCheck(tower, other);
            }
        }

        private void DeadCheck(HeadTower headTower, Collision other)
        {
            if (!ignoreTop || other.contacts[0].normal.y < topValue)
            {
                headTower.RemoveBottom();
            }
        }
    }
}
