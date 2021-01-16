using UnityEngine;
using System;
using System.Collections.Generic;
using System.IO;

namespace PPD
{
    public enum Log
    {
        Check,
    }
    public static class EX_Log
    {
        public static bool isLogEnabled = true;
        public static List<string> profile = new List<string>();

        public static void LoggingToFile(string msg)
        {
            var path = Application.persistentDataPath + "/DebugLog.txt";
            var sw = new StreamWriter(path, true);
            msg = "[" + DateTime.Now + "]" + msg;
            sw.WriteLine(msg);
            sw.Close();
        }

        [System.Diagnostics.Conditional("DEVELOPMENT_BUILD"), System.Diagnostics.Conditional("UNITY_EDITOR")]
        public static void Watch(this Log type, object obj, object text)
        {
            if (!isLogEnabled) return;

            ConsoleProDebug.Watch($"{type}: {obj}", text.ToString());
        }

        [System.Diagnostics.Conditional("DEVELOPMENT_BUILD"), System.Diagnostics.Conditional("UNITY_EDITOR")]
        public static void Info(this Log type, string[,] array, string subType = "")
        {
            if (!isLogEnabled) return;

            var str = $" {array.GetLength(0)}\n";
            for (int y = 0; y < array.GetLength(1); y++)
            {
                str += $"{y:000}: ";
                for (int x = 0; x < array.GetLength(0); x++)
                {
                    var word = array[x, y];
                    word = word.IsNullOrEmpty() ? "　" : word;
                    str += $"{word}, ";
                }
                str += "\n";
            }
            Info(type, str, subType);
        }

        [System.Diagnostics.Conditional("DEVELOPMENT_BUILD"), System.Diagnostics.Conditional("UNITY_EDITOR")]
        public static void Info(this Log type, object obj, string subType = "")
        {
            if (!isLogEnabled) return;

            var msg = message(type, obj, subType);
            Debug.Log(msg);
            LoggingToFile(msg);
        }

        [System.Diagnostics.Conditional("DEVELOPMENT_BUILD"), System.Diagnostics.Conditional("UNITY_EDITOR")]
        public static void Warning(this Log type, object obj, string subType = "")
        {
            if (!isLogEnabled) return;

            var msg = message(type, obj, subType);
            Debug.LogWarning(msg);
            LoggingToFile(msg);
        }

        /// <summary>
        /// 本番でもログ出す
        /// </summary>
        public static void ForceInfo(this Log type, object obj, string subType = "")
        {
            var msg = message(type, obj, subType);
            Debug.Log(msg);
        }

        /// <summary>
        /// isLogEnabledを無視する
        /// </summary>
        [System.Diagnostics.Conditional("DEVELOPMENT_BUILD"), System.Diagnostics.Conditional("UNITY_EDITOR")]
        public static void Insist(this Log type, object obj, string subType = "")
        {
            var msg = message(type, obj, subType);
            Debug.Log(msg);
        }

        public static void Error(this Log type, object obj, string subType = "")
        {
            var msg = message(type, obj, subType);
            Debug.LogError(msg);
        }

        private static string message(Log type, object obj, string subType)
        {
            string msg = header(type, subType);
            msg += " " + ToString(obj);
            return msg;
        }

        private static string header(Log type, string subType = "", string subKey = "")
        {
            var str = "[Splove][" + type.ToString() + "]" + subKey;
            if (subType.IsNullOrEmpty() == false)
            {
                str += "[" + subType + "]";
            }

            return str;
        }

        private static bool IsNull<T>(T obj) where T : class
        {
            var unityObj = obj as UnityEngine.Object;
            if (!object.ReferenceEquals(unityObj, null))
            {
                return unityObj == null;
            }
            else
            {
                return obj == null;
            }
        }

        private static string ToString(object obj)
        {
            // <color=yellow>黄色のログ</color>
            if (obj == null)
            {
                return "(t)null";
            }

            return obj.ToString();
        }
    }
}
