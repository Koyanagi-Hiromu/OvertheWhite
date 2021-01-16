using UnityEngine;

namespace SR.StateMachine
{
    public class InactiveOnEnter : StateMachineBehaviour
    {
        public bool SetActiveFalse = true;
        public bool SetEnableFalse = true;
        override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            animator.SetEnable(!SetEnableFalse);
            animator.gameObject.SetActive(!SetActiveFalse);
        }
    }
}
