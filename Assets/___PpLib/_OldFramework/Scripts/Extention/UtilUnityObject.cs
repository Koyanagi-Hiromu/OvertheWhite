using System.Collections;
using UnityEngine;
using System;
using UnityEngine.Playables;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace SR
{
    public static class UtilUnityObject
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

        /// <summary>
        /// timeScaleの影響を受けます。
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="delaySec"></param>
        /// <param name="action"></param>
        public static Coroutine DelayCall(this MonoBehaviour obj, float delaySec, Action action)
        {
            if (!obj.IsActive())
            {
                SLog.Check.Info("obj.DelayCall でobjが非アクティブ");
                return null;
            }
            else if (delaySec <= 0)
            {
                action();
                return null;
            }
            else
            {
                return obj.StartCoroutine(DelayCall(delaySec, action));
            }
        }

        /// <summary>
        /// timeScaleの影響を受けます。
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="delaySec"></param>
        /// <param name="action"></param>
        public static Coroutine DelayCall(this MonoBehaviour obj, WaitForSeconds wfs, Action action)
        {
            if (!obj.IsActive())
            {
                SLog.Check.Info("obj.DelayCall でobjが非アクティブ");
                return null;
            }
            else
            {
                return obj.StartCoroutine(DelayCall(wfs, action));
            }
        }

        public static void DelayCall_1F(this MonoBehaviour obj, Action action)
        {
            obj.StartCoroutine(DelayCall_1F(action));
        }

        public static void DelayCall_2F(this MonoBehaviour obj, Action action)
        {
            obj.StartCoroutine(DelayCall_2F(action));
        }

        /// <summary>
        /// timeScaleの影響を受けます。
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="delaySec"></param>
        /// <param name="action"></param>
        /// 
        public static IEnumerator DelayCall(float delaySec, Action action)
        {
            yield return new WaitForSeconds(delaySec);
            action();
        }

        /// <summary>
        /// timeScaleの影響を受けます。
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="delaySec"></param>
        /// <param name="action"></param>
        /// 
        public static IEnumerator DelayCall(WaitForSeconds wfs, Action action)
        {
            yield return wfs;
            action();
        }

        public static IEnumerator ErRealtimeDelayCall(float delaySec, Action action)
        {
            yield return new WaitForSecondsRealtime(delaySec);
            action();
        }

        public static IEnumerator DelayCall_1F(Action action)
        {
            yield return null;
            action();
        }

        public static IEnumerator DelayCall_2F(Action action)
        {
            yield return null;
            yield return null;
            action();
        }

        public static IEnumerator WaitUntil(Func<bool> func, Action endAction)
        {
            if (func != null)
            {
                func.Compress();
                yield return new WaitUntil(func);
            }
            if (endAction != null)
            {
                endAction();
            }
        }

        public static void WaitUntil(this MonoBehaviour obj, Func<bool> func, Action endAction)
        {
            obj.StartCoroutine(WaitUntil(func, endAction));
        }

        public static void WaitUntil(this MonoBehaviour obj, Action whileAction, Func<bool> func, Action endAction = null)
        {
            Func<bool> condition = () =>
            {
                whileAction();
                return func();
            };
            obj.StartCoroutine(WaitUntil(condition, endAction));
        }

        public static void SetLocalPosition(this GameObject obj, Vector3 position)
        {
            obj.transform.localPosition = position;
        }

        public static void SetLocalPosition(this Behaviour obj, Vector3 position)
        {
            obj.transform.localPosition = position;
        }

        public static void SetLocalPosition(this Transform transform, Vector3 position)
        {
            transform.localPosition = position;
        }

        public static void SetGlobalPosition(this Behaviour obj, Vector3 position)
        {
            obj.transform.position = position;
        }

        public static void SetGlobalPosition(this GameObject obj, Vector3 position)
        {
            obj.transform.position = position;
        }

        public static void SetGlobalPosition(this Transform transform, Vector3 position)
        {
            transform.position = position;
        }

        public static Vector3 GetLocalPosition(this GameObject obj)
        {
            return obj.transform.localPosition;
        }

        public static Vector3 GetLocalPosition(this Behaviour obj)
        {
            return obj.transform.localPosition;
        }

        public static Vector3 GetLocalPosition(this Transform transform)
        {
            return transform.localPosition;
        }

        public static Vector3 GetGlobalPosition(this Behaviour obj)
        {
            return obj.transform.position;
        }

        public static Vector3 GetGlobalPosition(this GameObject obj)
        {
            return obj.transform.position;
        }

        public static Vector3 GetGlobalPosition(this Transform transform)
        {
            return transform.position;
        }

        public static Vector3 GetPosition(this Transform transform, bool global)
        {
            return global ? transform.position : transform.localPosition;
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

        // http://answers.unity3d.com/questions/1013011/convert-recttransform-rect-to-screen-space.html?sort=newest
        public static Rect RectTransformToScreenSpace(RectTransform transform)
        {
            Vector2 size = Vector2.Scale(transform.rect.size, transform.lossyScale);
            var x = transform.position.x;
            var y = Screen.height - transform.position.y;

            x -= (transform.pivot.x * size.x);
            y -= ((1.0f - transform.pivot.y) * size.y);

            Rect rect = new Rect(x, y, size.x, size.y);
            return rect;
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

        /// <summary>
        /// Logical OR.
        /// </summary>
        /// <returns>a != null ? a : b;</returns>
        public static GameObject LOR(this GameObject a, GameObject b)
        {
            return a != null ? a : b;
        }

        /// <summary>
        /// Logical OR.
        /// </summary>
        /// <returns>a != null ? a : b;</returns>
        public static string LOR(this string a, string b)
        {
            return !a.IsNullOrEmpty() ? a : b;
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

        public static void WaitTimelineFinished(this MonoBehaviour obj, PlayableDirector director, Action onComplete)
        {
            obj.WaitUntil(director.IsDone, onComplete);
        }

        public static void ExecuteEvents<T>(this Behaviour bhv, Action<T> functor)
        where T : IEventSystemHandler
        {
            bhv.gameObject.ExecuteEvents(functor);
        }

        public static void ExecuteEvents<T>(this GameObject gameObject, Action<T> functor)
        where T : IEventSystemHandler
        {
            gameObject.SetActive(true);
            UnityEngine.EventSystems.ExecuteEvents.Execute<T>(
                target: gameObject,
                eventData: null,
                functor: (handler, eventData) => functor(handler));
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
