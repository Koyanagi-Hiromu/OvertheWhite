using System.Linq;
using UnityEngine;

namespace SR
{
    public static class AnimatorExtention
    {
        public static bool IsIdling(this Animator animator, params string[] triggers)
        {
            return animator.IsIdling(0, triggers);
        }

        public static bool IsIdling(this Animator animator, int layerIndex, params string[] triggers)
        {
            if (animator.IsActive())
            {
                return
                    triggers.All(trigger => animator.GetBool(trigger) == false) && 
                    animator.IsIdling(layerIndex);
            }
            else
            {
                return true;
            }
        }

        public static bool IsIdling(this Animator animator, string trigger, int layerIndex = 0)
        {
            if (animator.IsActive())
            {
                var noTrigger = animator.GetBool(trigger) == false;
                return noTrigger && animator.IsIdling(layerIndex);
            }
            else
            {
                return true;
            }
        }

        public static bool IsIdling(this Animator animator, int layerIndex = 0)
        {
            if (animator.IsActive())
            {
                {
                    var empty = animator.GetNextAnimatorClipInfo(layerIndex).Empty();
                    if (!empty) return false;
                }
                {
                    var current = animator.GetCurrentAnimatorStateInfo(layerIndex);
                    var idle = (current.IsTag("Idle") || current.IsName("Idle"));
                    if (!idle) return false;
                }
                return true;
            }
            else
            {
                return true;
            }
        }
    }
}
