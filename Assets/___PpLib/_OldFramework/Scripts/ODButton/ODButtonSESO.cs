using Sirenix.OdinInspector;
using SR.Nite;
using SR.ODButtonSOs;

namespace SR
{
    public class ODButtonSESO : SploveScriptableObject
    {
        public string memo;
        [HideLabel]
        public SE SE;
        public void Play(Key key, SE overrideSE)
        {
            SE.GetCAS(key, overrideSE).Play();
        }
    }

    namespace ODButtonSOs
    {
        public enum Key
        {
            ButtonPushSE, ButtonEnterSE, ButtonExitSE, PointerEnterSE, PointerDownSE, PointerExitSE,
        }

        [System.Serializable]
        public struct SE
        {
            [HideLabel, BoxGroup("ButtonPushSE")] public CustomAudioSource ButtonPushSE;
            [HideLabel, BoxGroup("ButtonEnterSE")] public CustomAudioSource ButtonEnterSE;
            [HideLabel, BoxGroup("ButtonExitSE")] public CustomAudioSource ButtonExitSE;
            [HideLabel, BoxGroup("PointerEnterSE")] public CustomAudioSource PointerEnterSE;
            [HideLabel, BoxGroup("PointerDownSE")] public CustomAudioSource PointerDownSE;
            [HideLabel, BoxGroup("PointerExitSE")] public CustomAudioSource PointerExitSE;

            public CustomAudioSource GetCAS(Key key, SE overrideSE)
            {
                var overrideCAS = overrideSE.Convert(key);
                if (!overrideCAS.keyName.IsNullOrEmpty())
                {
                    return overrideCAS;
                }

                return this.Convert(key);
            }

            public CustomAudioSource Convert(Key key)
            {
                switch (key)
                {
                    case Key.ButtonPushSE:
                        return ButtonPushSE;
                    case Key.ButtonEnterSE:
                        return ButtonEnterSE;
                    case Key.ButtonExitSE:
                        return ButtonExitSE;
                    case Key.PointerEnterSE:
                        return PointerEnterSE;
                    case Key.PointerDownSE:
                        return PointerDownSE;
                    case Key.PointerExitSE:
                        return PointerExitSE;
                    default:
                        return new CustomAudioSource();
                }
            }
        }
    }
}
