using UnityEngine;
using System;
namespace SR
{
    public static class UtilMath
    {
        public static float Abs(this float value)
        {
            return Mathf.Abs(value);
        }

        public static int Abs(this int value)
        {
            return value > 0 ? value : -value;
        }

        /// <returns>1 or 0</returns>
        public static int AsSign_1_0(this bool flg)
        {
            return flg ? 1 : 0;
        }

        public static bool IsOdd(this int origin)
        {
            return origin % 2 == 1;
        }

        public static bool IsEven(this int origin)
        {
            return origin % 2 == 0;
        }

        /// <summary>
        /// 符号を取得します 0は１を返します
        /// </summary>
        /// <param name="origin"></param>
        /// <returns></returns>
        public static Sign AsSign(this int origin)
        {
            return new Sign(origin);
        }

        /// <summary>
        /// 符号(1 or -1)を取得します
        /// </summary>
        /// <param name="origin"></param>
        /// <returns></returns>
        public static Sign AsSign(this float origin)
        {
            return new Sign(origin);
        }

        /// <summary>
        /// trueを１に、falseを-1に変換します。
        /// </summary>
        /// <returns> 1 or -1</returns>
        public static Sign AsSign(this bool origin)
        {
            return new Sign(origin);
        }

        /// <returns> -value if true or default; otherwise value.</returns>
        public static int Flip(this int value, bool flg = true)
        {
            return value * (!flg).AsSign();
        }

        /// <returns> -value if true; otherwise value.</returns>
        public static float Flip(this float value, bool flg = true)
        {
            return value * (!flg).AsSign();
        }

        /// <summary>
        /// 剰余を返します　ただし負の値を返しません
        /// </summary>
        /// <returns> [0, max] （※[-max, max]でないことに注意）</returns>
        public static int Wrap(this int value, int max)
        {
            var surplus = value % max;
            if (surplus < 0)
            {
                surplus += max;
            }
            return surplus;
        }

        /// <summary>
        /// 剰余を返します　ただし負の値を返しません
        /// </summary>
        /// <returns> [0, max] （※[-max, max]でないことに注意）</returns>
        public static float Wrap(this float value, float max)
        {
            var surplus = value % max;
            if (surplus < 0)
            {
                surplus += max;
            }
            return surplus;
        }

        /// <returns>[0, 360]</returns>
        public static float WrapDegree360(this float degree)
        {
            return Wrap(degree, 360);
        }

        /// <returns>[0, 180]</returns>
        public static float WrapDegree180(this float degree)
        {
            return Wrap(degree, 180);
        }

        /// <returns>[-180, 180]</returns>
        public static float WrapDegree180_180(this float degree)
        {
            var deg = Wrap(degree, 360);
            if (deg > 180) deg -= 360;
            return deg;
        }

        /// <returns>[0, 2Pi]</returns>
        public static float WrapRadian(this float rad)
        {
            return Wrap(rad, 2 * Mathf.PI);
        }


        public static int Clamp(this int value, int min, int max)
        {
            return Mathf.Min(Mathf.Max(value, min), max);
        }

        public static float Clamp(this float value, float min, float max)
        {
            return Mathf.Clamp(value, min, max);
        }

        public static float Clamp01(this float value, float min, float max)
        {
            return Mathf.Clamp01(value);
        }

        /// <returns>0, x => return false</returns>
        public static bool IsSameSign(this int a, int b)
        {
            return (a > 0 && b > 0) || (a < 0 && b < 0);
        }

        /// <returns>0, x => return false</returns>
        public static bool IsSameSign(this float a, float b)
        {
            return (a > 0 && b > 0) || (a < 0 && b < 0);
        }

        public static bool IsWithin(this float value, float min, float max)
        {
            return min <= value && value < max;
        }

        public static bool IsWithin(this int value, int min, int max)
        {
            return min <= value && value < max;
        }

        /// <summary>
        ///  10 is within [-20, 20].
        /// 370 is within [-20, 20].
        /// !!!! min ＜ max !!!!
        /// </summary>
        public static bool IsWithin_deg(this int value, int min, int max)
        {
            return IsWithin_deg((float)value, min, max);
        }

        /// <summary>
        ///  10 is within [-20, 20].
        /// 370 is within [-20, 20].
        /// !!!! min ＜ max !!!!
        /// </summary>
        public static bool IsWithin_deg(this float value, float min, float max)
        {
            Assert.IsTrue(min <= max);

            value = value.WrapDegree360();
            min = min.WrapDegree360();
            max = max.WrapDegree360();
            if (min < max)
            {
                // [0, 380] -> [0, 20]
                return value.IsWithin(min, max);
            }
            else
            {
                // [-20, 20] -> [340, 20]
                // v = 350 is OK (v > min)
                // v =  10 is OK (max > v)
                return value >= min || max >= value;
            }
        }

        /// <summary>
        /// 内積
        /// </summary>
        public static float InnerProduct(this Vector2 a, Vector2 b)
        {
            return a.x * b.x + a.y * b.y;
        }
    }
}
