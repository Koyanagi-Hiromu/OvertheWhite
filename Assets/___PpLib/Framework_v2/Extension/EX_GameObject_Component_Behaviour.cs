using System.Collections;
using UnityEngine;
using System;
using UnityEngine.Playables;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace PPD
{
    public static class EX_GameObject_Component_Behaviour
    {
        /// <summary>
        /// gameObjectを削除します。
        /// コンポーネントを削除するのではないことに注意してください。
        /// </summary>
        /// <param name="b"></param>
        public static void DestroyInstance(this Component b)
        {
            GameObject.Destroy(b.gameObject);
        }

        /// <summary>
        /// コンポーネントを削除します
        /// </summary>
        /// <param name="b"></param>
        public static void DestroyComponent(this Component b)
        {
            GameObject.Destroy(b);
        }

        /// <summary>
        /// Destroyメソッドのエイリアスです。
        /// </summary>
        /// <param name="obj"></param>
        public static void Destroy(this GameObject obj)
        {
            GameObject.Destroy(obj);
        }

        /// <summary>
        /// Destroyメソッドのエイリアスです。
        /// </summary>
        /// <param name="obj"></param>
        public static void DestroyInstance(this GameObject obj)
        {
            GameObject.Destroy(obj);
        }

        public static bool HasComponent<T>(this GameObject obj)
        where T : Behaviour
        {
            return obj.GetComponent<T>() != null;
        }

        public static GameObject GetParent(this GameObject obj)
        {
            var parentTransform = obj.transform.parent;
            return parentTransform ? parentTransform.gameObject : null;
        }

        public static GameObject GetParent(this Behaviour obj)
        {
            return obj.transform.parent.gameObject;
        }

        public static void SetParent(this Component child, bool worldPositionStays = false)
        {
            child.transform.SetParent(null, worldPositionStays);
        }

        public static void SetParent(this GameObject child, bool worldPositionStays = false)
        {
            child.transform.SetParent(null, worldPositionStays);
        }

        public static void SetParent(this Component child, GameObject parent, bool worldPositionStays = false)
        {
            pSetParent(child.gameObject, parent.gameObject, worldPositionStays);
        }

        public static void SetParent(this GameObject child, GameObject parent, bool worldPositionStays = false)
        {
            pSetParent(child.gameObject, parent.gameObject, worldPositionStays);
        }

        public static void SetParent(this Component child, Component parent, bool worldPositionStays = false)
        {
            pSetParent(child.gameObject, parent.gameObject, worldPositionStays);
        }

        public static void SetParent(this GameObject child, Component parent, bool worldPositionStays = false)
        {
            pSetParent(child.gameObject, parent.gameObject, worldPositionStays);
        }

        private static void pSetParent(GameObject c, GameObject p, bool worldPositionStays)
        {
            c.transform.SetParent(p.transform, worldPositionStays);
        }

        public static void SetEnable(this Behaviour bh, bool enabled)
        {
            bh.enabled = enabled;
        }

        public static void SetEnable(this Component c, bool enabled)
        {
            if (c is Renderer)
            {
                (c as Renderer).enabled = enabled;
            }
            else if (c is Behaviour)
            {
                (c as Behaviour).enabled = enabled;
            }
            else if (c is Collider)
            {
                (c as Collider).enabled = enabled;
            }
            else
            {
                Assert.UnReachable();
            }
        }

        public static void SetEnable<T>(this Component component, bool enable)
        where T : Component
        {
            var c = component.GetComponent<T>();
            if (c is Renderer)
            {
                (c as Renderer).enabled = enable;
            }
            else if (c is Behaviour)
            {
                (c as Behaviour).enabled = enable;
            }
            else if (c is Collider)
            {
                (c as Collider).enabled = enable;
            }
            else
            {
                Assert.UnReachable();
            }
        }

        /// <returns>nullの場合も return false</returns>
        public static bool IsActive(this Behaviour obj)
        {
            return obj != null && obj.gameObject.GetActive();
        }

        /// <summary>
        /// UnityObjectはsceneから削除されているとnullを返します
        /// しかし参照は残ったままのことがあります
        /// ガベッジコレクションを走らせるためにこのメソッドが有用です
        public static bool IsTrueNull(this GameObject obj)
        {
            return (System.Object)obj == null;
        }

        public static bool IsDone(this PlayableDirector director)
        {
            if (director.extrapolationMode == DirectorWrapMode.Hold)
            {
                if (director.time >= director.duration)
                {
                    return true;
                }
            }

            return director.state == PlayState.Paused;
        }

        public static void SetActive(this Behaviour bh, bool flg)
        {
            if (bh.gameObject.activeSelf != flg)
            {
                bh.gameObject.SetActive(flg);
            }
        }

        /// <summary>Unity-version-independent replacement for active GO property.</summary>
        /// <returns>Unity 3.5: active. Any newer Unity: activeInHierarchy.</returns>
        public static bool GetActive(this GameObject target)
        {
#if UNITY_3_5
        return target.active;
#else
            return target.activeInHierarchy;
#endif
        }

#if UNITY_3_5
        /// <summary>Unity-version-independent setter for active and SetActive().</summary>
        public static void SetActive(this GameObject target, bool value)
        {
            target.active = value;
        }
#endif
        public static void SetAlpha(this Image image, float alpha)
        {
            var color = image.color;
            color.a = alpha;
            image.color = color;
        }

        public static void SetColor(this Image image, float rgb, float alpha)
        {
            image.color = new Color(rgb, rgb, rgb, alpha);
        }
    }
}
