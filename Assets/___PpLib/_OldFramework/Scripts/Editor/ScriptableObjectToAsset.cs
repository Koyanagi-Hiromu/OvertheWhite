using UnityEngine;
using UnityEditor;
using System.IO;

/// <summary>
// ScriptableObjectをプレハブとして出力する汎用スクリプト  
/// </summary>
// <remarks>
// 指定したScriptableObjectをプレハブに変換する。
// 1.Editorフォルダ下にCreateScriptableObjectPrefub.csを配置  
// 2.ScriptableObjectのファイルを選択して右クリック→Create ScriptableObjectを選択  
// </remarks>
public class ScriptableObjectToAsset
{
    readonly static string[] labels = { "Data", "ScriptableObject", string.Empty };

    [MenuItem("Assets/Create/ScriptableObject", priority = 1)]
    static void Create()
    {
        foreach (var selectedObject in Selection.objects)
        {
            // create instance
            var obj = ScriptableObject.CreateInstance(selectedObject.name);
            var n = SR.Util.GetFileName(selectedObject);

            // get path
            var path = getSavePath(selectedObject, n);
            AssetDatabase.CreateAsset(obj, path);
            labels[2] = n;
            // add label
            var sobj = AssetDatabase.LoadAssetAtPath<ScriptableObject>(path);
            AssetDatabase.SetLabels(sobj, labels);
            EditorUtility.SetDirty(sobj);
            EditorGUIUtility.PingObject(sobj);
        }
    }

    public static T Create<T>(string className, string file, string name)
    where T : ScriptableObject
    {
        var path = $"{file}{name}.asset";

        if (File.Exists(path))
        {
            return AssetDatabase.LoadAssetAtPath<T>(path);
        }

        var obj = ScriptableObject.CreateInstance(className);
        AssetDatabase.CreateAsset(obj, path);

        labels[2] = name;
        // add label
        var sobj = AssetDatabase.LoadAssetAtPath<T>(path);
        AssetDatabase.SetLabels(sobj, labels);
        EditorUtility.SetDirty(sobj);
        EditorGUIUtility.PingObject(sobj);

        return sobj;
    }

    static string getSavePath(Object selectedObject, string name)
    {
        var dirPath = Path.GetDirectoryName(AssetDatabase.GetAssetPath(selectedObject));
        var SOPath = dirPath.Substring(0, dirPath.LastIndexOf('\\') + 1) + "SO";
        var path = string.Format("{0}/{1}.asset", Directory.Exists(SOPath) ? SOPath : dirPath, name);

        if (File.Exists(path))
            for (var i = 1; ; i++)
            {
                path = string.Format("{0}/{1} ({2}).asset", dirPath, name, i);
                if (!File.Exists(path))
                    break;
            }

        return path;
    }

    // public static string GetFileName(UnityEngine.Object selectedObject) => GetFileName(selectedObject.name);
    // public static string GetFileName(string n) => Oc.StringExtensions.GetFileName(n);
}
