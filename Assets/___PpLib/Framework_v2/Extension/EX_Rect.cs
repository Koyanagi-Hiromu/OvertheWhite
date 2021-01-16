using UnityEngine;

namespace PPD
{
    public static class EX_Rect
    {
        // http://answers.unity3d.com/questions/1013011/convert-recttransform-rect-to-screen-space.html?sort=newest
        public static Rect RectTransformToScreenSpace(this RectTransform transform)
        {
            Vector2 size = Vector2.Scale(transform.rect.size, transform.lossyScale);
            var x = transform.position.x;
            var y = Screen.height - transform.position.y;

            x -= (transform.pivot.x * size.x);
            y -= ((1.0f - transform.pivot.y) * size.y);

            Rect rect = new Rect(x, y, size.x, size.y);
            return rect;
        }

        public static Vector2 RandomPoint(this Rect rect)
        {
            var x = UnityEngine.Random.Range(rect.xMin, rect.xMax);
            var y = UnityEngine.Random.Range(rect.yMin, rect.yMax);
            return new Vector2(x, y);
        }

        public static Vector2 RandomPoint_PivotCenter(this Rect rect)
        {
            var x = rect.x + UnityEngine.Random.Range(-rect.width, rect.width) / 2;
            var y = rect.y + UnityEngine.Random.Range(-rect.height, rect.height) / 2;
            return new Vector2(x, y);
        }
    }
}
