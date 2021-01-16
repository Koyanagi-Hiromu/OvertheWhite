using Sirenix.OdinInspector.Editor;
using System.Linq;
using UnityEngine;
using Sirenix.Utilities.Editor;
using Sirenix.Serialization;
using UnityEditor;
using Sirenix.Utilities;
using Sirenix.OdinInspector;

namespace PPD
{
    // OdinMenuEditorWindowExample.cs を見ようね
    public class ED_MainMenu : OdinMenuEditorWindow
    {
        class DebugTest
        {
            [Button]
            void TestButton()
            {
                Debug.Log("test dayo");
            }
        }

        [MenuItem("Pocketpair/汎用デバッグ")]
        private static void OpenWindow()
        {
            var window = GetWindow<ED_MainMenu>("ED_MainMenu");
        }

        protected override OdinMenuTree BuildMenuTree()
        {
            var tree = new OdinMenuTree(supportsMultiSelect: true)
            {
                { "デバッグテスト", new DebugTest(), EditorIcons.Car },
                { "選択中オブジェクト", new ED_SelectedSceneObject(), EditorIcons.ArrowDown },
            };

            return tree;
        }

        // Odin用のデバッグメニューを置いてる
        [MenuItem("Tools/Odin Inspector/Demos/EditorIconsOverview")]
        private static void OpenWindow2()
        {
            EditorIconsOverview.OpenEditorIconsOverview();
        }
    }
}
