using UnityEngine;

namespace SR.StateMachine
{
    public class KillMe : StateMachineBehaviour
    {
        override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            animator.DestroyInstance();
        }
    }
}