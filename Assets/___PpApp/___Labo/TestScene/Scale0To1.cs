using DG.Tweening;
using UnityEngine;

namespace PPD
{
    public class Scale0To1 : MonoBehaviour
    {
        public float duration;
        public Ease ease;
        public Vector3 startScale = Vector3.zero;
        public Vector3 endScale = Vector3.one;
        void OnDisable() => this.transform.localScale = startScale;
        void OnEnable() => Begin();
        public void Begin()
        {
            this.transform.localScale = startScale;
            this.transform.DOScale(endScale, duration).SetEase(ease);
        }
    }
}
