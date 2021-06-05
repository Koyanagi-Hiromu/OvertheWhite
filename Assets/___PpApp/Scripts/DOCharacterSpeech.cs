using DG.Tweening;
using TMPro;
using UnityEngine;

namespace PPD
{
    public class DOCharacterSpeech : PPD_MonoBehaviour, IDOPhaseComponent
    {
        [TextArea] public string text;
        public Character character;
        public Color color = Color.white;
        public float eachDelay = 0.1f;
        public float fadeDuration = 0.2f;
        public float shakeStrength;
        public float shakeDuration;
        public TextMeshPro textMesh => character.speechBabble.textMesh;
        public void EditorTransition()
        {
            textMesh.text = text;
            textMesh.color = color;
        }

        private void OnDisable()
        {
            character.speechBabble.babbleImage.DOKill();
            character.speechBabble.textMesh.DOKill();

            character.speechBabble.babbleImage.DOColor(Color.clear, 0.2f);
            character.speechBabble.textMesh.DOColor(Color.clear, 0.2f);
        }

        //TODO: フェイズから呼んであげる。
        public void OnEnable()
        {
            character.speechBabble.babbleImage.DOKill();
            character.speechBabble.textMesh.DOKill();

            character.speechBabble.babbleImage.color = Color.white;

            textMesh.text = text;
            textMesh.color = color;

            textMesh.DOFade(0, 0).Complete();
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
