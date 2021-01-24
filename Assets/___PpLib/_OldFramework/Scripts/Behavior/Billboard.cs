using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

namespace SR
{
    public class Billboard : MonoBehaviour
    {
        [SerializeField, Title("完全にカメラの方向を向きます（通常はY軸無視）")] private bool unholdY;
        public bool findMainCameraEachUpdate;
        // [SerializeField] private bool invalidParentScale;
        private Transform m_CameraTransform;
        Transform tCamera => findMainCameraEachUpdate ? Camera.main.transform : m_CameraTransform;
        private new Transform transform;
        // private Vector3 defaultLocalScale;

        private void Start()
        {
            Init();
        }

        [Button]
        private void LookInInspector()
        {
            Init();
            LookAtMainCamera();
        }

        private void Init()
        {
            transform = base.transform;
        }

        void LateUpdate() => LookAtMainCamera();

        private void LookAtMainCamera()
        {
            if (m_CameraTransform == null)
            {
                m_CameraTransform = Camera.main.transform;
            }

            if (unholdY)
            {
                this.transform.LookAt(tCamera);
            }
            else
            {
                this.transform.LookAt(
                    transform.position + tCamera.rotation * Vector3.forward,
                    tCamera.rotation * Vector3.up
                );
            }
        }
    }
}