using System;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Events;
using PPD;

namespace SR
{
    public class ButtonInfoHolder : SploveBehaviour
    {
        [HideLabel]
        public ButtonInfo info;
    }

    [Serializable]
    public struct ButtonInfo
    {
        public const float longHoldSec = 1.0f;
        [BoxGroup("ボタンが押されたときの処理", centerLabel: true)]
        [LabelText("連打を可能にする(オフなら、押すともう押せない)"), ToggleLeft, PropertyOrder(-1), GUIColor(1, 0, 0)] public bool repeatable;

        [BoxGroup("ボタンが押されたときの処理")]
        [LabelText("クリック時のイベント")] public UnityEvent OnExecute;


        [BoxGroup("遅延処理", centerLabel: true)]
        [LabelText("遅延イベント(秒)"), MinValue(0)] public float delaySec;
        [BoxGroup("遅延処理")]
        [LabelText("クリックしたあと（遅延）秒後に呼ばれるイベント"), ShowIf("isDelay")]
        public UnityEvent OnDelayExecute;
        bool isDelay => delaySec > 0;

        [BoxGroup("ロングタッチ", centerLabel: true)]
        [LabelText("有効にする？"), ToggleLeft] public bool isLongHold;
        [BoxGroup("ロングタッチ")]
        [LabelText("ロングタッチ時のイベント"), ShowIf("isLongHold")] public UnityEvent OnLongHold;


        [BoxGroup("選択中クリック（半選択）", centerLabel: true)]
        [LabelText("有効にする？"), ToggleLeft] public bool canClickInSelect;
        [BoxGroup("選択中選択（半選択）")]
        [LabelText("選択中選択時のイベント"), ShowIf("canClickInSelect")] public UnityEvent OnSelectInSelect;

        [HideLabel] public Activator activator;
        public void Select(ODButton owner)
        {
            owner.ButtonInteractable = repeatable;
            activator.Execute(true);
            OnExecute.Invoke();
            if (OnDelayExecute.GetPersistentEventCount() > 0)
            {
                StEr.DelayCall(delaySec, OnDelayExecute.Invoke);
            }
        }

        public void Select_LongHold(ODButton owner)
        {
            owner.ButtonInteractable = repeatable;
            OnLongHold.Invoke();
        }

        public void SelectInSelect(ODButton owner)
        {
            owner.ButtonInteractable = repeatable;
            OnSelectInSelect.Invoke();
        }

        public void Unselect(ODButton owner)
        {
            owner.ButtonInteractable = true;
            activator.Execute(false);
        }
    }

    [Serializable]
    public struct Activator
    {
        [HorizontalGroup("Split", 0.5f)]
        [BoxGroup("Split/ON"), LabelText("GameObject")] public GameObject[] SetActive;
        [BoxGroup("Split/ON"), LabelText("Component")] public Component[] SetEnable;
        [BoxGroup("Split/OFF"), LabelText("GameObject")] public GameObject[] SetInactive;
        [BoxGroup("Split/OFF"), LabelText("Component")] public Component[] SetDisable;

        public void Execute(bool flg)
        {
            SetActive.ForEach(e => e.SetActive(flg));
            SetEnable.ForEach(e => e.SetEnable(flg));
            SetInactive.ForEach(e => e.SetActive(!flg));
            SetDisable.ForEach(e => e.SetEnable(!flg));
        }
    }
}
