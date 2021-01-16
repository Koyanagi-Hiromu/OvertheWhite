using UnityEngine;

namespace PPD
{
    [ExecuteInEditMode]
    public class ED_PingPongRouteTest : ED_AutoMoveTest
    {
        protected override float Interval => (end.position - start.position).magnitude / speed;
        public float speed = 2;
        public Transform start;
        public Transform end;

        protected override void UnityUpdate(float ratio)
        {
            this.transform.position = start.position + (end.position - start.position) * ratio;
        }
    }
}