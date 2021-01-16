using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;

namespace SR.Nite
{
    [ExecuteInEditMode]
    public abstract class CustomAudioSourceComponent : SploveBehaviour
    {
        public CustomAudioSource audioSourceKey;

        [FormerlySerializedAs("playIfNotPlaying")]
        [InfoBox("チェックを入れると、停止中にのみ再生する\n（同じ音が重なって鳴らない）")]
        public bool playOnlyIfStop;

        [FoldoutGroup("高度な設定", false)]
        public UnityEvent onPlay;
        private void OnEnable()
        {
            if (gameObject.GetActive() && enabled)
            {
                Play();
            }
        }

        public virtual void Play()
        {
            audioSourceKey.Play(playOnlyIfStop);
            onPlay.Invoke();
        }
    }
}
