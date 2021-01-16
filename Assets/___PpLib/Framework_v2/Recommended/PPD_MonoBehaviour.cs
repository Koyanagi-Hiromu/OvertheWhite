using Sirenix.OdinInspector;
using UnityEditor;
using UnityEngine;

namespace PPD
{
    public abstract class PPD_MonoBehaviour : MonoBehaviour
    {
        protected void Rename(bool undo = false)
        {
#if UNITY_EDITOR
            if (undo)
            {
                Undo.RecordObject(gameObject, "名前変更");
            }
#endif
            this.name = GetName_UNITY_EDITOR(this);
        }

        protected string GetName_UNITY_EDITOR(Component c)
        {
            var n = c.GetType().ToString();
            return n.Substring(n.LastIndexOf(".") + 1);
        }
    }
}
