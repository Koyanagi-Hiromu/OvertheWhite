using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SR
{
    public class ObjectChaser : MonoBehaviour
    {
        public GameObject target;

        void Update()
        {
            this.transform.position = target.transform.position;
        }
    }
}
