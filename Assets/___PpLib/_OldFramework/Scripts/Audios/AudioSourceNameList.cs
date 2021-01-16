using UnityEngine;
using System.Collections.Generic;
using Sirenix.OdinInspector;

namespace SR
{
#if UNITY_5_3_OR_NEWER
    [CreateAssetMenu(menuName = "Splove/AudioSourceNameList/AudioSourceNameList")]
#endif
    public class AudioSourceNameList : SploveScriptableObject
    {
        [SerializeField]
        public Dictionary<string, string> descMap;
    }
}
