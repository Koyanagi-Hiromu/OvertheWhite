using TMPro;
using UnityEngine;

namespace PPD
{
    public class DebugTextManager : SingletonMonoBehaviour<DebugTextManager>
    {
        [SerializeField] TextMeshProUGUI[] texts;

        protected override void UnityAwake()
        {
#if !UNITY_EDITOR
            this.SetActive(false);
#endif
        }

        public static void SetText(int index, string text)
        {
            if (Ins != null)
            {
                Ins.texts[index].text = text;
            }
        }

        public string this[int i]
        {
            get => texts[i].text;
            set => texts[i].text = value;
        }
    }
}