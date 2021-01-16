using UnityEngine;

namespace PPD
{
    public class ScrollElement : PPD_MonoBehaviour, IScrollElement
    {
        [SerializeField] RectTransform _rectTransform;
        int IScrollElement.id { get; set; }
        RectTransform IScrollElement.rectTransform => _rectTransform;
        IScrollElement IScrollElement.Inst(Transform owner) => this.Inst(owner);
    }

    public interface IScrollElement
    {
        int id { get; set; }
        RectTransform rectTransform { get; }
        IScrollElement Inst(Transform owner);
    }
}
