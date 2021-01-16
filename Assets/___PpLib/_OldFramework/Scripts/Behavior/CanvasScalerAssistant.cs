using Sirenix.OdinInspector;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

namespace SR
{
    public class CanvasScalerAssistant : AssistantBehaviour<CanvasScaler>
    {
        [SerializeField] private CanvasScalerObject so;
        // [SerializeField] private SO_CanvasOverlay overlay;

        protected void Awake() => Set();

        [Button]
        public void Set()
        {
            owner.uiScaleMode = CanvasScaler.ScaleMode.ScaleWithScreenSize;
            owner.screenMatchMode = CanvasScaler.ScreenMatchMode.MatchWidthOrHeight;
            owner.matchWidthOrHeight = so.matchWidthOrHeight;
            owner.referencePixelsPerUnit = so.referencePixelsPerUnit;
            owner.referenceResolution = so.referenceResolution;

            // if (overlay != null)
            // {
            //     var c = GetComponent<Canvas>();
            //     c.sortingOrder = overlay.sortOrder;
            // }
        }
    }
}
