using Sirenix.OdinInspector;
using UnityEngine;

namespace SR
{
    public abstract class SploveBehaviour : MonoBehaviour
    {
#if UNITY_EDITOR
        [HideInInlineEditors, Button("オートネーム"), PropertyOrder(int.MaxValue), PropertySpace(16)]
        void SetName_UNITY_EDITOR()
        {
            var next = GetName_UNITY_EDITOR();
            if (this.name != next)
            {
                this.name = next;
                UnityEditor.EditorUtility.SetDirty(this);
            }
        }

        protected virtual string GetName_UNITY_EDITOR()
        {
            var n = this.GetType().ToString();
            return n.Substring(n.LastIndexOf(".") + 1);
        }
#endif
    }
}
