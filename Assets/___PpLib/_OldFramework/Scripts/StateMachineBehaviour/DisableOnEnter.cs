using UnityEngine;

namespace SR.StateMachine
{
    public class DisableOnEnter : StateMachineBehaviour
    {
        override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            animator.SetEnable(false);
        }
    }
}