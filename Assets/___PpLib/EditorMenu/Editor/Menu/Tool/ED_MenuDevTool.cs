using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.Reflection;

namespace Oc
{
    public class ED_MenuDevTool : Editor
    {
        private static Object[] selectionObjects;

        /*
        % : CTRL (Windows) または command (OSX)
        # : Shift
        & : Altキー
        LEFT/RIGHT/UP/DOWN : 方向キー
        F1…F2 : Fキー
        HOME, END, PGUP, PGDN : HOME、END, PageUp, PageDown

        Overcraft/ExampleLog #%1 は Shift + Ctrl + 1
        */
        [MenuItem("Pocketpair/Tools/RefreshMaterial #%SPACE")]
        private static void RefreshMaterial()
        {
            AssignNullObject();
            EditorApplication.delayCall += AssignOriginalSelection;
        }

        [MenuItem("Pocketpair/Tools/Capture")]
        private static void CaptureScreenshot()
        {
            var now = System.DateTime.Now.ToString("yyyyMMdd-HHmmss-fff");

            ScreenCapture.CaptureScreenshot($"Capture/{now}.png");
        }

        private static void AssignNullObject()
        {
            //Debug.Log("OC: AssignNullObject");
            selectionObjects = Selection.objects;
            Selection.objects = new Object[0];
        }
        private static void AssignOriginalSelection()
        {
            Debug.Log("OC: Material Refreshed");
            Selection.objects = selectionObjects;
            EditorApplication.delayCall -= AssignOriginalSelection;
        }

        [MenuItem("Pocketpair/Tools/Trigger Recompile %T")]
        private static void TriggerRecompile()
        {
            GenerateDummyScript();
            AssetDatabase.Refresh();
        }

        [MenuItem("Pocketpair/Tools/Clear Console %&#C")]
        private static void ClearConsole()
        {
            var assembly = Assembly.GetAssembly(typeof(SceneView));
            var type = assembly.GetType("UnityEditor.LogEntries");
            var method = type.GetMethod("Clear");
            method.Invoke(new object(), null);
        }

        static readonly string SAVE_FILE_POINT = "/___PpApp/zzzTemp/";
        private static void GenerateDummyScript()
        {
            string assetPath = Application.dataPath + SAVE_FILE_POINT + "ForceRecompileDummy.cs";
            var textScript = $@"
                namespace Oc
                {{
                    public class ForceRecompileDummyDayo
                    {{
                        // ここはランダムで毎回変わるよ {Random.Range(0, 10000)}
                    }}
                }}
            ";
            System.IO.File.WriteAllText(assetPath, textScript);
        }
    }
}