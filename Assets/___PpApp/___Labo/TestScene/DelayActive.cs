using System;
using DG.Tweening;
using UnityEngine;

namespace PPD
{
    public class DelayActive : MonoBehaviour
    {
        public bool setActiveFalseOnAwake___ForActivate = true;
        public float delaySec;
        public GameObject[] activate;
        public GameObject[] inactivate;
        private void Awake()
        {
            if (setActiveFalseOnAwake___ForActivate)
            {
                foreach (var o in activate)
                {
                    o.SetActive(false);
                }
            }

            DOVirtual.DelayedCall(delaySec, AllActive);
        }

        private void AllActive()
        {
            foreach (var o in activate)
            {
                o.SetActive(true);
            }
            foreach (var o in inactivate)
            {
                o.SetActive(false);
            }
        }
    }
}
