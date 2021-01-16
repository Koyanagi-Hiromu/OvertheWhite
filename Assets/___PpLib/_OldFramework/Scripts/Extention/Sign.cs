using System;
using UnityEngine;
namespace SR
{
    /// <summary>
    /// 正負を返すストラクト
    /// </summary>
    public struct Sign : IEquatable<Sign>
    {
        public static Sign Random
        {
            get
            {
                return new Sign(UnityEngine.Random.value < 0.5f);
            }
        }

        public static readonly Sign Plus = new Sign(true);
        public static readonly Sign Minus = new Sign(false);

        /// <summary>
        /// 2 : プラス
        /// 1 : ゼロ
        /// 0 : マイナス
        /// </summary>
        private byte value;
        public Sign(float x)
        {
            this.value = (byte)(Mathf.Sign(x) + 1);
        }

        public Sign(bool b)
        {
            this.value = b ? (byte)2 : (byte)0;
        }

        public static implicit operator int(Sign sign)
        {
            return sign.value - 1;
        }

        public bool Equals(Sign other) => value == other.value;
    }
}
