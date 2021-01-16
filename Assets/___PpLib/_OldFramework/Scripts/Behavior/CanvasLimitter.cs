using UnityEngine;
using Sirenix.OdinInspector;

namespace SR
{
    public class CanvasLimitter : AssistantBehaviour<RectTransform>
    {
        public static bool writeLog = false;
        [SerializeField] private CanvasLimitMax limitMax;

        private void Awake() => SetLimit();

        [Button]
        public void SetLimit()
        {
            var widthCurrent = (float)Screen.width;
            var heightCurrent = (float)Screen.height;

#if UNITY_EDITOR
            if (writeLog)
            {
                SLog.System.Info("CanvasLimitter.SetLimit");
                SLog.System.Info($"Screen:{Screen.width}, {Screen.height}");
                SLog.System.Info($"UnityEditor.UnityStats.screenRes:{UnityEditor.UnityStats.screenRes}");
            }

            var stringValues = UnityEditor.UnityStats.screenRes.Split('x');

            widthCurrent = float.Parse(stringValues[0]);
            heightCurrent = float.Parse(stringValues[1]);
#endif

            var heightLimit = (float)widthCurrent / 9f * 16f;

            var limit = (heightCurrent - heightLimit) / 2f;

            if (limit < 0)
            {
                limit = 0f;
            }

            var limitTop = limit;
            var limitBottom = limit;

            if (this.limitMax != null)
            {
                if (this.limitMax.top >= 0 && limitTop > this.limitMax.top)
                {
                    limitTop = this.limitMax.top;
                }
                if (this.limitMax.bottom >= 0 && limitBottom > this.limitMax.bottom)
                {
                    limitBottom = this.limitMax.bottom;
                }
            }

            this.owner.offsetMin = new Vector2(0f, limitTop);
            this.owner.offsetMax = new Vector2(0f, -limitBottom);
        }

        [Button]
        public void ClearLimit()
        {
            SLog.System.Info("CanvasLimitter.ClearLimit");

            this.owner.offsetMin = new Vector2(0f, 0f);
            this.owner.offsetMax = new Vector2(0f, 0f);
        }
    }
}
