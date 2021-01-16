using UnityEngine;
using Sirenix.OdinInspector;

namespace SR
{
    public class AnimationCurveSO_Color : SploveScriptableObject
    {
        public AnimationCurve animationCurve;
        [ReadOnly] public float duration = 1f;
        [SerializeField] Color _color;
        public Color color => _color;

        [Button(ButtonSizes.Gigantic)]
        void CalcDuration()
        {
            var max = -1f;
            foreach (var key in animationCurve.keys)
            {
                max = Mathf.Max(key.time, max);
            }
            duration = max;
        }

        public Color GetColor(float lapse)
        {
            var value = animationCurve.Evaluate(lapse);
            var c = color;
            c.a = value;
            return c;
        }
    }

    public class AnimationCurveColor_Creation
    {
        public AnimationCurveSO_Color so;
        float lapse;

        public AnimationCurveColor_Creation(AnimationCurveSO_Color so) => this.so = so;
        public void UpdateLapse(float deltaTime) => lapse += deltaTime;
        public bool IsEnd() => lapse > so.duration;
        public Color GetColor() => so.GetColor(lapse);
    }
}
