using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using SR.Nite;
using UnityEngine;

namespace SR
{
    public abstract class SMBSound : StateMachineBehaviour
    {
        public CustomAudioSource audioSourceKey;

        [Title("再生中にも再生する（音が重なる）")]
        public bool playEvenWhilePlaying = true;
    }
}
