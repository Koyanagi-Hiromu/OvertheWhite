using System;
using DG.Tweening;
using DG.Tweening.Core;
using DG.Tweening.Plugins.Options;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.UI;

namespace SR
{
    public class ODButton_TintSO : SploveScriptableObject
    {
        [BoxGroup("Tint"), LabelText("default (通常色＋Toggleで選択中)")] public Color defaultColor = Color.white;
        [BoxGroup("Tint"), LabelText("hover (押さずに乗せてる間)")] public Color hoverColor = new Color(.9f, .9f, .9f);
        [BoxGroup("Tint"), LabelText("pressed (押してる最中)")] public Color pressedColor = new Color(.6f, .6f, .6f);
        [BoxGroup("Tint"), LabelText("untoggled (Toggleで選択外)")] public Color untoggledColor = new Color(.8f, .8f, .8f);
        [BoxGroup("Tint"), LabelText("Disabled (無効時)")] public Color disabledColor = Color.black;
        [BoxGroup("Tint/選択中選択(Toggle中にもう一度押す)"), LabelText("hover2")] public Color hover2Color = new Color(.9f, .9f, .9f);
        [BoxGroup("Tint/選択中選択(Toggle中にもう一度押す)"), LabelText("pressed2")] public Color pressed2Color = new Color(.6f, .6f, .6f);
    }


    [Serializable]
    public class TintData
    {
        public Image tintTarget;
        public ODButton_TintSO so;
        TweenerCore<Color, Color, ColorOptions> tint;
        public void Init(float duration)
        {
            tint = tintTarget.DOColor(Color.white, duration).SetAutoKill(false);
        }

        void Tint(Color endColor)
        {
            tint.endValue = endColor;
            tint.startValue = tintTarget.color;
            tint.changeValue = tint.endValue - tint.startValue;
            tint.Restart();
        }

        public static void TintNormal(ODButton owner)
        {
            foreach (var d in owner.tintData)
            {
                d.Tint(d.so.defaultColor);
            }
        }

        public static void TintHover(ODButton owner)
        {
            if (owner.isToggleON)
            {
                foreach (var d in owner.tintData)
                {
                    d.Tint(d.so.hover2Color);
                }
            }
            else
            {
                foreach (var d in owner.tintData)
                {
                    d.Tint(d.so.hoverColor);
                }
            }
        }

        public static void TintPressed(ODButton owner)
        {
            if (owner.isToggleON)
            {
                foreach (var d in owner.tintData)
                {
                    d.Tint(d.so.pressed2Color);
                }
            }
            else
            {
                foreach (var d in owner.tintData)
                {
                    d.Tint(d.so.pressedColor);
                }
            }
        }

        public static void TintUntoggled(ODButton owner)
        {
            foreach (var d in owner.tintData)
            {
                d.Tint(d.so.untoggledColor);
            }
        }

        public static void TintDisabled(ODButton owner)
        {
            foreach (var d in owner.tintData)
            {
                d.Tint(d.so.disabledColor);
            }
        }
    }
}
