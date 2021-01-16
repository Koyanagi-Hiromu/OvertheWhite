using UnityEngine;
using System;
using System.Collections.Generic;
using System.IO;

namespace SR
{
    public static class SDebug
    {
        public static bool isLogEnabled = true;
        public static bool isAllActionEndTracker;
        public static List<string> profile = new List<string>();

        public static void NOT_END_YET()
        {
            if (isAllActionEndTracker)
            {
                SLog.Event.Insist("Not end yet");
            }
        }

        public static void CProfile(this Type type, params object[] values)
        {
            SLog.Check.Profile(type.Name, values);
        }

        public static void CProfile(this object obj, params object[] values)
        {
            SLog.Check.Profile(obj.GetType().Name, values);
        }

        public static void ProfileTitle(this SLog type, string subType, params string[] titles)
        {
            if (!isLogEnabled) return;

            if (profile.Contains(subType))
            {
                return;
            }
            profile.Add(subType);

            string msg = header(type, subType, "[Profile]");
            titles.ForEach((title) =>
            {
                msg += "|" + title;
            });
            msg += "|";
            Debug.Log(msg);
        }

        public static void Profile(this SLog type, string subType, params object[] values)
        {
            if (!isLogEnabled) return;

            int textMinLength = 6;
            string format = "0.00";
            var msg = header(type, subType, "[Profile]");
            values.ForEach((value) =>
            {
                string x;

                if (value is float)
                {
                    x = Convert.ToSingle(value).ToString(format);
                }
                else if (value is double)
                {
                    x = Convert.ToDouble(value).ToString(format);
                }
                else
                {
                    x = value.ToString();
                }

                var fmat = "{0, " + textMinLength.ToString() + "}";
                msg += "|" + String.Format(fmat, x);
            });
            msg += "|";
            Debug.Log(msg);
        }

        public static void LoggingToFile(string msg)
        {
            var path = Application.persistentDataPath + "/DebugLog.txt";
            var sw = new StreamWriter(path, true);
            msg = "[" + DateTime.Now + "]" + msg;
            sw.WriteLine(msg);
            sw.Close();
        }

        [System.Diagnostics.Conditional("DEVELOPMENT_BUILD"), System.Diagnostics.Conditional("UNITY_EDITOR")]
        public static void Watch(this SLog type, object obj, object text)
        {
            if (!isLogEnabled) return;

            ConsoleProDebug.Watch($"{type}: {obj}", text.ToString());
        }

        [System.Diagnostics.Conditional("DEVELOPMENT_BUILD"), System.Diagnostics.Conditional("UNITY_EDITOR")]
        public static void Info(this SLog type, string[,] array, string subType = "")
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
        public static void Info(this SLog type, object obj, string subType = "")
        {
            if (!isLogEnabled) return;

            var msg = message(type, obj, subType);
            Debug.Log(msg);
            LoggingToFile(msg);
        }

        [System.Diagnostics.Conditional("DEVELOPMENT_BUILD"), System.Diagnostics.Conditional("UNITY_EDITOR")]
        public static void Warning(this SLog type, object obj, string subType = "")
        {
            if (!isLogEnabled) return;

            var msg = message(type, obj, subType);
            Debug.LogWarning(msg);
            LoggingToFile(msg);
        }

        /// <summary>
        /// 本番でもログ出す
        /// </summary>
        public static void ForceInfo(this SLog type, object obj, string subType = "")
        {
            var msg = message(type, obj, subType);
            Debug.Log(msg);
        }

        /// <summary>
        /// isLogEnabledを無視する
        /// </summary>
        [System.Diagnostics.Conditional("DEVELOPMENT_BUILD"), System.Diagnostics.Conditional("UNITY_EDITOR")]
        public static void Insist(this SLog type, object obj, string subType = "")
        {
            var msg = message(type, obj, subType);
            Debug.Log(msg);
        }

        public static void Error(this SLog type, object obj, string subType = "")
        {
            var msg = message(type, obj, subType);
            Debug.LogError(msg);
        }

        private static string message(SLog type, object obj, string subType)
        {
            string msg = header(type, subType);
            msg += " " + ToString(obj);
            return msg;
        }

        private static string header(SLog type, string subType = "", string subKey = "")
        {
            var str = "[Splove][" + type.ToString() + "]" + subKey;
            if (subType.Empty() == false)
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

    /// <summary>
    /// Splove Log
    /// </summary>
    public enum SLog
    {
        Production,
        Event,
        Temporary,
        Floor,
        Check,
        Say,
        Audio,
        Camera,
        Animation,
        AC,
        Collision,
        System,
        Editor,
    }
}
