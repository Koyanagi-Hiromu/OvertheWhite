using TMPro;
using DG.Tweening;
using UnityEngine;
using Sirenix.OdinInspector;

namespace PPD
{
    public class Speech : PPD_MonoBehaviour
    {
        public float eachDelay = 0.1f;
        public float fadeDuration = 0.2f;
        public float shakeStrength;
        public float shakeDuration;
        public TextMeshPro textMesh;
        [TextArea] public string text;

        public void OnEnable()
        {
            textMesh.text = text;
            textMesh.DOFade(0, 0);
            DOTweenTMPAnimator animator = new DOTweenTMPAnimator(textMesh);
            if (shakeStrength > 0 && shakeDuration > 0)
            {
                for (int i = 0; i < animator.textInfo.characterCount; i++)
                {
                    animator.DOFadeChar(i, 1, fadeDuration).SetDelay(i * eachDelay);
                    animator.DOShakeCharOffset(i, shakeDuration, shakeStrength, fadeOut: false).SetEase(Ease.Linear).SetLoops(-1);
                }
            }
            else
            {
                for (int i = 0; i < animator.textInfo.characterCount; i++)
                {
                    animator.DOFadeChar(i, 1, fadeDuration).SetDelay(i * eachDelay);
                }
            }
        }
    }
}
