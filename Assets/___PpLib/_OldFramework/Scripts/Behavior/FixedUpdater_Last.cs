using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SR
{
    public class FixedUpdater_Last : MonoBehaviour
    {
        public event Action lastUpdate = delegate { };

        private void FixedUpdate()
        {
            lastUpdate();
        }
    }
}
