using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SR
{
    [RequireComponent(typeof(Animator))]
    public class AnimationEventHandler : SploveBehaviour
    {
        public event Action<string> onStart = delegate { };
        public event Action<string> onEnd = delegate { };
        public event Action<string> onEvent = delegate { };

        [SerializeField]
        private int triggerLength;
        private Animator animator;

        void Awake()
        {
            animator = GetComponent<Animator>();
        }

        public void OnStart(string name)
        {
            SLog.Animation.Info("OnStart: + " + name);
            onStart(name);
        }

        public void OnEnd(string name)
        {
            SLog.Animation.Info("OnEnd: + " + name);
            onEnd(name);
        }

        public void OnEvent(string name)
        {
            SLog.Animation.Info("OnEvent: + " + name);
            onEvent(name);
        }

        public void SetTrigger(int index = 0)
        {
            if (animator.IsActive())
            {
                animator.SetTrigger(index);
            }
        }

        public void SetTrigger(string key)
        {
            if (animator.IsActive())
            {
                animator.SetTrigger(key);
            }
        }

        public void SetInt(string key, int value)
        {
            if (animator.IsActive())
            {
                animator.SetInteger(key, value);
            }
        }

        public void SetInt(int id, int value)
        {
            if (animator.IsActive())
            {
                animator.SetInteger(id, value);
            }
        }

        public bool IsIdling()
        {
            return IsIdling(layerIndex: 0);
        }

        public bool IsIdling(int layerIndex)
        {
            if (animator.IsActive())
            {
                var current = animator.GetCurrentAnimatorStateInfo(layerIndex);
                return (current.IsTag("Idle") || current.IsName("Idle")) && animator.GetNextAnimatorClipInfo(layerIndex).Empty();
            }
            else
            {
                return true;
            }
        }
    }
}
