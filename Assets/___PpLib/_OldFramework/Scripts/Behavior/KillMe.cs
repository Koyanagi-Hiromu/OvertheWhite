using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SR
{
    public class KillMe : MonoBehaviour
    {
        [SerializeField] private GameObject target;
        public void Kill()
        {
            if (target == null)
            {
                this.DestroyInstance();
            }
            else
            {
                target.DestroyInstance();
            }
        }
    }
}