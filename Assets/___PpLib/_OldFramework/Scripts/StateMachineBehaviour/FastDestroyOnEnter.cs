using UnityEngine;

namespace SR.StateMachine
{
    public class FastDestroyOnEnter : StateMachineBehaviour
    {
        override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            animator.FastDestroy_ExByIFI();
        }
    }
}