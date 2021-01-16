using UnityEngine;

namespace SR
{
    public class SoundOnStateEnter : SMBSound
    {
        override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            audioSourceKey.Play(!playEvenWhilePlaying);
        }
    }
}
