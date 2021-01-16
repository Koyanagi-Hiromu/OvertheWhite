using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SR;

namespace SR.Nite
{
    public class SploveBGM : CustomAudioSourceComponent
    {
        public float fadeTime = AudioManager.DEFAULT_FADING_TIME;

        private void OnDisable()
        {
            audioSourceKey.StopBgm(fadeTime);
        }

        public override void Play()
        {
            audioSourceKey.PlayBgm(fadeTime);
        }
    }
}
