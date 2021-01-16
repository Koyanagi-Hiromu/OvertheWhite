using System;
using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;

namespace SR
{
    public class TextMeshProHandler : MonoBehaviour
    {
        [Title("TextMeshProの子供をここにドラッグアンドドロップ")]
        [SerializeField]
        private TMP_Text[] textMeshArray;

        [SerializeField]
        private Animator animator;
        public Animator Animator { get { return animator; } }

        [Title("ボタンを押すとサイズが一括で変更されます（押されない限り変更されません）"), InlineButton("Refresh")]
        public int fontSize = 36;

        public string text
        {
            set
            {
                textMeshArray.ForEach(text => text.text = value);
            }
        }

        public TMP_Text Get(int i)
        {
            return textMeshArray[i];
        }
        
        public void Refresh()
        {
            textMeshArray.ForEach(text => text.fontSize = fontSize);
        }

        public void SetActive(int index)
        {
            textMeshArray.ForEach(e => e.gameObject.SetActive(false));
            textMeshArray[index].gameObject.SetActive(true);
        }
    }
}
