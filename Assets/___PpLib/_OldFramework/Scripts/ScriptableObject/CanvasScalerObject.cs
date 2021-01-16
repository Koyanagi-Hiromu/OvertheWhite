using UnityEngine;

namespace SR
{
    public class CanvasScalerObject : SploveScriptableObject
    {
        [SerializeField] internal Vector2 referenceResolution;
        [SerializeField] internal float matchWidthOrHeight;
        [SerializeField] internal float referencePixelsPerUnit;
    }
}
