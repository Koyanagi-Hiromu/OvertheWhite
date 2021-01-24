using System;
using DG.Tweening;
using UnityEngine;

namespace PPD
{
    public class DelayEnable : MonoBehaviour
    {
        public bool setEnableFalseOnAwake___ForEnable = true;
        public float delaySec;
        public Component[] enableComponent;
        public Component[] disableComponent;
        private void Awake()
        {
            if (setEnableFalseOnAwake___ForEnable)
            {
                foreach (var o in enableComponent)
                {
                    o.SetEnable(false);
                }
            }
        }

        private void Start()
        {
            DOVirtual.DelayedCall(delaySec, AllEnable);
        }

        private void AllEnable()
        {
            foreach (var o in enableComponent)
            {
                o.SetEnable(true);
            }
            foreach (var o in disableComponent)
            {
                o.SetEnable(false);
            }
        }
    }
}
