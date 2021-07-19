using Sirenix.OdinInspector;
using UnityEngine;

namespace PPD
{
    [TypeInfoBox("Localでマッチします。")]
    public class MatchTransform_LateUpdate : PPD_MonoBehaviour
    {
        [InfoBox("0がターゲットがこちらに来る。")]
        [Range(0, 1)] public float t_self_vs_other;
        public bool matchPosition = true;
        public bool matchRotation = true;
        public bool matchScale = true;
        public Transform target;
        new Transform transform;
        private void Awake()
        {
            this.transform = base.transform;
        }

        private void LateUpdate()
        {
            if (matchPosition)
            {
                var p = Vector3.Lerp(transform.localPosition, target.localPosition, t_self_vs_other);
                if (t_self_vs_other > 0)
                {
                    target.localPosition = p;
                }

                if (t_self_vs_other < 1)
                {
                    target.localPosition = p;
                }
            }

            if (matchRotation)
            {
                var r = Quaternion.Lerp(transform.localRotation, target.localRotation, t_self_vs_other);
                if (t_self_vs_other > 0)
                {
                    target.localRotation = r;
                }

                if (t_self_vs_other < 1)
                {
                    target.localRotation = r;
                }
            }

            if (matchScale)
            {
                var s = Vector3.Lerp(transform.localScale, target.localScale, t_self_vs_other);
                if (t_self_vs_other > 0)
                {
                    target.localScale = s;
                }

                if (t_self_vs_other < 1)
                {
                    target.localScale = s;
                }
            }
        }
    }
}

