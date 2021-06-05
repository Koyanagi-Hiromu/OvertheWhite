using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
namespace PPD
{
    public class FingerUI : SingletonMonoBehaviour<FingerUI>
    {
        public Transform viewTf;
        public Transform fingerTf;
        public Image circle;
        public Image finger;

        bool hide = false;
        Color init;
        Color clear = new Color(1, 1, 1, 0);

        protected override void UnityAwake()
        {
            init = finger.color;
        }

        void Update()
        {
            if (hide) return;

            float targetR = 0f;
            float targetA = 0f;
            if (Input.GetMouseButton(0))
            {
                targetR = 30;
                targetA = 0.5f;
            }

            fingerTf.rotation = Quaternion.Lerp(fingerTf.rotation, Quaternion.Euler(0, 0, targetR), 0.2f);

            var color = circle.color;
            color.a = Mathf.Lerp(color.a, targetA, 0.1f);
            circle.color = color;

            viewTf.position = Input.mousePosition;
        }

        public void FadeOut()
        {
            StartCoroutine(FadeOutAnim());
        }

        IEnumerator FadeOutAnim()
        {
            hide = true;

            float t = 0;
            while (t < 1)
            {
                finger.color = Color.Lerp(init, clear, t);
                t += Time.deltaTime;
                yield return null;
            }
            finger.color = clear;
        }
    }
}