using UnityEngine.SceneManagement;
using UnityEditor;
using SR;

namespace SREditor
{
    public class CameraResizeSwitcher : EditorWindow
    {
        [MenuItem("Tools/Camera Resize Switcher/Set Resize")]
        private static void SetLimit()
        {
            foreach (var gameObject in SceneManager.GetActiveScene().GetRootGameObjects())
            {
                foreach (var cameraResizer in gameObject.GetComponentsInChildren<CameraResizer>())
                {
                    cameraResizer.SetResize();
                }
            }
        }

        [MenuItem("Tools/Camera Resize Switcher/Clear Resize")]
        private static void ClearResize()
        {
            foreach (var gameObject in SceneManager.GetActiveScene().GetRootGameObjects())
            {
                foreach (var cameraResizer in gameObject.GetComponentsInChildren<CameraResizer>())
                {
                    cameraResizer.ClearResize();
                }
            }
        }
    }
}
