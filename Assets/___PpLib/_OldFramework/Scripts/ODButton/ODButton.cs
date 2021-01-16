using System.Collections;
using Sirenix.OdinInspector;
using SR.ODButtonSOs;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Serialization;
using UnityEngine.UI;
using System;
using DG.Tweening;

namespace SR
{
    public class ODButton : SploveBehaviour,
        IPointerClickHandler,
        IPointerDownHandler, IPointerUpHandler,
        IPointerEnterHandler, IPointerExitHandler
    {
        public static readonly int EnterKey = Animator.StringToHash("Enter");
        public static readonly int ExitKey = Animator.StringToHash("Exit");
        public static readonly int ToggledKey = Animator.StringToHash("Toggled");
        public static readonly int UntoggledKey = Animator.StringToHash("Untoggled");
        public static readonly int Multi_Enter_Exit = Animator.StringToHash("Multi_Enter_Exit");

        public static readonly int PointerEnter = Animator.StringToHash("PointerEnter");
        public static readonly int PointerDown = Animator.StringToHash("PointerDown");
        public static readonly int PointerCancel = Animator.StringToHash("PointerCancel");
        public static readonly int PointerClick = Animator.StringToHash("PointerClick");
        public static readonly int PointerLongHold = Animator.StringToHash("PointerLongHold");

        public static readonly int PointerEnterInToggle = Animator.StringToHash("PointerEnterInToggle");
        public static readonly int PointerDownInToggle = Animator.StringToHash("PointerDownInToggle");
        public static readonly int PointerClickInToggle = Animator.StringToHash("PointerClickInToggle");

        public static readonly int ON_Enable = Animator.StringToHash("ON_Enable");
        public static readonly int OFF_Disable = Animator.StringToHash("OFF_Disable");


        [BoxGroup("◆◆設定◆◆", centerLabel: true), LabelText("Tint遷移時間"), Range(0, 0.5f)]
        public float tintDuration = 0.1f;

        [BoxGroup("◆◆設定◆◆"), LabelText("Tint情報"), PropertyOrder(-255)]
        public TintData[] tintData;

        [BoxGroup("◆◆設定◆◆"), ToggleLeft, LabelText("押せない状態にする？(Unlockメソッドで解除できます)")]
        [SerializeField] bool lockInteractable = false;

        [BoxGroup("◆◆設定◆◆"), LabelText("Enter/Exitアニメの秒数"), Range(0.001f, 0.5f)]
        public float animEnterExitSec = 0.3f;

        [BoxGroup("◆◆設定◆◆"), LabelText("効果音設定")]
        public ODButtonSESO baseSE;

        [BoxGroup("◆◆上書き◆◆", centerLabel: true), LabelText("効果音を上書きする？"), ToggleLeft]
        public bool isOverrideSE;

        [BoxGroup("◆◆上書き◆◆", centerLabel: true), HideLabel, ShowIf("isOverrideSE")]
        public SE overrideSE;
        SE OverrideSE => isOverrideSE ? overrideSE : default(SE);
        bool hasOverrided;

        [BoxGroup("◆◆Toggle◆◆", centerLabel: true), ShowInInspector, DisplayAsString, LabelText("トグルモード"), PropertyOrder(-1)]
        bool IsToggleMode => owner != null && owner.isToggle;

        [BoxGroup("◆◆Toggle◆◆", centerLabel: true), EnableIf("IsToggleMode"), LabelText("トグル状態と一致"), SuffixLabel("[v]画像とか")]
        public GameObject activeSameAsToggle;

        [BoxGroup("◆◆Toggle◆◆", centerLabel: true), EnableIf("IsToggleMode"), LabelText("トグル状態と反対")]
        public GameObject activeSameAsUntoggle;

        [FoldoutGroup("情報", false)]
        [BoxGroup("情報/Old"), SuffixLabel("アニメ設定"), FormerlySerializedAs("so"), InfoBox("使用しないならNoneにしてください")]
        public ODButtonSO baseAnim;

        [BoxGroup("情報/Reference")]
        public Animator _Animator;

        [BoxGroup("情報/Reference")]
        public ButtonInfoHolder _ButtonInfoHolder;
        ButtonInfo info => _ButtonInfoHolder.info;

        [InfoBox("設定忘れ？", InfoMessageType.Warning, "NotExistsOwner"), BoxGroup("情報/Reference"), PropertyOrder(1)]
        public ODButtonList owner;

        bool _isToggleON;
        [BoxGroup("情報/実行時データ"), DisplayAsString, ShowInInspector]
        internal bool isToggleON
        {
            get => _isToggleON;
            set
            {
                _isToggleON = value;
                if (activeSameAsToggle)
                {
                    activeSameAsToggle.SetActive(value);
                }
                if (activeSameAsUntoggle)
                {
                    activeSameAsUntoggle.SetActive(!value);
                }
            }
        }

        [BoxGroup("情報/実行時データ"), DisplayAsString, ShowInInspector] bool buttonInteractable = true;

        public bool ButtonInteractable
        {
            set
            {
                if (lockInteractable) return;
                if (buttonInteractable == value) return;

                buttonInteractable = value;
                if (value)
                {
                    _Animator.Play(ON_Enable, 1);
                    TintData.TintNormal(this);
                }
                else
                {
                    _Animator.Play(OFF_Disable, 1);
                    TintData.TintDisabled(this);
                    CancelLongHold();
                }
            }
        }
        public void SetEnable(bool flg) => ButtonInteractable = flg;

        private void Awake()
        {
            TrySetOverride();
            foreach (var d in tintData)
            {
                d.Init(tintDuration);
            }

            if (IsToggleMode)
            {
                isToggleON = false;
            }
        }

        void TrySetOverride()
        {
            if (!hasOverrided && baseAnim != null && !baseAnim.Anim.isNoOverride)
            {
                baseAnim.Anim.animatorInfo.FSetOverride(_Animator);
            }
            hasOverrided = true;
        }

        void OnEnable() => pOnEnable();
        void pOnEnable()
        {
            if (baseAnim != null)
                _Animator.SetFloat(Multi_Enter_Exit, 1f / baseAnim.Anim.animEnterExitSec);
            else
                _Animator.SetFloat(Multi_Enter_Exit, 1f / animEnterExitSec);

            if (lockInteractable)
            {
                buttonInteractable = false;
                _Animator.Play(OFF_Disable, 1);
                TintData.TintDisabled(this);
            }
            else
            {
                if (IsToggleMode && !isToggleON)
                    TintData.TintUntoggled(this);
                else
                    TintData.TintNormal(this);
            }

            TryEnter();
        }


        void TryEnter()
        {
            if (owner != null) return;
            if (baseAnim != null && !baseAnim.Anim.hasEterAnim) return;

            Enter();
        }

        bool NotExistsOwner()
        {
            if (owner != null) return false;

            var refer = this.gameObject;
            while (refer != null)
            {
                if (refer.GetComponent<ODButtonList>())
                {
                    return true;
                }
                refer = refer.GetParent();
            }
            return false;
        }

        public void Lock()
        {
            lockInteractable = true;
            buttonInteractable = false;
            pOnEnable();
        }

        public void Unlock()
        {
            if (lockInteractable)
            {
                var prev = lockInteractable;
                lockInteractable = false;
                ButtonInteractable = true;
                lockInteractable = prev;
            }
        }

        public void ForciblyUnlock()
        {
            if (lockInteractable)
            {
                lockInteractable = false;
                ButtonInteractable = true;
            }
        }

        public void Toggle_選択して他を非選択にする()
        {
            TrySetOwner();
            if (owner != null)
                owner.Toggle(this);
            else
                Assert.UnReachable("Toggleには親に ODButtonList が必要です。");
        }

        private void TrySetOwner()
        {
            if (owner == null)
            {
                var refer = this.gameObject;
                while (owner == null && refer != null)
                {
                    owner = refer.GetComponent<ODButtonList>();
                    refer = refer.GetParent();
                }
            }
        }

        public void Select_AnimOnly()
        {
            if (IsToggleMode)
            {
                _Animator.Play(ToggledKey, 1);
                TintData.TintNormal(this);
            }
            PointerTrigger(PointerClick);
        }

        public void Select()
        {
            if (IsToggleMode)
            {
                _Animator.Play(ToggledKey, 1);
                TintData.TintNormal(this);
            }
            PointerTrigger(PointerClick);
            info.Select(this);
        }

        public void Select_LongHold()
        {
            PointerTrigger(PointerLongHold);
            info.Select_LongHold(this);
        }

        public void Select_InSelect()
        {
            PointerTrigger(PointerClickInToggle);
            info.SelectInSelect(this);
        }

        public void Unselect()
        {
            if (IsToggleMode && buttonInteractable)
            {
                _Animator.Play(UntoggledKey, 1);
                TintData.TintUntoggled(this);
            }
            PointerTrigger(PointerCancel);
            info.Unselect(this);
        }

        internal void Enter()
        {
            TrySetOverride();
            baseSE.Play(Key.ButtonEnterSE, OverrideSE);
            _Animator.Play(EnterKey, 0);
        }

        /// <summary>
        /// 外部イベントから呼ばれたりする
        /// </summary>
        internal void Exit()
        {
            baseSE.Play(Key.ButtonExitSE, OverrideSE);
            _Animator.Play(ExitKey, 0);
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            if (buttonInteractable)
            {
                if (!isToggleON)
                {
                    if (eventData.pointerPress == this.gameObject)
                    {
                        OnPointerDown(eventData);
                    }
                    else
                    {
                        baseSE.Play(Key.PointerEnterSE, OverrideSE);
                        TintData.TintHover(this);
                        PointerTrigger(PointerEnter);
                    }
                }
                else if (info.canClickInSelect)
                {
                    baseSE.Play(Key.PointerEnterSE, OverrideSE);
                    TintData.TintHover(this);
                    PointerTrigger(PointerEnterInToggle);
                }
            }
        }

        Coroutine longHold;
        public void OnPointerDown(PointerEventData eventData)
        {
            if (buttonInteractable)
            {
                if (!isToggleON || info.canClickInSelect)
                {
                    baseSE.Play(Key.PointerDownSE, OverrideSE);
                    TintData.TintPressed(this);

                    if (isToggleON)
                        PointerTrigger(PointerDownInToggle);
                    else
                        PointerTrigger(PointerDown);

                    if (info.isLongHold && longHold == null)
                    {
                        longHold = this.StartCoroutine(ErLongHold());
                    }
                }
            }
        }

        IEnumerator ErLongHold()
        {
            yield return new WaitForSeconds(ButtonInfo.longHoldSec);

            if (buttonInteractable)
            {
                if (owner != null && owner.isToggle)
                    owner.Toggle_LongHold(this);
                else
                    Select_LongHold();
            }
            longHold = null;
        }

        public void CancelLongHold()
        {
            if (longHold != null)
            {
                this.StopCoroutine(longHold);
                longHold = null;
            }
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            if (buttonInteractable)
            {
                if (isToggleON)
                {
                    if (info.canClickInSelect)
                    {
                        Select_InSelect();
                    }
                }
                else if (longHold == null)
                {
                    OnClick();
                }
            }
        }

        private void OnClick()
        {
            baseSE.Play(Key.ButtonPushSE, OverrideSE);
            if (owner != null && owner.isToggle)
                owner.Toggle(this);
            else
                Select();
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            if (buttonInteractable)
            {
                if (isToggleON)
                {
                    PointerTrigger(PointerClick);
                    TintData.TintNormal(this);
                }
                else
                {
                    PointerTrigger(PointerCancel);
                    if (IsToggleMode)
                        TintData.TintUntoggled(this);
                    else
                        TintData.TintNormal(this);
                }
            }
            CancelLongHold();
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            if (buttonInteractable)
            {
                if (!isToggleON || info.canClickInSelect)
                {
                    baseSE.Play(Key.PointerExitSE, OverrideSE);
                    if (!isToggleON)
                    {
                        PointerTrigger(PointerCancel);
                        if (IsToggleMode)
                            TintData.TintUntoggled(this);
                        else
                            TintData.TintNormal(this);
                    }
                    else
                    {
                        TintData.TintNormal(this);
                    }
                }
            }
        }

        private void PointerTrigger(int key) => _Animator.Play(key, 2);

#if UNITY_EDITOR
        [FoldoutGroup("情報"), Button("オート情報", ButtonSizes.Large)]
        public void AddActivator()
        {
            if (baseSE == null)
            {
                baseSE = NonResources.Load<ODButtonSESO>("Assets/_SR/_Common/OD_ButtonAssets/btnSeSO_Default.asset");
            }

            if (baseAnim == null)
            {
                baseAnim = NonResources.Load<ODButtonSO>("Assets/_SR/_Common/OD_ButtonAssets/btnAnimSO_Common.asset");
            }

            if (_Animator == null)
            {
                _Animator = gameObject.transform.GetChild(0).GetComponent<Animator>();
            }

            if (_ButtonInfoHolder == null)
            {
                _ButtonInfoHolder = gameObject.GetComponent<ButtonInfoHolder>();

                if (_ButtonInfoHolder == null)
                {
                    _ButtonInfoHolder = gameObject.AddComponent<ButtonInfoHolder>();
                }
            }
            TrySetOwner();
        }
#endif

    }

    public enum ButtonTrigger
    {
        Show_現れる,
        Hide_隠れる,
        Select_選択,
        Unselect_非選択,
    }
}
