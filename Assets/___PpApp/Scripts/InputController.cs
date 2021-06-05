using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

namespace PPD
{
    // プロジェクトごとに自由に変えちゃってね
    public class InputController : PPD_MonoBehaviour
    {
        Vector3 touchPos;
        void Update()
        {
            var dir3D = GetInputDirection();
            if (dir3D != Vector3.zero)
            {
                // どの向きに入力されたかを検査するなら…
                // if (dir3D.x > 0.7f)
                // {
                // 
                // }
                // else if (dir3D.x < -0.7f)
                // {
                // 
                // }
                // else if (dir3D.z < -0.7f)
                // {
                // 
                // }
                // else if (dir3D.z < -0.7f)
                // {
                // 
                // }

                // 歩行処理。　不要なら削除すること。
                Move(dir3D);
            }
        }

        public void Move(Vector3 dir3D)
        {
            transform.localPosition += dir3D * Time.deltaTime * Data.Ins.characterWalkSpeed;
            var a = transform.localRotation;
            var b = Quaternion.LookRotation(dir3D);
            var t = Data.Ins.characterAdjRotParameter * Time.deltaTime;
            transform.localRotation = Quaternion.Lerp(a, b, t);
        }

        bool FullInput(Vector3 v3) => v3.sqrMagnitude == 1;

        private Vector3 GetInputDirection()
        {
            var v = Vector3.zero;
            //Key
            {
                if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A))
                {
                    v.x -= 1;
                }
                if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D))
                {
                    v.x += 1;
                }
                if (Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.S))
                {
                    v.z -= 1;
                }
                if (Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.W))
                {
                    v.z += 1;
                }
            }

            //Pointer
            {
                if (Input.GetMouseButtonDown(0))
                {
                    touchPos = Input.mousePosition;
                }

                if (Input.GetMouseButton(0))
                {
                    var vector = Input.mousePosition - touchPos;
#if UNITY_EDITOR
                    vector *= Data.Ins.sensivityOnEditor;
#endif
                    var max = Screen.width * Data.Ins.fingerRange;
                    var ans = vector / max;
                    v.x += ans.x;
                    v.z += ans.y;
                }
            }

            if (v.sqrMagnitude > 1)
            {
                v.Normalize();
            }
            return v;
        }
    }
}

