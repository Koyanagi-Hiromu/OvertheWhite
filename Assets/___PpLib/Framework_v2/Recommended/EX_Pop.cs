using System.Diagnostics;
using UnityEditor;

namespace PPD
{
    public static class EX_Pop
    {
        /// <summary>
        /// ユニティエディタ上でしか動かないので注意
        /// </summary>
        [Conditional("DEVELOPMENT_BUILD"), Conditional("UNITY_EDITOR")]
        public static void ED_Error(string message, string person = "ウェレイ")
        {
#if UNITY_EDITOR
            EditorUtility.DisplayDialog("エラー!", $"{message}\n\n{person}に連絡して下さい", "OK");
            Log.Check.Error(message);
#endif
        }

        /// <summary>
        /// ユニティエディタ上でしか動かないので注意
        /// </summary>
        [Conditional("DEVELOPMENT_BUILD"), Conditional("UNITY_EDITOR")]
        public static void ED_Message(string message)
        {
#if UNITY_EDITOR
            EditorUtility.DisplayDialog("メッセージ", message, "OK");
#endif
        }

        /// <summary>
        /// ユニティエディタ上でしか動かないので注意
        /// </summary>
        [Conditional("DEVELOPMENT_BUILD"), Conditional("UNITY_EDITOR")]
        public static void ED_Message(string title, string nameStr, string ok = "OK")
        {
#if UNITY_EDITOR
            Log.Check.Info($"{title}\n{nameStr}");
            EditorUtility.DisplayDialog(title, nameStr, ok);
#endif
        }
    }
}
