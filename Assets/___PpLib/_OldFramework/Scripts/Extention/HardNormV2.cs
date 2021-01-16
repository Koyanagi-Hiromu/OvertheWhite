using Sirenix.OdinInspector;
using UnityEngine;

namespace SR
{
    /// <summary>
    /// 単位ベクトル　ただし、(0, 0) を許可しない (0, 1)にします
    /// 
    /// (1, 0)ではいことに注意！
    /// </summary>
    public struct HardNormV2
    {
        public static HardNormV2 DEFAULT => UP;

        [ShowInInspector] public float x { get; private set; }
        [ShowInInspector] public float y { get; private set; }

        public float rad => new V2(this.x, this.y).rad;

        [ShowInInspector] public float deg => new V2(this.x, this.y).deg;
        public float degWorld => new V2(this.x, this.y).degWorld;

        public Vector2 AsVector2 => new Vector2(x, y);
        public Vector2 AsVec2 => new Vector2(x, y);
        public Vector3 AsVec3OnUI => new Vector3(x, y, 0);
        public Vector3 AsVec3OnWorld => new Vector3(x, 0, y);
        public readonly static HardNormV2 RIGHT = new HardNormV2(1, 0);
        public readonly static HardNormV2 RIGHT_UP = new HardNormV2(1, 1);
        public readonly static HardNormV2 UP = new HardNormV2(0, 1);
        public readonly static HardNormV2 UP_LEFT = new HardNormV2(-1, 1);
        public readonly static HardNormV2 LEFT = new HardNormV2(-1, 0);
        public readonly static HardNormV2 LEFT_DOWN = new HardNormV2(-1, -1);
        public readonly static HardNormV2 DOWN = new HardNormV2(0, -1);
        public readonly static HardNormV2 DOWN_RIGHT = new HardNormV2(1, -1);

        public HardNormV2(SoftNormV2 n)
        {
            if (n.IsZero())
            {
                this.x = 0;
                this.y = 1;
            }
            else
            {
                this.x = n.x;
                this.y = n.y;
            }
        }

        public HardNormV2(float x, float y)
        {
            if (x == 0 && y == 0)
            {
                this.x = DEFAULT.x;
                this.y = DEFAULT.y;
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

        public static implicit operator V2(HardNormV2 unitV2)
        {
            return unitV2.AsV2;
        }

        public V2 AsV2
        {
            get { return new V2(x, y); }
        }

        public Vector3 eulerAnglesUI => new Vector3(0, 0, deg_UnityUI);
        public Vector3 eulerAnglesWorld => new Vector3(0, deg_UnityWorld, 0);

        public float deg_UnityUI => deg_Math - 90;
        public float deg_UnityWorld => -deg_Math + 90;

        /// <summary>
        /// x軸から左回りへの角度。
        /// 
        /// 数学座標系。
        /// </summary>
        /// <returns></returns>
        public float deg_Math => rad_Math * Mathf.Rad2Deg;
        public float rad_Math => Mathf.Acos(x) * y.AsSign();

        public HardNormV2 RotateDeg(float deg)
        {
            return RotateRad(deg * Mathf.Deg2Rad);
        }

        public HardNormV2 RotateRad(float rad)
        {
            var _x = x * Mathf.Cos(rad) - y * Mathf.Sin(rad);
            var _y = x * Mathf.Sin(rad) + y * Mathf.Cos(rad);
            x = _x;
            y = _y;
            return this;
        }

        public static V2 operator +(HardNormV2 a, V2 b)
        {
            //20190319 重いからimplicit castは使わない 
            return new V2(a.x, a.y) + b;
            // return (V2) a + b;
        }

        public static V2 operator +(V2 a, HardNormV2 b)
        {
            //20190319 重いからimplicit castは使わない 
            return a + new V2(b.x, b.y);
            // return a + (V2) b;
        }

        public static V2 operator +(HardNormV2 a, HardNormV2 b)
        {
            //20190319 重いからimplicit castは使わない 
            return new V2(a.x, a.y) + new V2(b.x, b.y);
            // return (V2) a + (V2) b;
        }

        public static V2 operator *(HardNormV2 a, float b)
        {
            //20190319 重いからimplicit castは使わない 
            return new V2(a.x, a.y) * b;
            // return (V2) a * b;
        }

        public static V2 operator /(HardNormV2 a, float b)
        {
            //20190319 重いからimplicit castは使わない 
            return new V2(a.x, a.y) / b;
            // return (V2) a / b;
        }

        public static V2 operator *(float a, HardNormV2 b)
        {
            //20190319 重いからimplicit castは使わない 
            return a * new V2(b.x, b.y);
            // return a * (V2) b;
        }

        public static V2 operator /(float a, HardNormV2 b)
        {
            //20190319 重いからimplicit castは使わない 
            return a / new V2(b.x, b.y);
            // return a / (V2) b;
        }

        public static bool operator ==(HardNormV2 a, HardNormV2 b)
        {
            return a.x == b.x && a.y == b.y;
        }

        public static bool operator !=(HardNormV2 a, HardNormV2 b)
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