using DG.Tweening;

namespace PPD
{

    public class DOCameraMoveHere : PPD_MonoBehaviour, IDOPhaseComponent
    {
        public new PD_Camera camera;
        public float moveDuration;

        public void EditorTransition()
        {
            camera.transform.position = transform.position;
            camera.transform.localRotation = transform.localRotation;
        }

        public void OnEnable()
        {
            camera.transform.DOMove(transform.position, moveDuration);
            camera.transform.DORotateQuaternion(transform.localRotation, moveDuration);
        }
    }
}
