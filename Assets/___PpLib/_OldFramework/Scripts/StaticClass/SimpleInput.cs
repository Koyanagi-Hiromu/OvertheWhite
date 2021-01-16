using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SR
{
    public static class SimpleInput
    {
        public static bool GetDown()
        {
            // This method reacts with also Screen Touch event 
            if (Input.GetMouseButtonDown(0))
            {
                return true;
            }
            return false;
        }
        /// <summary>
        /// Canvasに対応していないことに注意
        /// （Canvasは画面にストレッチしてサイズを合わせている）
        /// </summary>
        /// <returns></returns>
        public static Vector3 GetScreenPoint()
        {
            return GetLocation();
        }

        public static Vector3 GetPlanePoint()
        {
            Ray ray = Camera.main.ScreenPointToRay(SimpleInput.GetLocation());

            var v1 = ray.direction;
            var v2 = Vector3.down;
            Vector3.Angle(v1, v2);

            return ray.GetPoint(ray.origin.y);
        }

        private static Vector3 GetLocation()
        {
#if ((UNITY_ANDROID || UNITY_IOS || UNITY_WINRT || UNITY_BLACKBERRY) && !UNITY_EDITOR)
            var i = 0;
            return Input.GetTouch(i).position;
#else
            return Input.mousePosition;
#endif
        }
        // public static Vector3 GetLocation()
        // {
        //     RaycastHit hit;
        //     Ray ray = Camera.main.ScreenPointToRay(Input.touches[0].position);
        //     if (Physics.Raycast(ray, out hit))
        //     {
        //         Transform objectHit = hit.transform;
        //         // Do something with the object that was hit by the raycast.
        //         // Debug.Log("ray:" + hit.point);
        //         transform.position = new Vector3(transform.position.x, 0.13f, transform.position.z);
        //         // endPos = new Vector3 (hit.point.x, 0.1f, hit.point.z);
        //         endPos.Set(hit.point.x, 0.13f, hit.point.z);
        //     }
        // }
        public static bool GetActive()
        {
            return Input.touchCount > 0 || Input.GetMouseButton(0);
        }
        public static bool GetUp()
        {
            // This method reacts with also Screen Touch event 
            // This method returns true AFTER touch ended;
            // -> only if GetMouButton(0) is false
            if (Input.GetMouseButtonUp(0))
            {
                return true;
            }
            return false;
        }
    }
}