using DG.Tweening;

namespace PPD
{

    public class DOCameraMoveHere : DOPhaseComponent
    {
        public new PD_Camera camera;
        public float moveDuration;

        public override void EditorTransition()
        {
            camera.transform.position = transform.position;
            camera.transform.localRotation = transform.localRotation;
        }

        public override void OnUnityDisable() { }

        public override void OnUnityEnable()
        {
            camera.transform.DOMove(transform.position, moveDuration);
            camera.transform.DORotateQuaternion(transform.localRotation, moveDuration);
        }
    }
}
