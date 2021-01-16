using System;
using System.Collections.Generic;
using System.Linq;
using Sirenix.OdinInspector;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;

namespace PPD
{
    [Serializable]
    public class ED_SelectedSceneObject
    {
        [MenuItem("Pocketpair/ショートカット/アクティブを切り替え %&a")]
        private static void SwitchActive()
        {
            foreach (var o in Selection.gameObjects)
            {
                o.SetActive(!o.GetActive());
                Undo.RecordObject(o, "アクティブを切り替え");
            }
            if (!Application.isPlaying)
            {
                EditorSceneManager.MarkSceneDirty(EditorSceneManager.GetActiveScene());
            }
        }

        [MenuItem("Pocketpair/選択が１つのときだけ有効/親を選択 %w")]
        private static void SelectParent()
        {
            if (Selection.gameObjects.Length == 1)
            {
                var parent = Selection.gameObjects[0].transform.parent;
                if (parent != null)
                {
                    Selection.activeObject = parent;
                }
            }
        }

        [MenuItem("Pocketpair/選択が１つのときだけ有効/子を選択 %#w")]
        private static void SelectChild()
        {
            if (Selection.gameObjects.Length == 1)
            {
                var child = Selection.gameObjects[0].transform.GetChild(0);
                if (child != null)
                {
                    Selection.activeObject = child;
                }
            }
        }

        private static int _lastMenuCallTimestamp = 0;
        [MenuItem("GameObject/Create Empty 000", false, 0)]
        public static void CreateEmpty_0_0_0()
        {
            var currentTime = (int)EditorApplication.timeSinceStartup;
            //hack https://answers.unity.com/questions/608256/how-to-execute-menuitem-for-multiple-objects-once.html
            if (currentTime < _lastMenuCallTimestamp + 1) return;
            GameObject go = new GameObject("000");
            go.transform.position = new Vector3(0, 0, 0);
            _lastMenuCallTimestamp = currentTime;
            Undo.RegisterCreatedObjectUndo(go, "Generated 000");

            Selection.activeObject = go;
        }

        [MenuItem("GameObject/★便利メソッド★/選択中をまとめる(Ctrl+G) %g", false, 0)]
        public static void Group_Objects_Into_Centered_Empty()
        {
            if (Selection.objects.Length <= 0) return;
            var transforms = Selection.transforms;
            GameObject go = new GameObject("GroupedGameObject");
            float x = 0, y = 0, z = 0;
            Undo.RegisterCreatedObjectUndo(go, "Grouped");
            foreach (var transform in transforms)
            {
                x += transform.position.x;
                y += transform.position.y;
                z += transform.position.z;
            }
            go.transform.position = new Vector3(x, y, z) / transforms.Length;
            foreach (var trans in Selection.transforms)
            {
                Undo.SetTransformParent(trans, go.transform, "Grouped");
            }
            Selection.activeObject = go;
        }

        [MenuItem("GameObject/★便利メソッド★/選択中を000にまとめる(Shift+Ctrl+G) %#g", false, 0)]
        public static void Group_Objects_Into_Empty_0_0_0()
        {
            if (Selection.objects.Length <= 0) return;
            var transforms = Selection.transforms;
            GameObject go = new GameObject("000");
            go.transform.position = new Vector3(0, 0, 0);
            Undo.RegisterCreatedObjectUndo(go, "Grouped");
            foreach (var trans in Selection.transforms)
            {
                Undo.SetTransformParent(trans, go.transform, "Grouped");
            }
            Selection.activeObject = go;
        }

        [MenuItem("GameObject/★便利メソッド★/子をPositionでソート/+X", false, 0)]
        public static void SortByX() => SortBy(t => t.localPosition.x);

        [MenuItem("GameObject/★便利メソッド★/子をPositionでソート/+Y", false, 0)]
        public static void SortByY() => SortBy(t => t.localPosition.y);

        [MenuItem("GameObject/★便利メソッド★/子をPositionでソート/+Z", false, 0)]
        public static void SortByZ() => SortBy(t => t.localPosition.z);

        [MenuItem("GameObject/★便利メソッド★/子をPositionでソート/逆順/X", false, 0)]
        public static void SortBy_X() => SortBy(t => -t.localPosition.x);

        [MenuItem("GameObject/★便利メソッド★/子をPositionでソート/逆順/Y", false, 0)]
        public static void SortBy_Y() => SortBy(t => -t.localPosition.y);

        [MenuItem("GameObject/★便利メソッド★/子をPositionでソート/逆順/Z", false, 0)]
        public static void SortBy_Z() => SortBy(t => -t.localPosition.z);

        public static void SortBy(Func<Transform, float> func)
        {
            var o = SelectGameObjects();
            o.ForEach(e =>
            {
                Undo.RecordObject(e, "名前順にソート");
                e.transform.GetComponentsInChildren<Transform>()
                .OrderBy(func)
                .ForEach(t =>
                {
                    if (t != e.transform)
                    {
                        t.SetAsLastSibling();
                        EditorUtility.SetDirty(t);
                    }
                });
            });
        }

        [MenuItem("GameObject/★便利メソッド★/子を名前順にソート", false, 0)]
        public static void SortSiblingByName()
        {
            var o = SelectGameObjects();
            o.ForEach(e =>
            {
                Undo.RecordObject(e, "名前順にソート");
                e.transform.GetComponentsInChildren<Transform>()
                .OrderBy(t => t.name)
                .ForEach(t =>
                {
                    if (t != e.transform)
                    {
                        t.SetAsLastSibling();
                    }
                });
            });
        }

        [BoxGroup("等間隔で並べる"), HideLabel] public Vector3 eachDistance;

        [BoxGroup("等間隔で並べる")]
        [Button("GO")]
        public void SortPosition()
        {
            var o = SelectGameObjects();

            var basePosition = o[0].transform.localPosition;
            for (int i = 0; i < o.Length; i++)
            {
                o[i].transform.localPosition = basePosition + eachDistance * i;
            }
        }

        [BoxGroup("同じ名前で番号を振る"), LabelText("名前")] public string number_name;
        [BoxGroup("同じ名前で番号を振る"), LabelText("0埋めする")] public bool number_zero;

        [BoxGroup("同じ名前で番号を振る")]
        [Button("GO")]
        public void WriteNumbers()
        {
            var o = SelectGameObjects();

            if (number_zero)
            {
                if (o.Length >= 100)
                {
                    for (int i = 0; i < o.Length; i++)
                    {
                        o[i].name = $"{number_name} ({i:000})";
                    }
                }
                {

                    for (int i = 0; i < o.Length; i++)
                    {
                        o[i].name = $"{number_name} ({i:00})";
                    }
                }
            }
            else
            {
                for (int i = 0; i < o.Length; i++)
                {
                    o[i].name = $"{number_name} ({i})";
                }
            }
        }

        public static GameObject[] SelectGameObjects()
        {
            // return Selection.gameObjects;
            var o = Selection.gameObjects.Select(e => e as GameObject);

            if (o.Any(e => e == null))
            {
                ST_Pop.ED_Error("GameObjectを選択してください");
            }

            return o.ToArray();
        }
    }
}
