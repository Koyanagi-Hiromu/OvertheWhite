using Sirenix.OdinInspector;
using UnityEngine;

namespace SR
{
    // registee
    public class ManualSingletonResistee : SploveBehaviour
    {
        [InlineEditor]
        [SerializeField]
        private MSSO[] manualSingletonScriptableObjects;

        protected void Awake()
        {
            foreach (var msso in manualSingletonScriptableObjects)
            {
                msso.OnResister();
            }
        }
    }
}
