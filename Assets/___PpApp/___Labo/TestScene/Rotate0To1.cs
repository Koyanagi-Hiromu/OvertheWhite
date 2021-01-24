using DG.Tweening;
using UnityEngine;

namespace PPD
{
    public class Rotate0To1 : MonoBehaviour
    {
        public float duration;
        public Ease ease;
        public Vector3 startEulerAngles = Vector3.zero;
        public Vector3 endEulerAngles = Vector3.one;
        void OnDisable() => this.transform.localEulerAngles = startEulerAngles;
        void OnEnable() => Begin();
        public void Begin()
        {
            this.transform.localEulerAngles = startEulerAngles;
            this.transform.DOLocalRotate(endEulerAngles, duration).SetEase(ease);
        }
    }
}
