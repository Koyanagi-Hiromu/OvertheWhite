using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SR
{
    public class FixedUpdater_First : MonoBehaviour
    {
        public event Action firstUpdate = delegate { };

        private void FixedUpdate()
        {
            firstUpdate();
        }
    }
}
