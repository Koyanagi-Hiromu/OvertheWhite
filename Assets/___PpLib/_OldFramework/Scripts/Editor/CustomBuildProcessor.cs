#if UNITY_IOS || UNITY_ANDROID || SMARTPHONE_TEST
#define SMARTPHONE
#endif

using System.IO;
using UnityEngine;
using UnityEditor;
using UnityEditor.Build;
using UnityEditor.Build.Reporting;
using SR;

#if UNITY_IOS
using UnityEditor.iOS.Xcode;
#endif

class CustomBuildProcessor : IPreprocessBuildWithReport, IPostprocessBuildWithReport
{
    public int callbackOrder { get { return 0; } }

#if SMARTPHONE
    public void OnPreprocessBuild(BuildReport report)
    {
        Debug.Log($"CustomBuildProcessor.OnPreprocessBuild for target {report.summary.platform} at path {report.summary.outputPath}");

#if ZH
        Debug.Log("PlayerSettings.productName = \"超载物語\"");
        
        PlayerSettings.productName = "超载物語";
#endif

//         var data = AssetDatabase.LoadAssetAtPath("Assets/_SR/VariantAsset/ReplaceAssets.asset", typeof(ReplaceAssets)) as ReplaceAssets;

//         foreach (var asset in data.assets)
//         {
//             Debug.Log(AssetDatabase.GetAssetPath(asset.dest));

// #if ZH
//             Debug.Log(AssetDatabase.GetAssetPath(asset.spZh));

//             FileUtil.ReplaceFile(AssetDatabase.GetAssetPath(asset.spZh), AssetDatabase.GetAssetPath(asset.dest));
// #else
//             Debug.Log(AssetDatabase.GetAssetPath(asset.sp));

//             FileUtil.ReplaceFile(AssetDatabase.GetAssetPath(asset.sp), AssetDatabase.GetAssetPath(asset.dest));
// #endif

//             AssetDatabase.ImportAsset(AssetDatabase.GetAssetPath(asset.dest), ImportAssetOptions.ForceUpdate);
//         }

//         AssetDatabase.SaveAssets();
    }

#if UNITY_IOS
	public void OnPostprocessBuild(BuildReport report)
    {
        var path = report.summary.outputPath;

        var pjPath = PBXProject.GetPBXProjectPath(path);
        var pj = new PBXProject();

        pj.ReadFromString(File.ReadAllText(pjPath));

        string target = pj.TargetGuidByName("Unity-iPhone");

        pj.SetBuildProperty(target, "ENABLE_BITCODE", "NO");

        File.WriteAllText(pjPath, pj.WriteToString());

        AddUrlScheme(path, "overstory");
    }
    
    private static void AddUrlScheme(string path, string scheme)
    {
        Debug.Log($"path:{path}");
        Debug.Log($"scheme:{scheme}");

        var pathPlist = Path.Combine(path, "Info.plist");
        var document = new PlistDocument();

        document.ReadFromFile(pathPlist);

        var urlTypes = document.root.CreateArray("CFBundleURLTypes");

        var dict = urlTypes.AddDict();

        // dict.SetString("CFBundleURLName", "test_name");
        var urlSchemes = dict.CreateArray("CFBundleURLSchemes");
        
        urlSchemes.AddString(scheme);

        document.WriteToFile(pathPlist);
    }
#else
	public void OnPostprocessBuild(BuildReport report) { }
#endif
#else
    public void OnPreprocessBuild(BuildReport report)
    {
        Debug.Log($"CustomBuildProcessor.OnPreprocessBuild for target {report.summary.platform} at path {report.summary.outputPath}");

#if ZH
        Debug.Log("PlayerSettings.productName = \"超载物語\"");
        
        PlayerSettings.productName = "超载物語";
#endif

//         var data = AssetDatabase.LoadAssetAtPath("Assets/_SR/VariantAsset/ReplaceAssets.asset", typeof(ReplaceAssets)) as ReplaceAssets;

//         foreach (var asset in data.assets)
//         {
//             Debug.Log(AssetDatabase.GetAssetPath(asset.dest));

// #if ZH
//             Debug.Log(AssetDatabase.GetAssetPath(asset.pcZh));
            
//             FileUtil.ReplaceFile(AssetDatabase.GetAssetPath(asset.pcZh), AssetDatabase.GetAssetPath(asset.dest));
// #else
//             Debug.Log(AssetDatabase.GetAssetPath(asset.pc));
            
//             FileUtil.ReplaceFile(AssetDatabase.GetAssetPath(asset.pc), AssetDatabase.GetAssetPath(asset.dest));
// #endif

//             AssetDatabase.ImportAsset(AssetDatabase.GetAssetPath(asset.dest), ImportAssetOptions.ForceUpdate);
//         }

//         AssetDatabase.SaveAssets();
    }
    
	public void OnPostprocessBuild(BuildReport report) { }
#endif
}
