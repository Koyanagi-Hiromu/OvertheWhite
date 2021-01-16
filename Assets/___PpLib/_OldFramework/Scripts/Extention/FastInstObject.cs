using Sirenix.OdinInspector;
using UnityEngine;

namespace SR
{
    public class FastInstObject : SploveBehaviour, IFastInst
    {
        static Transform staticMainCamera;
        private Component _sourcePrefab;
        public Component SourcePf
        {
            get => _sourcePrefab;
            set
            {
                _sourcePrefab = value;
                if (autoDestroy_sec >= 0)
                {
                    this.DelayCall(autoDestroy_sec, FastDestroy);
                }
            }
        }

        [SerializeField, LabelText("自動削除（秒）"), SuffixLabel("-1で無効")]
        private float autoDestroy_sec = -1;
        [SerializeField, LabelText("生成時にカメラの方向に振り向く")]
        private bool billboardAtStart;
        void OnEnable()
        {
            if (billboardAtStart)
            {
                if (staticMainCamera == null)
                {
                    staticMainCamera = Camera.main.transform;
                }

                this.transform.LookAt(
                    transform.position + staticMainCamera.rotation * Vector3.forward,
                    staticMainCamera.rotation * Vector3.up
                );
            }
        }
        private void OnFastInst() => this.DelayCall(autoDestroy_sec, FastDestroy);
        public void FastDestroy() => this.FastDestroy_Ex();
    }
}

