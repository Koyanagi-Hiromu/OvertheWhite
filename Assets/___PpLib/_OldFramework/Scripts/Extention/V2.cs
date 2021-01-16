using System;
using Sirenix.OdinInspector;
using UnityEngine;

namespace SR
{
    [Serializable]
    [InlineProperty(LabelWidth = 12)]
    public struct V2
    {
        [HorizontalGroup]
        public float x;

        [HorizontalGroup]
        public float y;

        public float length => Mathf.Sqrt(sqrLength);
        public float sqrLength => x * x + y * y;
        public float magnitude => length;
        public float sqrMagnitude => sqrLength;
        public float rad => GetRad();
        /// <summary>
        /// x軸から左回りへの角度。
        /// 
        /// 数学座標系。
        /// </summary>
        /// <returns></returns>
        public float deg => GetDegree();

        /// <summary>
        /// y軸から右回りへの角度。
        /// 
        /// eulerAnglesに使用してください。
        /// </summary>
        public float degWorld => GetDegree_World();
        /// <summary>
        /// 単位ベクトル　ただし、(0, 0) を許容する
        /// </summary>
        public SoftNormV2 asSoftNorm => new SoftNormV2(x, y);

        /// <summary>
        /// 単位ベクトル　ただし、(0, 0) を許可しない (0, 1)にします
        /// </summary>
        /// 
        public HardNormV2 asHardNorm => new HardNormV2(x, y);

        /// <summary>
        /// 単位ベクトル　ただし、(0, 0) を許可しない (x, y)にします
        /// </summary>
        /// 
        public HardNormV2 AsRandomNorm()
        {
            if (x == 0 && y == 0)
            {
                return RandomNorm();
            }
            else
            {
                return new HardNormV2(x, y);
            }
        }
        public HardNormV2 AsHardNorm(HardNormV2 basic)
        {
            if (x == 0 && y == 0)
            {
                return basic;
            }
            else
            {
                return new HardNormV2(x, y);
            }
        }
        public Vector2 AsVec2 => new Vector2(x, y);
        public Vector3 AsUI => new Vector3(x, y, 0);
        public Vector3 AsVec3OnUI => new Vector3(x, y, 0);
        public Vector3 AsWorld => new Vector3(x, 0, y);
        public Vector3 AsVec3OnWorld => new Vector3(x, 0, y);

        public static HardNormV2 RIGHT => HardNormV2.RIGHT;
        public static HardNormV2 RIGHT_UP => HardNormV2.RIGHT_UP;
        public static HardNormV2 UP => HardNormV2.UP;
        public static HardNormV2 UP_LEFT => HardNormV2.UP_LEFT;
        public static HardNormV2 LEFT => HardNormV2.LEFT;
        public static HardNormV2 LEFT_DOWN => HardNormV2.LEFT_DOWN;
        public static HardNormV2 DOWN => HardNormV2.DOWN;
        public static HardNormV2 DOWN_RIGHT => HardNormV2.DOWN_RIGHT;
        public static SoftNormV2 ZERO => SoftNormV2.DEFAULT;
        public static HardNormV2 ONE => HardNormV2.RIGHT_UP;

        public Vector3 AsXZ(float Y)
        {
            return new Vector3(x, Y, y);
        }

        public V2(float x, float y) : this()
        {
            this.x = x;
            this.y = y;
        }

        public static HardNormV2 MakeForRad(float rad)
        {
            return new HardNormV2(Mathf.Cos(rad), Mathf.Sin(rad));
        }

        public static Vector2 MakeForDegAsVector2(float degree)
        {
            var rad = (degree + 90) * Mathf.Deg2Rad;
            return new Vector2(Mathf.Cos(rad), Mathf.Sin(rad));
        }

        public static HardNormV2 MakeForDeg(float degree)
        {
            return MakeForRad(Mathf.Deg2Rad * degree);
        }

        public static HardNormV2 MakeForDeg_World(float degree)
        {
            degree = -degree + 90;
            return MakeForRad(Mathf.Deg2Rad * degree);
        }

        public static HardNormV2 RandomNorm()
        {
            return MakeForRad(UnityEngine.Random.Range(0, Mathf.PI * 2));
        }

        public static V2 RandomV2(float length)
         => RandomNorm() * length;

        public static V2 RandomV2(float minLength, float maxLength)
         => RandomV2(UnityEngine.Random.Range(minLength, maxLength));

        public V2 Vertical()
        {
            return new V2(-this.y, this.x);
        }

        public float GetMinMaxRange()
        {
            return UnityEngine.Random.Range(x, y);
        }

        /// <summary>
        /// y軸から右回りへの角度。
        /// 
        /// eulerAnglesに使用してください。
        /// </summary>
        public float GetDegree_World()
        {
            return -GetDegree() + 90;
        }

        /// <summary>
        /// x軸から左回りへの角度。
        /// 
        /// 数学座標系。
        /// </summary>
        /// <returns></returns>
        public float GetDegree()
        {
            return GetRad() * Mathf.Rad2Deg;
        }

        public float GetRad()
        {
            if (this == V2.ZERO)
            {
                return 0;
            }
            else
            {
                var cos = x / length;
                return Mathf.Acos(cos) * y.AsSign();
            }
        }

        public V2 GetRotatePos(float degree)
        {
            var radius = degree * Mathf.Deg2Rad;
            var cos = Mathf.Cos(radius);
            var sin = Mathf.Sin(radius);
            var x = this.x * cos - this.y * sin;
            var y = this.x * sin + this.y * cos;
            return new V2(x, y);
        }

        public V2 Set(float x, float y)
        {
            this.x = x;
            this.y = y;
            return this;
        }

        public V2 SetX(float x)
        {
            this.x = x;
            return this;
        }

        public V2 SetY(float y)
        {
            this.y = y;
            return this;
        }

        public V2 AddX(float x)
        {
            this.x += x;
            return this;
        }

        public V2 AddY(float y)
        {
            this.y += y;
            return this;
        }

        public V2 Reverse()
        {
            return this * -1;
        }

        /// <summary>
        /// 内積
        /// </summary>
        /// <returns></returns>
        public float InnerProduct(V2 another)
        {
            return this.x * another.x + this.y * another.y;
        }

        public bool IsEmpty()
        {
            return
            Mathf.Approximately(x, 0) &&
                Mathf.Approximately(y, 0);
        }

        public V2 Scale(float x, float y)
        {
            return ScaleX(x).ScaleY(y);
        }

        public V2 ScaleX(float x)
        {
            this.x *= x;
            return this;
        }

        public V2 ScaleY(float y)
        {
            this.y *= y;
            return this;
        }

        public override string ToString()
        {
            return "x: " + x + ", y: " + y;
        }

        public static V2 operator +(V2 a, V2 b)
        {
            return new V2(a.x + b.x, a.y + b.y);
        }

        public static V2 operator -(V2 a, V2 b)
        {
            return new V2(a.x - b.x, a.y - b.y);
        }

        public static V2 operator *(V2 a, V2 b)
        {
            return new V2(a.x * b.x, a.y * b.y);
        }

        public static V2 operator /(V2 a, V2 b)
        {
            return new V2(a.x / b.x, a.y / b.y);
        }

        public static V2 operator *(V2 a, float b)
        {
            return new V2(a.x * b, a.y * b);
        }

        public static V2 operator /(V2 a, float b)
        {
            return new V2(a.x / b, a.y / b);
        }

        public static V2 operator *(float a, V2 b)
        {
            return new V2(a * b.x, a * b.y);
        }

        public static V2 operator /(float a, V2 b)
        {
            return new V2(a / b.x, a / b.y);
        }

        public static bool operator ==(V2 a, V2 b)
        {
            return a.x == b.x && a.y == b.y;
        }

        public static bool operator !=(V2 a, V2 b)
        {
            return a.x != b.x || a.y != b.y;
        }

        public override int GetHashCode()
        {
            return 0;
        }

        public override bool Equals(object o)
        {
            return this == (V2)o;
        }
    }

    public enum Direction : int
    {
        LEFT_DOWN = 1,
        DOWN = 2,
        DOWN_RIGHT = 3,
        LEFT = 4,
        DEFAULT = 5,
        RIGHT = 6,
        UP_LEFT = 7,
        UP = 8,
        RIGHT_UP = 9,
    }

    public static class Pos2Extension
    {
        public static V2 AsV2(this Vector3 v3)
        {
            return v3.ToVec2_xz().AsV2();
        }

        public static V2 AsV2xy(this Vector3 v3)
        {
            return v3.ToVec2_xy().AsV2();
        }

        public static V2 AsV2(this Vector2 v2)
        {
            return new V2(v2.x, v2.y);
        }

        public static SoftNormV2 AsSoftNormV2(this Vector2 v2)
        {
            return new SoftNormV2(v2.x, v2.y);
        }

        [Obsolete("Use AsV2")]
        public static V2 ToV2(this Vector3 v3)
        {
            return v3.AsV2();
        }

        [Obsolete("Use AsV2")]
        public static V2 ToV2(this Vector2 v2)
        {
            return v2.AsV2();
        }

        public static SoftNormV2 AsNormV2(this Direction d)
        {
            switch (d)
            {
                case Direction.RIGHT:
                    return SoftNormV2.RIGHT;
                case Direction.RIGHT_UP:
                    return SoftNormV2.RIGHT_UP;
                case Direction.UP:
                    return SoftNormV2.UP;
                case Direction.UP_LEFT:
                    return SoftNormV2.UP_LEFT;
                case Direction.LEFT:
                    return SoftNormV2.LEFT;
                case Direction.LEFT_DOWN:
                    return SoftNormV2.LEFT_DOWN;
                case Direction.DOWN:
                    return SoftNormV2.DOWN;
                case Direction.DOWN_RIGHT:
                    return SoftNormV2.DOWN_RIGHT;
                case Direction.DEFAULT:
                default:
                    return SoftNormV2.DEFAULT;
            }
        }
    }
}