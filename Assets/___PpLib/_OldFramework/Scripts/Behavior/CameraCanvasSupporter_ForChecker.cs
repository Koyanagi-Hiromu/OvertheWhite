using UnityEngine;

namespace SR
{
    [RequireComponent(typeof(Canvas))]
    public class CameraCanvasSupporter_ForChecker : MonoBehaviour
    {
        private bool enabledOnAwake;
        private Canvas canvas;
        private void Awake()
        {
            canvas = GetComponent<Canvas>();
            enabledOnAwake = canvas.enabled;
            canvas.enabled = false;
        }

        private void Start()
        {
            SetMainCamera();
        }

        public void SetMainCamera()
        {
            this.DelayCall_1F(Set);
        }

        private void Set()
        {
            canvas.enabled = enabledOnAwake;
            canvas.worldCamera = Camera.main;
        }
    }
}
