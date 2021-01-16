using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace SR
{
    public class DetailTextHider : SploveBehaviour, IPointerDownHandler
    {
        private void OnDisable()
        {
#if SMARTPHONE_TEST || UNITY_IOS || UNITY_ANDROID
            // TODO:必要なら実装する
            // QuestDetailTextManager.Ins.Hide();
#endif
        }

        public virtual void OnPointerDown(PointerEventData eventData)
        {
#if SMARTPHONE_TEST || UNITY_IOS || UNITY_ANDROID
            // TODO:必要なら実装する
            // QuestDetailTextManager.Ins.Hide();
#endif
        }
    }
}
