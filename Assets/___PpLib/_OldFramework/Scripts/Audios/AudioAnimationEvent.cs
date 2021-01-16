using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

namespace SR.Nite
{
    public class AudioAnimationEvent : SploveBehaviour
    {
        public void Play(string name)
        {
            Debug.Log("test");
            AudioManager.Ins.Play(name);
        }
    }
}
