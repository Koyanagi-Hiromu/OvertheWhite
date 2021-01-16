using System;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

namespace SR
{
    [Serializable, LabelWidth(100f)]
    public struct ButtonAnimation
    {
        public AnimationClip Life_Hide;
        public AnimationClip Life_Enter;
        public AnimationClip Life_Idle;
        public AnimationClip Life_Exit;
        public AnimationClip Life_ON_Enable;
        public AnimationClip Life_OFF_Disable;
        public AnimationClip Pointer_Reset;
        public AnimationClip Pointer_Enter;
        public AnimationClip Pointer_Down;
        public AnimationClip Pointer_Click;
        public AnimationClip Pointer_Cancel;
        public AnimationClip Pointer_LongHold;
        [BoxGroup("なくても大丈夫")] public AnimationClip Life_Toggled;
        [BoxGroup("なくても大丈夫")] public AnimationClip Life_Untoggled;

        public void FSetOverride(Animator animator)
        {
            var overrideController = new AnimatorOverrideController(animator.runtimeAnimatorController);
            overrideController.runtimeAnimatorController = animator.runtimeAnimatorController;
            animator.runtimeAnimatorController = overrideController;
            overrideController.ApplyOverrides(GetAnimationOverrideList());
        }

        public IList<KeyValuePair<AnimationClip, AnimationClip>> GetAnimationOverrideList()
        {
            // var BTN = Oc.OcResidentData.ButtonData;
            // var map = new List<KeyValuePair<AnimationClip, AnimationClip>>();
            // map.Add(Generate(BTN.Life_Enter, Life_Enter));
            // map.Add(Generate(BTN.Life_Exit, Life_Exit));
            // map.Add(Generate(BTN.Life_Hide, Life_Hide));
            // map.Add(Generate(BTN.Life_Idle, Life_Idle));
            // map.Add(Generate(BTN.Life_ON_Enable, Life_ON_Enable));
            // map.Add(Generate(BTN.Life_OFF_Disable, Life_OFF_Disable));
            // map.Add(Generate(BTN.Pointer_Reset, Pointer_Reset));
            // map.Add(Generate(BTN.Pointer_Enter, Pointer_Enter));
            // map.Add(Generate(BTN.Pointer_Down, Pointer_Down));
            // map.Add(Generate(BTN.Pointer_Click, Pointer_Click));
            // map.Add(Generate(BTN.Pointer_Cancel, Pointer_Cancel));
            // map.Add(Generate(BTN.Pointer_LongHold, Pointer_LongHold));
            // map.Add(Generate(BTN.Life_Toggled, Life_Toggled));
            // map.Add(Generate(BTN.Life_Untoggled, Life_Untoggled));
            // return map;
            return null;
        }

        private KeyValuePair<AnimationClip, AnimationClip> Generate(AnimationClip key, AnimationClip value)
        {
            if (value == null)
            {
                value = key;
            }
            return new KeyValuePair<AnimationClip, AnimationClip>(key, value);
        }
    }
}
