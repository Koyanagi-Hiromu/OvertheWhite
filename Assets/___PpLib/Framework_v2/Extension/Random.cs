using System;
using UnityEngine;

namespace PPD
{
    public static class Random
    {
        public static Quaternion rotation => UnityEngine.Random.rotation;
        public static Vector3 onUnitSphere => UnityEngine.Random.onUnitSphere;
        ///<summary>yが0以上の半球です</summary>
        public static Vector3 onUnitHalfSphere
        {
            get
            {
                var ret = UnityEngine.Random.onUnitSphere;
                return ret.ChangeY_In(ret.y.Abs());
            }
        }

        public static Vector2 insideUnitCircle => UnityEngine.Random.insideUnitCircle;
        public static Vector3 insideUnitSphere => UnityEngine.Random.insideUnitSphere;
        public static Quaternion rotationUniform => UnityEngine.Random.rotationUniform;
        public static float value => UnityEngine.Random.value;

        public static Color ColorHSV(float hueMin, float hueMax, float saturationMin, float saturationMax, float valueMin, float valueMax, float alphaMin, float alphaMax)
        => UnityEngine.Random.ColorHSV(hueMin, hueMax, saturationMin, saturationMax, valueMin, valueMax, alphaMin, alphaMax);
        public static Color ColorHSV()
        => UnityEngine.Random.ColorHSV();
        public static Color ColorHSV(float hueMin, float hueMax)
        => UnityEngine.Random.ColorHSV(hueMin, hueMax);
        public static Color ColorHSV(float hueMin, float hueMax, float saturationMin, float saturationMax)
        => UnityEngine.Random.ColorHSV(hueMin, hueMax, saturationMin, saturationMax);
        public static Color ColorHSV(float hueMin, float hueMax, float saturationMin, float saturationMax, float valueMin, float valueMax)
        => UnityEngine.Random.ColorHSV(hueMin, hueMax, saturationMin, saturationMax, valueMin, valueMax);
        public static void InitState(int seed)
        => UnityEngine.Random.InitState(seed);
        public static int Range(int min, int max)
        => UnityEngine.Random.Range(min, max);
        public static float Range(float min, float max)
        => UnityEngine.Random.Range(min, max);

        ///<summary>a : b = true : false</summary>
        public static bool Weight(int a, int b) => value * (a + b) < a;
    }
}
