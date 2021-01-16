using UnityEngine;

namespace PPD
{
    public static class EX_Vector
    {
        public static Vector3 RandomNorm3
         => Random.onUnitSphere;

        public static Vector3 insideUnitSphere => Random.insideUnitSphere;
        public static Vector2 insideUnitCircle => Random.insideUnitCircle;
        public static Vector2 RandomNorm
        => MakeForRad(Random.value * Mathf.PI * 2);

        public static Vector2 MakeForDeg(float deg)
        => MakeForRad(deg * Mathf.Deg2Rad);

        public static Vector2 MakeForRad(float rad)
        => new Vector2(Mathf.Cos(rad), Mathf.Sin(rad));

        public static void Inverse_Ref(ref this Vector2 v)
        {
            v.x = 1f / v.x;
            v.y = 1f / v.y;
        }

        public static Vector2 Inverse_In(in this Vector2 v)
        {
            var ret = v;
            ret.Inverse_Ref();
            return ret;
        }

        public static Vector2 Inverse_In(in this Vector2Int v)
        {
            var ret = new Vector2(v.x, v.y);
            ret.Inverse_Ref();
            return ret;
        }

        public static void Inverse_Ref(ref this Vector3 v)
        {
            v.x = 1f / v.x;
            v.y = 1f / v.y;
            v.z = 1f / v.z;
        }

        public static Vector3 Inverse_In(in this Vector3 v)
        {
            var ret = v;
            ret.Inverse_Ref();
            return ret;
        }

        public static Vector3 Inverse_In(in this Vector3Int v)
        {
            var ret = new Vector3(v.x, v.y, v.z);
            ret.Inverse_Ref();
            return ret;
        }

        public static Quaternion AsQuaternion(this Vector3 v)
        => Quaternion.LookRotation(v);

        public static void ChangeX_Ref(ref this Vector3 v, float val)
        {
            v.x = val;
        }

        public static void ChangeY_Ref(ref this Vector3 v, float val)
        {
            v.y = val;
        }

        public static void ChangeZ_Ref(ref this Vector3 v, float val)
        {
            v.z = val;
        }

        public static Vector3 ChangeX_In(in this Vector3 v, float val)
        {
            var ret = new Vector3(v.x, v.y, v.z);
            ret.x = val;
            return ret;
        }

        public static Vector3 ChangeY_In(in this Vector3 v, float val)
        {
            var ret = new Vector3(v.x, v.y, v.z);
            ret.y = val;
            return ret;
        }

        public static Vector3 ChangeZ_In(in this Vector3 v, float val)
        {
            var ret = new Vector3(v.x, v.y, v.z);
            ret.z = val;
            return ret;
        }

        public static void AddX_Ref(ref this Vector3 v, float val)
        {
            v.x += val;
        }

        public static void AddY_Ref(ref this Vector3 v, float val)
        {
            v.y += val;
        }

        public static void AddZ_Ref(ref this Vector3 v, float val)
        {
            v.z += val;
        }

        public static Vector3 AddX_In(in this Vector3 v, float val)
        {
            var ret = new Vector3(v.x, v.y, v.z);
            ret.x += val;
            return ret;
        }

        public static Vector3 AddY_In(in this Vector3 v, float val)
        {
            var ret = new Vector3(v.x, v.y, v.z);
            ret.y += val;
            return ret;
        }

        public static Vector3 AddZ_In(in this Vector3 v, float val)
        {
            var ret = new Vector3(v.x, v.y, v.z);
            ret.z += val;
            return ret;
        }

        public static int MMRandom(this Vector2Int v) => Random.Range(v.x, v.y);
        public static float MMRandom(this Vector2 v) => Random.Range(v.x, v.y);

        ///<summar> rangeDegree = 30 なら x,y,z をそれぞれ -15 ~ +15 deg回転させます。
        public static Vector3 RandomRotate_In(in this Vector3 v, float rangeDegree)
        {
            var x = (Random.value - 0.5f) * rangeDegree;
            var y = (Random.value - 0.5f) * rangeDegree;
            var z = (Random.value - 0.5f) * rangeDegree;
            return Quaternion.Euler(x, y, z) * v;
        }

        ///<summar> rangeDegree = 30 なら x,y,z をそれぞれ -15 ~ +15 deg回転させます。
        public static void RandomRotate_Ref(ref this Vector3 v, float rangeDegree)
        {
            var gen = v.RandomRotate_In(rangeDegree);
            v.x = gen.x;
            v.y = gen.y;
            v.z = gen.z;
        }

        ///<summar> rangeDegree = 30 なら -15 ~ +15
        public static Vector2 RandomRotate_In(in this Vector2 v, float rangeDegree)
        {
            var ret = new Vector2(v.x, v.y);
            ret.RandomRotate_Ref(rangeDegree);
            return ret;
        }

        ///<summar> rangeDegree = 30 なら -15 ~ +15
        public static void RandomRotate_Ref(ref this Vector2 v, float rangeDegree)
        {
            var degree = (Random.value - 0.5f) * rangeDegree;
            v.Rotate_Ref(degree);
        }

        private static void Rotate_Ref(ref this Vector2 v, float degree)
        {
            float sin = Mathf.Sin(degree * Mathf.Deg2Rad);
            float cos = Mathf.Cos(degree * Mathf.Deg2Rad);

            float tx = v.x;
            float ty = v.y;
            v.x = (cos * tx) - (sin * ty);
            v.y = (sin * tx) + (cos * ty);
        }

        /// <summary>
        /// ベクトルrotateeをベクトルfrontの向きに合わせるように回転させます。
        /// xは、地面に水平面（y=0）に変換されます。すなわちｘは、frontのy成分に影響を受けません
        /// RotationEXP.sceneで挙動を試すことが出来ます
        /// </summary>
        /// <param name="rotatee">x:frontに対して地面に水平面上での垂直。y:xとzに垂直。</param>
        /// <returns></returns>
        public static Vector3 RotateTo(this Vector3 rotatee, Vector3 front)
        {
            if (front == Vector3.zero)
            {
                return Vector3.zero;
            }

            front.Normalize();

            var x = front.x;
            var y = front.y;
            var z = front.z;

            var horizon = new Vector3()
            {
                x = z,
                y = 0,
                z = -x
            }.normalized;

            var vertical = new Vector3()
            {
                x = -x * y,
                y = x * x + z * z,
                z = -y * z
            }.normalized;

            return horizon * rotatee.x + vertical * rotatee.y + front * rotatee.z;
        }

        /// <summary>
        /// @see RotateTo(relPos, front);
        /// </summary>
        public static Vector3 RotateTo(this Vector3 rotatee, Vector3 from, Vector3 to)
        {
            return rotatee.RotateTo(to - from);
        }

        public static Vector3 InputWorldPointZ0(this Camera camera)
        {
            var ray = camera.ScreenPointToRay(Input.mousePosition);
            var p = new Vector3();
            var t = -ray.origin.z / ray.direction.z;
            p.x = ray.origin.x + (ray.direction.x * t);
            p.y = ray.origin.y + (ray.direction.y * t);

            return p;
        }
    }
}
