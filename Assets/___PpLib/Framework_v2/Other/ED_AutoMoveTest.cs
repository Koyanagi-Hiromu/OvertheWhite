
using Sirenix.OdinInspector;
using UnityEngine;

namespace PPD
{
    [ExecuteInEditMode]
    public abstract class ED_AutoMoveTest : PPD_MonoBehaviour
    {
        public static bool isActiveOnEditor = false;
        [ShowInInspector]
        public bool IsActiveOnEditor { get => isActiveOnEditor; set => isActiveOnEditor = value; }

        private float lapse;
        protected abstract float Interval { get; }

        private void Update()
        {
            if (isActiveOnEditor || Application.isPlaying)
            {
                lapse += Time.deltaTime;
                UnityUpdate(Mathf.PingPong(lapse, Interval) / Interval);
            }
            else
            {
                UnityUpdate(1);
            }
        }

        protected abstract void UnityUpdate(float ratio);
    }
}