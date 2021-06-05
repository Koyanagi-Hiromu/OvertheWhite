using DG.Tweening;
using TMPro;
using UnityEngine;

namespace PPD
{
    public class DOCharacterSpeech : DOPhaseComponent
    {
        [TextArea] public string text;
        public Character character;
        public Color color = Color.white;
        [Min(0)] public int delayCountFor1stChar = 2;
        public float eachDelay = 0.1f;
        public float fadeDuration = 0.2f;
        public float shakeStrength;
        public float shakeDuration;
        public TextMeshPro textMesh => character.speechBabble.textMesh;
        DOTweenTMPAnimator animator;
        public override void EditorTransition()
        {
            textMesh.text = text;
            textMesh.color = color;
        }

        private void Awake()
        {
            animator = new DOTweenTMPAnimator(textMesh);
        }

        public override void OnUnityDisable()
        {
            animator.Dispose();

            character.speechBabble.babbleImage.DOKill();
            character.speechBabble.textMesh.DOKill();

            character.speechBabble.babbleImage.DOColor(Color.clear, 0.2f);
            character.speechBabble.textMesh.DOColor(Color.clear, 0.2f);
        }

        public override void OnUnityEnable()
        {
            character.speechBabble.babbleImage.DOKill();
            character.speechBabble.textMesh.DOKill();

            character.speechBabble.babbleImage.color = Color.white;

            textMesh.text = text;
            var clearColor = color;
            clearColor.a = 0;
            textMesh.color = clearColor;

            animator.Refresh();
            if (shakeStrength > 0 && shakeDuration > 0)
            {
                for (int i = 0; i < animator.textInfo.characterCount; i++)
                {
                    animator.DOFadeChar(i, 1, fadeDuration).SetDelay((i + delayCountFor1stChar) * eachDelay);
                    animator.DOShakeCharOffset(i, shakeDuration, shakeStrength, fadeOut: false).SetEase(Ease.Linear).SetLoops(-1);
                }
            }
            else
            {
                for (int i = 0; i < animator.textInfo.characterCount; i++)
                {
                    animator.DOFadeChar(i, 1, fadeDuration).SetDelay((i + delayCountFor1stChar) * eachDelay);
                }
            }
        }
    }
}
