using UnityEngine;
namespace SR
{
    public static class UtilVectorAndAngle
    {
        /// <param name="dampValue">must be > 0 or neglected</param>
        public static Vector3 Damp(this Vector3 vector3, float dampValue)
        {
            if (dampValue > 0)
            {
                if (vector3.magnitude > dampValue)
                {
                    return vector3 - vector3.normalized * dampValue;
                }
                else
                {
                    return Vector3.zero;
                }
            }
            else
            {
                return vector3;
            }
        }

        /// <param name="dampValue">must be > 0 or neglected</param>
        public static Vector2 Damp(this Vector2 vector2, float dampValue)
        {
            if (dampValue > 0)
            {
                if (vector2.magnitude > dampValue)
                {
                    return vector2 - vector2.normalized * dampValue;
                }
                else
                {
                    return Vector2.zero;
                }
            }
            else
            {
                return vector2;
            }
        }

        public static Vector3 Inverse(this Vector3 vector3)
        {
            return new Vector3(1.0f / vector3.x, 1.0f / vector3.y, 1.0f / vector3.z);
        }

        public static bool EqualsXZ(this Vector3 a, Vector3 b)
        {
            return a.x == b.x && a.z == b.z;
        }

        public static Vector3 To(this float origin, float x, float y, float z)
        {
            return origin * new Vector3(x, y, z);
        }

        public static Vector3 Clone(this Vector3 v3)
        {
            return new Vector3(v3.x, v3.y, v3.z);
        }

        /// <summary>
        /// [-180, +180]
        /// </summary>
        public static Vector3 Clamp180_180(this Vector3 v3)
        {
            if (v3.x > 180)
            {
                v3.x -= 360;
            }
            if (v3.y > 180)
            {
                v3.y -= 360;
            }
            if (v3.z > 180)
            {
                v3.z -= 360;
            }
            return v3;
        }

        public static void ChangeLocalY(this Transform t, float y)
        {
            t.localPosition = t.localPosition.ChangeY(y);
        }

        public static Vector3 ChangeX(this Vector3 v3, float x)
        {
            v3.x = x;
            return v3;
        }

        public static Vector3 ChangeY(this Vector3 v3, float y)
        {
            v3.y = y;
            return v3;
        }

        public static Vector3 ChangeZ(this Vector3 v3, float z)
        {
            v3.z = z;
            return v3;
        }

        public static Vector3 AddX(this Vector3 v3, float x)
        {
            v3.x = v3.x + x;
            return v3;
        }

        public static Vector3 AddY(this Vector3 v3, float y)
        {
            v3.y = v3.y + y;
            return v3;
        }

        public static Vector3 AddZ(this Vector3 v3, float z)
        {
            v3.z = v3.z + z;
            return v3;
        }

        /// <summary>
        /// xy？に変換します。【？はデフォルトで０です】
        /// Vector2は、Vector3へのキャストが可能で、その変換はこのメソッドと同じものです。
        /// キャストでは不明瞭なので、こちらのメソッドを積極的に使ってください。
        /// </summary>
        /// <param name="v2"></param>
        /// <returns>new Vector3(v2.x, v2.y, 0);</returns>
        public static Vector3 ToVec3_xy0(this Vector2 v2, float Z = 0)
        {
            return new Vector3(v2.x, v2.y, Z);
        }

        /// <summary>
        /// x？yに変換します。【？はデフォルトで０です】
        /// Vector2は、Vector3へのキャストが可能ですが、その変換はこのメソッドとは異なります。
        /// </summary>
        /// <param name="v2"></param>
        /// <returns>new Vector3(v2.x, 0, v2.y);</returns>
        public static Vector3 ToVec3_x0y(this Vector2 v2, float Y = 0)
        {
            return new Vector3(v2.x, Y, v2.y);
        }

        public static Vector2 ToVec2_xy(this Vector3 v3)
        {
            return new Vector2(v3.x, v3.y);
        }

        public static Vector2 ToVec2_xz(this Vector3 v3)
        {
            return new Vector2(v3.x, v3.z);
        }

        public static HardNormV2 AsHardNorm_xz(this Vector3 v3)
        {
            return new HardNormV2(v3.x, v3.z);
        }

        public static HardNormV2 AsHardNorm_xy(this Vector3 v3)
        {
            return new HardNormV2(v3.x, v3.y);
        }

        public static SoftNormV2 AsSoftNorm_xz(this Vector3 v3)
        {
            return new SoftNormV2(v3.x, v3.z);
        }

        public static SoftNormV2 AsSoftNorm_xy(this Vector3 v3)
        {
            return new SoftNormV2(v3.x, v3.y);
        }

        public static float Averate(this Vector2 v2)
        {
            return (v2.x + v2.y) / 3.0f;
        }

        public static float Averate(this Vector3 v3)
        {
            return (v3.x + v3.y + v3.z) / 3.0f;
        }

        public static float GetRandomValue(this Vector2 range)
        {
            return UnityEngine.Random.Range(range.x, range.y);
        }

        public static float GetRad(this Vector2 fromZero)
        {
            float dx = fromZero.x;
            float dy = fromZero.y;
            return Mathf.Atan2(dy, dx);
        }

        public static float GetDegree(this Vector2 fromZero)
        {
            float dx = fromZero.x;
            float dy = fromZero.y;
            float rad = Mathf.Atan2(dy, dx);
            return (rad * Mathf.Rad2Deg).WrapDegree360();
        }

        public static float GetDegree(this Vector2 fromPos, Vector2 toPos)
        {
            float dx = toPos.x - fromPos.x;
            float dy = toPos.y - fromPos.y;
            float rad = Mathf.Atan2(dy, dx);
            return (rad * Mathf.Rad2Deg).WrapDegree360();
        }

        /// <returns>[0, 360]</returns>
        public static float GetDegreeXZ(this Vector3 fromPos, Vector3 toPos)
        {
            return fromPos.ToVec2_xz().GetDegree(toPos.ToVec2_xz());
        }


        /// <returns>[0, 360]</returns>
        public static float GetDegreeXY(this Vector3 fromPos, Vector3 toPos)
        {
            return fromPos.ToVec2_xy().GetDegree(toPos.ToVec2_xy());
        }

        public static Vector2 ClosestPosition(this Vector2 targetPos, Rect rect)
        {
            var returnPos = targetPos;

            if (targetPos.x > rect.xMax)
            {
                returnPos.x = rect.xMax;
            }
            if (targetPos.x < rect.xMin)
            {
                returnPos.x = rect.xMin;
            }
            if (targetPos.y > rect.yMax)
            {
                returnPos.y = rect.yMax;
            }
            if (targetPos.y < rect.yMin)
            {
                returnPos.y = rect.yMin;
            }

            return returnPos;
        }

        public static Vector2 CrossPosition(this Vector2 targetPos, Rect rect)
        {
            return targetPos.CrossPosition(rect.center, rect.size);
        }

        public static Vector2 Rotate(this Vector2 self, float degree)
        {
            return Quaternion.Euler(0, 0, degree) * self;
        }

        public static Vector2 CrossPosition(this Vector2 targetPos, Vector2 originPos, Vector2 size)
        {
            var rad = (targetPos - originPos).GetRad();

            var hSize = size / 2;
            var cos = Mathf.Cos(rad);
            var sin = Mathf.Sin(rad);

            // 交点の目標座標
            var x = hSize.x * (cos > 0).AsSign();
            var y = hSize.y * (sin > 0).AsSign();

            //交点までの長さ
            //Absは -x / 0 or -y / 0 対策
            var lx = (x / cos).Abs();
            var ly = (y / sin).Abs();

            if (lx < ly)
            {
                y = lx * sin;
            }
            else
            {
                x = ly * cos;
            }

            return originPos + new Vector2(x, y);
        }
    }
}
