using UnityEngine.SceneManagement;
using UnityEditor;
using SR;

namespace SREditor
{
    public class CanvasLimitSwitcher : EditorWindow
    {
        [MenuItem("Tools/Canvas Limit Switcher/Set Limit")]
        private static void SetLimit()
        {
            foreach (var gameObject in SceneManager.GetActiveScene().GetRootGameObjects())
            {
                foreach (var canvasLimitter in gameObject.GetComponentsInChildren<CanvasLimitter>())
                {
                    canvasLimitter.SetLimit();
                }
            }
        }

        [MenuItem("Tools/Canvas Limit Switcher/Clear Limit")]
        private static void ClearLimit()
        {
            foreach (var gameObject in SceneManager.GetActiveScene().GetRootGameObjects())
            {
                foreach (var canvasLimitter in gameObject.GetComponentsInChildren<CanvasLimitter>())
                {
                    canvasLimitter.ClearLimit();
                }
            }
        }
    }
}
