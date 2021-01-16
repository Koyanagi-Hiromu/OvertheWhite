using UnityEngine;

namespace SR
{
    public struct Mass
    {
        public int x, y;
        /// <summary>
        /// 3.6f -> 4 としたとき、+0.4f
        /// 1.2f -> 1 としたとき、-0.2f
        /// </summary>
        public float correctX, correctY;

        public Mass(int x, int y)
        {
            this.x = x;
            this.y = y;
            this.correctX = 0;
            this.correctY = 0;
        }

        public Mass(float x, float y)
        {
            this.x = Mathf.RoundToInt(x);
            this.y = Mathf.RoundToInt(y);
            this.correctX = this.x - x;
            this.correctY = this.y - y;
        }

        public Mass(Vector2 v2) : this(v2.x, v2.y) { }
    }
}
