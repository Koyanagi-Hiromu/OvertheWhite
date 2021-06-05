using DG.Tweening;
using TMPro;
using UnityEngine;

namespace PPD
{
    public class SpeechBabble : PPD_MonoBehaviour
    {
        public SpriteRenderer babbleImage;
        public TextMeshPro textMesh;
        private void Awake()
        {
            SetClear();
        }

        public void SetClear()
        {
            babbleImage.DOKill();
            textMesh.DOKill();

            babbleImage.color = Color.clear;
            textMesh.color = Color.clear;
        }

        public void DOClear()
        {
            babbleImage.DOKill();
            textMesh.DOKill();

            babbleImage.DOColor(Color.clear, 0.2f);
            textMesh.DOColor(Color.clear, 0.2f);
        }
    }
}
