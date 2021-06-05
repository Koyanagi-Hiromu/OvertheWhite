using System;
using DG.Tweening;
using Sirenix.OdinInspector;
using UnityEngine;

namespace PPD
{
    [TypeInfoBox("子を全部アクティブにします。\nあらかじめ非アクティブにしておいてください。")]
    public class DODelayActiveChildren : PPD_MonoBehaviour, IDOPhaseComponent
    {
        public float delaySec;
        Transform[] activedTransforms;

        public void EditorTransition()
        {
            foreach (var child in GetComponentsInChildren<IDOPhaseComponent>(includeInactive: true))
            {
                if ((IDOPhaseComponent)this != child)
                {
                    child.EditorTransition();
                }
            }
        }

        private void Awake()
        {
            activedTransforms = GetComponentsInChildren<Transform>(includeInactive: true);
            SetAllActive(false);
        }

        public void OnEnable()
        {
            DOVirtual.DelayedCall(delaySec, SetAllActive);
        }

        private void OnDisable()
        {
            SetAllActive(false);
        }

        private void SetAllActive() => SetAllActive(true);
        private void SetAllActive(bool flg)
        {
            foreach (var t in activedTransforms)
            {
                if (t != this.transform)
                {
                    t.gameObject.SetActive(flg);
                }
            }
        }
    }
}
