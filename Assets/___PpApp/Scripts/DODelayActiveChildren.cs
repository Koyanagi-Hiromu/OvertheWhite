using System;
using DG.Tweening;
using Sirenix.OdinInspector;
using UnityEngine;

namespace PPD
{
    [TypeInfoBox("子を全部アクティブにします。\nあらかじめ非アクティブにしておいてください。")]
    public class DODelayActiveChildren : DOPhaseComponent
    {
        public float delaySec;
        DOPhaseComponent[] targetDOComponents;

        public override void EditorTransition()
        {
            foreach (var child in GetComponentsInChildren<DOPhaseComponent>())
            {
                if (this != child)
                {
                    child.EditorTransition();
                }
            }
        }

        private void Awake()
        {
            targetDOComponents = GetComponentsInChildren<DOPhaseComponent>();
            SetAllActive(true, false);
        }

        public override void OnUnityEnable()
        {
            DOVirtual.DelayedCall(delaySec, SetAllActive);
        }

        public override void OnUnityDisable()
        {
            SetAllActive(false, false);
        }

        private void SetAllActive() => SetAllActive(false, true);
        private void SetAllActive(bool ignore, bool active)
        {
            foreach (var child in targetDOComponents)
            {
                if (this != child)
                {
                    child.ignoreOnEnable = ignore;
                    child.gameObject.SetActive(active);
                }
            }
        }
    }
}
