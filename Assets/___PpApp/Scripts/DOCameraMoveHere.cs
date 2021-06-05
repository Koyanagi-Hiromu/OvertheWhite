using System.Diagnostics;
using DG.Tweening;
using Sirenix.OdinInspector;

namespace PPD
{
    public class DOCameraMoveHere : DOPhaseComponent
    {
        public new PD_Camera camera;
        public float moveDuration;

        public override void Init()
        {
        }

        [Conditional("DEVELOPMENT_BUILD"), Conditional("UNITY_EDITOR")]
        [Button(ButtonSizes.Gigantic)]
        void EDITOR_ResetTransform()
        {
            // UnityEditor.Undo.RecordObject(transform, "ResetTransform");
            transform.position = camera.transform.position;
            transform.localRotation = camera.transform.localRotation;
        }

        public override void FlashMove()
        {
            camera.transform.position = transform.position;
            camera.transform.localRotation = transform.localRotation;
        }

        public override void DOKill() { }

        public override void DOStart()
        {
            camera.transform.DOMove(transform.position, moveDuration);
            camera.transform.DORotateQuaternion(transform.localRotation, moveDuration);
        }
    }
}
