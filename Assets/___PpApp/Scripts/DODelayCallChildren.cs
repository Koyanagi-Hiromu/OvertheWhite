using System;
using System.Collections.Generic;
using DG.Tweening;
using Sirenix.OdinInspector;
using UnityEngine;

namespace PPD
{
    public class DODelayCallChildren : DOPhaseComponent
    {
        public float delaySec;
        List<DOPhaseComponent> targetDOComponents;

        public override void Init()
        {
            targetDOComponents = GetComponentsInChildrenJustBelow<DOPhaseComponent>();

            foreach (var child in targetDOComponents)
            {
                child.gameObject.SetActive(false);
            }
        }

        public override void FlashMove()
        {
            foreach (var child in GetComponentsInChildrenJustBelow<DOPhaseComponent>())
            {
                child.FlashMove();
            }
        }

        public override void DOStart()
        {
            DOVirtual.DelayedCall(delaySec, Call_DOStart);
        }

        private void Call_DOStart()
        {
            foreach (var child in targetDOComponents)
            {
                child.gameObject.SetActive(true);
                child.DOStart();
            }
        }

        public override void DOKill()
        {
            foreach (var child in targetDOComponents)
            {
                child.DOKill();
                child.gameObject.SetActive(false);
            }
        }
    }
}
