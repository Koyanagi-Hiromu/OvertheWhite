using System.Collections;
using UnityEngine;
using System;
using UnityEngine.Playables;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace PPD
{
    public static class EX_Transform
    {
        public static void LocalLerp(this Transform self, Transform a, Transform b, float ratio, bool position = true, bool rotation = true, bool scale = true)
        {
            if (position)
            {
                self.localPosition = Vector3.Lerp(a.localPosition, b.localPosition, ratio);
            }
            if (rotation)
            {
                self.localEulerAngles = Vector3.Lerp(a.localEulerAngles, b.localEulerAngles, ratio);
            }
            if (scale)
            {
                self.localScale = Vector3.Lerp(a.localScale, b.localScale, ratio);
            }
        }

        ///<summary>scaleの記述が正しいか不安です。使用した方はウェレイまで教えて下さい。</summary>
        public static void WorldLerp(this Transform self, Transform a, Transform b, float ratio, bool position = true, bool rotation = true, bool scale = true)
        {
            if (position)
            {
                self.position = Vector3.Lerp(a.position, b.position, ratio);
            }
            if (rotation)
            {
                self.eulerAngles = Vector3.Lerp(a.eulerAngles, b.eulerAngles, ratio);
            }
            if (scale)
            {
                self.SetWorldScale(Vector3.Lerp(a.lossyScale, b.lossyScale, ratio));
            }
        }

        ///<summary>記述が正しいか不安です。使用した方はウェレイまで教えて下さい。</summary>
        public static void SetWorldScale(this Transform self, Vector3 worldScale)
        {
            var ls = self.lossyScale;
            self.localScale = new Vector3(worldScale.x / ls.x, worldScale.y / ls.y, worldScale.z / ls.z);
        }

        public static void SetLocalScaleX(this Transform transform, float value)
        {
            var v = transform.localScale;
            v.x = value;
            transform.localScale = v;
        }

        public static void SetLocalScaleY(this Transform transform, float value)
        {
            var v = transform.localScale;
            v.y = value;
            transform.localScale = v;
        }

        public static void SetLocalScaleZ(this Transform transform, float value)
        {
            var v = transform.localScale;
            v.z = value;
            transform.localScale = v;
        }


        public static void SetLocalX(this Transform transform, float value)
        {
            var v = transform.localPosition;
            v.x = value;
            transform.localPosition = v;
        }

        public static void SetLocalY(this Transform transform, float value)
        {
            var v = transform.localPosition;
            v.y = value;
            transform.localPosition = v;
        }

        public static void SetLocalZ(this Transform transform, float value)
        {
            var v = transform.localPosition;
            v.z = value;
            transform.localPosition = v;
        }

        public static void SetGlobalX(this Transform transform, float value)
        {
            var v = transform.position;
            v.x = value;
            transform.position = v;
        }

        public static void SetGlobalY(this Transform transform, float value)
        {
            var v = transform.position;
            v.y = value;
            transform.position = v;
        }

        public static void SetGlobalZ(this Transform transform, float value)
        {
            var v = transform.position;
            v.z = value;
            transform.position = v;
        }

        public static void SetRotateX(this Transform transform, float value)
        {
            var v = transform.rotation;
            v.x = value;
            transform.rotation = v;
        }

        public static void SetRotateY(this Transform transform, float value)
        {
            var v = transform.rotation;
            v.y = value;
            transform.rotation = v;
        }

        public static void SetRotateZ(this Transform transform, float value)
        {
            var v = transform.rotation;
            v.z = value;
            transform.rotation = v;
        }
        public static void AddX(this Transform transform, float value)
        {
            var v = transform.localPosition;
            v.x += value;
            transform.localPosition = v;
        }

        public static void AddY(this Transform transform, float value)
        {
            var v = transform.localPosition;
            v.y += value;
            transform.localPosition = v;
        }

        public static void AddZ(this Transform transform, float value)
        {
            var v = transform.localPosition;
            v.z += value;
            transform.localPosition = v;
        }

        public static void AddDegX(this Transform transform, float value)
        {
            var v = transform.localEulerAngles;
            v.x += value;
            transform.localEulerAngles = v;
        }

        public static void AddDegY(this Transform transform, float value)
        {
            var v = transform.localEulerAngles;
            v.y += value;
            transform.localEulerAngles = v;
        }

        public static void AddDegZ(this Transform transform, float value)
        {
            var v = transform.localEulerAngles;
            v.z += value;
            transform.localEulerAngles = v;
        }
    }
}
