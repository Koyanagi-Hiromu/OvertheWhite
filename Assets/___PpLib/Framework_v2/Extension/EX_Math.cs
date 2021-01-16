using UnityEngine;

namespace PPD
{
    public static class EX_Math
    {
        public static float Abs(in this int value) => Mathf.Abs(value);
        public static float Abs(in this float value) => Mathf.Abs(value);
        public static int AsInt(in this bool value) => value ? 1 : -1;
        public static int Sign(in this bool value) => value ? 1 : -1;
        public static int Sign(in this int value) => (value == 0) ? 0 : (value > 0 ? 1 : -1);
        public static int Sign(in this float value) => (value == 0) ? 0 : (value > 0 ? 1 : -1);
        public static bool IsSameSign(in this int a, int b)
         => (a > 0 && b > 0) || (a < 0 && b < 0) || (a == 0 && b == 0);
        public static float Clamp01(in this float value) => Mathf.Clamp01(value);
        public static float ClampM1_P1(in this float value) => Mathf.Clamp(value, -1, 1);
        public static float Clamp(in this float value, float min, float max) => Mathf.Clamp(value, min, max);
    }
}
