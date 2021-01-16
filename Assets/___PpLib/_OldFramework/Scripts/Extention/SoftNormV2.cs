using Sirenix.OdinInspector;
using UnityEngine;

namespace SR
{
    /// <summary>
    /// 単位ベクトル　ただし、(0, 0) を許容する
    /// </summary>
    public struct SoftNormV2
    {
        [ShowInInspector] public float x { get; private set; }

        [ShowInInspector] public float y { get; private set; }

        public readonly static SoftNormV2 RIGHT = new SoftNormV2(1, 0);
        public readonly static SoftNormV2 RIGHT_UP = new SoftNormV2(1, 1);
        public readonly static SoftNormV2 UP = new SoftNormV2(0, 1);
        public readonly static SoftNormV2 UP_LEFT = new SoftNormV2(-1, 1);
        public readonly static SoftNormV2 LEFT = new SoftNormV2(-1, 0);
        public readonly static SoftNormV2 LEFT_DOWN = new SoftNormV2(-1, -1);
        public readonly static SoftNormV2 DOWN = new SoftNormV2(0, -1);
        public readonly static SoftNormV2 DOWN_RIGHT = new SoftNormV2(1, -1);
        public readonly static SoftNormV2 DEFAULT = new SoftNormV2(0, 0);

        public SoftNormV2(float x, float y)
        {
            if (x == 0 && y == 0)
            {
                this.x = 0;
                this.y = 0;
            }
            else
            {
                var sqMagnitude = x * x + y * y;
                if (sqMagnitude == 1)
                {
                    this.x = x;
                    this.y = y;
                }
                else
                {
                    var magnitude = Mathf.Sqrt(sqMagnitude);
                    this.x = x / magnitude;
                    this.y = y / magnitude;
                }
            }
        }

        public SoftNormV2(HardNormV2 hnv2)
        {
            this.x = hnv2.x;
            this.y = hnv2.y;
        }

        public static implicit operator V2(SoftNormV2 unitV2)
        {
            return unitV2.AsV2;
        }

        public Vector3 eulerAnglesUI => new Vector3(0, 0, deg_UnityUI);
        public Vector3 eulerAnglesWorld => new Vector3(0, deg_UnityWorld, 0);
        public float deg_UnityUI => deg_Math - 90;
        public float deg_UnityWorld => -deg_Math + 90;
        public float deg_Math => rad_Math * Mathf.Rad2Deg;
        public float rad_Math => Mathf.Acos(x) * y.AsSign();

        public V2 AsV2
        {
            get { return new V2(x, y); }
        }

        public Vector2 AsVector2 => new Vector2(x, y);
        public Vector2 AsVec2 => new Vector2(x, y);
        public Vector2 AsVec3 => new Vector3(x, y, 0);

        public HardNormV2 AsHard => new HardNormV2(this);

        public SoftNormV2 RotateDeg(float deg)
        {
            return RotateRad(deg * Mathf.Deg2Rad);
        }

        public SoftNormV2 RotateRad(float rad)
        {
            var _x = x * Mathf.Cos(rad) - y * Mathf.Sin(rad);
            var _y = x * Mathf.Sin(rad) + y * Mathf.Cos(rad);
            x = _x;
            y = _y;
            return this;
        }

        public bool IsZero()
        {
            return x == 0 && y == 0;
        }

        public static V2 operator +(SoftNormV2 a, V2 b)
        {
            return (V2)a + b;
        }

        public static V2 operator +(V2 a, SoftNormV2 b)
        {
            return a + (V2)b;
        }

        public static V2 operator +(SoftNormV2 a, SoftNormV2 b)
        {
            return (V2)a + (V2)b;
        }

        public static V2 operator *(SoftNormV2 a, float b)
        {
            return (V2)a * b;
        }

        public static V2 operator /(SoftNormV2 a, float b)
        {
            return (V2)a / b;
        }

        public static V2 operator *(float a, SoftNormV2 b)
        {
            return a * (V2)b;
        }

        public static V2 operator /(float a, SoftNormV2 b)
        {
            return a / (V2)b;
        }

        public static bool operator ==(SoftNormV2 a, SoftNormV2 b)
        {
            return a.x == b.x && a.y == b.y;
        }

        public static bool operator !=(SoftNormV2 a, SoftNormV2 b)
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
}