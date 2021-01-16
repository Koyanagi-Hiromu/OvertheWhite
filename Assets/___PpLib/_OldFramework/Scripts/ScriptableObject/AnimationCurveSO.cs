using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

namespace SR
{
    public class AnimationCurveSO : SploveScriptableObject
    {
        public AnimationCurve animationCurve;
        [DisableInEditorMode, DisableInPlayMode] public bool isLinear;
    }
}
