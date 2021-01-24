using DG.Tweening;
using UnityEngine;

namespace PPD
{
    public class Transform0To1 : MonoBehaviour
    {
        public float duration;
        public Ease ease;
        public Transform start;
        public Transform end;
        Vector3 startPos;
        Quaternion startRot;
        Vector3 startScl;
        private void Reset()
        {
            start = this.transform;
        }

        void OnDisable()
        {
            start.localPosition = startPos;
            start.localRotation = startRot;
            start.localScale = startScl;
        }

        private void Awake()
        {
            startPos = start.localPosition;
            startRot = start.localRotation;
            startScl = start.localScale;
        }

        void OnEnable() => Begin();
        public void Begin()
        {
            start.DOMove(end.position, duration).SetEase(ease);
            start.DORotate(end.eulerAngles, duration).SetEase(ease);
            start.DOScale(end.localScale, duration).SetEase(ease);
        }
    }
}
