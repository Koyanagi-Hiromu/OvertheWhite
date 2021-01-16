using System;
using Sirenix.OdinInspector;

namespace SR
{
    public class ODButtonSO : SploveScriptableObject
    {
        public string memo;

        [HideLabel]
        public ODButtonSOs.Anim Anim;
    }

    namespace ODButtonSOs
    {
        [Serializable]
        public class Anim
        {
            [BoxGroup("Enter", false), ToggleLeft]
            [LabelText("リストに登録されてなくてもEnterアニメをする?")]
            public bool hasEterAnim = true;

            [BoxGroup("Enter", false)]
            [LabelText("EnterやExitアニメの秒数")]
            [MinValue(0.001f)]
            public float animEnterExitSec = 0.05f;

            [BoxGroup("個別設定", false), ToggleLeft]
            public bool isNoOverride = true;

            [BoxGroup("個別設定", false)]
            [HideLabel, DisableIf("isNoOverride")]
            public ButtonAnimation animatorInfo;
        }
    }
}
