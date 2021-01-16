using Sirenix.OdinInspector;
using UnityEngine;

namespace PPD
{
    public class AutoCircularMove : PPD_MonoBehaviour
    {
        public bool canMoveByOther;
        [PropertyRange(-1, 1)] public float firstTimePosition;
        public float interval = 4;
        public Vector3 amplitude = new Vector3(1, 0, 0);

        float lapse;
        new Transform transform;
        Vector3 originPos;
        Vector3 lastPos;
        private void Start()
        {
            this.transform = base.transform;
            originPos = transform.localPosition;
            lapse += interval * firstTimePosition;
        }

        private void Update()
        {
            lapse += Time.deltaTime;
            var moveVec = amplitude * Mathf.Sin(2 * Mathf.PI * lapse / interval);

            if (canMoveByOther)
            {
                var diff = transform.localPosition - lastPos;
                originPos += diff;
            }

            transform.localPosition = originPos + moveVec;
            lastPos = transform.localPosition;
        }
    }
}
