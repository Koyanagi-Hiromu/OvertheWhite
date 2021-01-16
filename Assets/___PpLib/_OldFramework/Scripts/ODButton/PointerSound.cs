using UnityEngine.EventSystems;

namespace SR
{
    public class PointerSound : SploveBehaviour,
    IPointerEnterHandler, IPointerDownHandler, IPointerExitHandler
    {
        public ODButtonSESO baseSE;

        public void OnPointerEnter(PointerEventData eventData)
        {
            baseSE.SE.ButtonEnterSE.Play();
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            baseSE.SE.PointerDownSE.Play();
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            baseSE.SE.ButtonExitSE.Play();
        }
    }
}
