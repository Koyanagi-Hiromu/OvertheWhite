using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using I2.Loc;
using Sirenix.OdinInspector;
using UnityEditor;
using UnityEngine;
namespace SR
{
    /// <summary>
    /// extentionでない便利なメソッド群です
    /// </summary>
    public static class Util
    {
        public static string ToSymbol(this bool flg) => flg ? "Ｏ" : "Ｘ";
        //------------------------------
        // Math系 (Vector含む)
        //------------------------------

        public static Vector3 Lerp(Vector3 a, Vector3 b, float t)
        {
            return new Vector3()
            {
                x = Util.Lerp(a.x, b.x, t),
                y = Util.Lerp(a.y, b.y, t),
                z = Util.Lerp(a.z, b.z, t),
            };
        }

        public static Vector4 Lerp(Vector4 a, Vector4 b, float t)
        {
            return new Vector4()
            {
                x = Util.Lerp(a.x, b.x, t),
                y = Util.Lerp(a.y, b.y, t),
                z = Util.Lerp(a.z, b.z, t),
                w = Util.Lerp(a.w, b.w, t),
            };
        }

        public static float Lerp(float a, float b, float t)
        {
            return a + (b - a) * t;
        }

        public static Vector2 MakeRandomVec2()
        {
            return MakeVec2ForDeg(RandomDegree());
        }

        public static Vector2 MakeVec2ForRad(float rad)
        {
            return new Vector2(Mathf.Cos(rad), Mathf.Sin(rad));
        }

        public static Vector2 MakeVec2ForDeg(float degree)
        {
            return MakeVec2ForRad(Mathf.Deg2Rad * degree);
        }

        /// <returns> 0 - 359</returns>
        public static float RandomDegree()
        {
            return UnityEngine.Random.Range(0, 360);
        }

        //------------------------------
        // UnityEngine系
        //------------------------------
        private static GameObject pCreateFolder(string name, GameObject parentObject)
        {
            var folder = new GameObject(name);
            if (parentObject)
            {
                folder.transform.parent = parentObject.transform;
            }
            return folder;
        }

        /// <summary>
        /// 空のGameObjectを生成します。
        /// フォルダーやnamespaceとしてのみ利用してください。
        /// 生成コストが気になるので、シーンの読み込み時まで(Awake - Start)に呼ぶこと。
        /// </summary>
        /// <param name="name">フォルダの名前</param>
        /// <param name="parentMonoBehaviour">フォルダの位置 シーン直下に置く場合は　CreateFolderOnTop をコールしてください。</param>
        /// <returns>生成されたfolder</returns>
        public static GameObject CreateFolder(string name, GameObject parentObject)
        {
            if (parentObject == null)
            {
                return CreateFolderOnTop(name);
            }
            else
            {
                string prosseccedName = "*[" + name + "]";
                return pCreateFolder(prosseccedName, parentObject);
            }
        }

        public static GameObject CreateFolderOnTop(string name)
        {
            return pCreateFolder("*[" + name + "]", null);
        }

        public static GameObject CreateFolderOnTopIfNotFound(string name)
        {
            name = "*[" + name + "]";

            var folder = GameObject.Find(name);
            if (folder)
            {
                return folder;
            }
            else
            {
                return pCreateFolder(name, null);
            }
        }

        public static string ToF1(this float f)
        {
            var str = f.ToString("F1");
            if (!str.Contains("."))
            {
                return str + ".0";
            }
            else
            {
                return str;
            }
        }

        public static string ToF2(this float f)
        {
            var str = f.ToString("F2");
            if (!str.Contains("."))
            {
                return str + ".00";
            }
            else
            {
                return str;
            }
        }

        public static string ToF1(this double f)
        {
            var str = f.ToString("F1");
            if (!str.Contains("."))
            {
                return str + ".0";
            }
            else
            {
                return str;
            }
        }

        #region 未分類
        public static string I2(string text, string overrideLanguage = null)
        {
            return LocalizationManager.GetTranslation(text, overrideLanguage: overrideLanguage);
        }

        public static void GradeAction<V>(this Dictionary<int, V> dictionary, int key, Action<V> action)
        {
            var restriction = dictionary.Where(pair => pair.Key <= key);
            if (restriction.Empty())
            {
                return;
            }
            else
            {
                action(restriction.FindMax(pair => pair.Key).Value);
            }
        }

        #endregion
        /// <summary>
        /// ユニティエディタ上でしか動かないので注意
        /// </summary>
        public static void PopError(string message)
        {
#if UNITY_EDITOR
            EditorUtility.DisplayDialog("エラー!", $"{message}\n\nウェレイに連絡して下さい", "OK");
            SLog.Check.Error(message);
#endif
        }

        /// <summary>
        /// ユニティエディタ上でしか動かないので注意
        /// </summary>
        public static void PopMessage(string message)
        {
#if UNITY_EDITOR
            EditorUtility.DisplayDialog("メッセージ", message, "OK");
#endif
        }

        /// <summary>
        /// ユニティエディタ上でしか動かないので注意
        /// </summary>
        public static void PopMessage(string title, string nameStr, string ok = "OK")
        {
#if UNITY_EDITOR
            SLog.Check.Info($"{title}\n{nameStr}");
            EditorUtility.DisplayDialog(title, nameStr, ok);
#endif
        }

        public static IEnumerable ValueDropdown<T>(string path)
        where T : UnityEngine.Object
        {
#if UNITY_EDITOR
            return NonResources.LoadAll<T>(path).Select(x => new ValueDropdownItem(x.name, x));
#else
            return null;
#endif
        }

        public static string GetFileName(UnityEngine.Object selectedObject)
         => GetFileName(selectedObject.name);

        public static string GetFileName(Type t)
        {
            var n = t.ToString();
            return GetFileName(n.Substring(n.LastIndexOf(".") + 1));
        }

        public static string GetFileName(string n)
        {
            int i;
            i = n.LastIndexOf("Source");
            if (i > 0)
            {
                n = n.Substring(0, i);
            }
            i = n.LastIndexOf("SO");
            if (i > 0)
            {
                n = n.Substring(0, i);
            }

            if (n.EndsWith("_"))
            {
                n = n.Substring(0, n.Length - 1);
            }

            return $"so--{n}";
        }
    }
}
