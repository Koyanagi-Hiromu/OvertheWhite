using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.Networking;
using UnityEditor.Callbacks;
using UnityEditor;
using MiniJSON;

#if UNITY_IOS
using UnityEditor.iOS.Xcode;
#endif

public class BuildVersionPostProcess
{
    private static string companyName = "pocketpair";
    private static string projectName = "overstory";
    private static string androidBuildTargetName = "release-android";
    private static string iOSBuildTargetName = "release-ios";
    private static string AuthorizationToken = "Basic 5397c9f9f9a34f242d100d2b19732dbd";

    [PostProcessBuild(101)]
    public static void OnPostprocessBuild(BuildTarget buildTarget, string buildPath)
    {
        Debug.Log("BuildVersionPostProcess:OnPostprocessBuild");
        Debug.Log($"Application.buildGUID: {Application.buildGUID}");

        if (buildTarget != BuildTarget.Android && buildTarget != BuildTarget.iOS)
        {
            Debug.LogWarning($"Can't change buildVersion, only iOS and Android are supported. Not {buildTarget.ToString()}");

            return;
        }

        var buildNumber = FindLatestBuildNumber(buildTarget == BuildTarget.Android ? androidBuildTargetName : iOSBuildTargetName);

        AssignBuildNumber(buildNumber, buildPath);
    }

    private static long FindLatestBuildNumber(string buildTarget)
    {
        var url = string.Format("https://build-api.cloud.unity3d.com/api/v1/orgs/{0}/projects/{1}/buildtargets/{2}/builds", companyName, projectName, buildTarget);

        var www = UnityWebRequest.Get(url);

        www.SetRequestHeader("Content-Type", "application/json");
        www.SetRequestHeader("Authorization", AuthorizationToken);

        var op = www.Send();

        while (!op.isDone) ;

        if (www.isNetworkError)
        {
            Debug.Log(www.error);

            return 1;
        }

        var list = Json.Deserialize(www.downloadHandler.text) as List<object>;

        foreach (var item in list)
        {
            var dictionary = item as Dictionary<string, object>;

            var build = (long)dictionary.GetOrDefault("build", 0);
            var buildGUID = (string)dictionary.GetOrDefault("buildGUID", "");
            var buildStatus = (string)dictionary.GetOrDefault("buildStatus", "");

            Debug.Log("dictionary[\"build\"]: " + build);
            Debug.Log("dictionary[\"buildGUID\"]: " + buildGUID);
            Debug.Log("dictionary[\"buildStatus\"]: " + buildStatus);

            if (buildStatus == "started")
            {
                Debug.Log("return: " + build);

                return build;
            }
        }

        return 0;
    }

    private static void AssignBuildNumber(long correctBuildNumber, string buildPath)
    {
        Debug.Log("Assigning build number: " + correctBuildNumber);

#if UNITY_IOS
        ChangeXCodeBuildNumber(correctBuildNumber, buildPath);
#elif UNITY_ANDROID
        ChangeAndroidBuildNumber(correctBuildNumber, buildPath);
#endif
    }

#if UNITY_IOS
    private static void ChangeXCodeBuildNumber(long correctBuildNumber, string buildPath)
    {
        var projPath = Path.Combine(buildPath, "Info.plist");

        var plist = new PlistDocument();

        plist.ReadFromString(File.ReadAllText(projPath));

        var rootDict = plist.root;

        // CloudBuildHelper:PreExportで同等の処理を行うように変更したため削除
        // rootDict.SetString("CFBundleVersion", correctBuildNumber.ToString());

        rootDict.SetString("ITSAppUsesNonExemptEncryption", "false");

        // `ITMS-90683: Missing Purpose String in Info.plist`対応
        rootDict.SetString("NSPhotoLibraryUsageDescription ", "User can record video");

        // `ITMS-90683: Missing Purpose String in Info.plist`対応
        // -> 位置情報を利用しないように修正したため削除
        // rootDict.SetString("NSLocationWhenInUseUsageDescription", "User can record video");

        File.WriteAllText(projPath, plist.WriteToString());
    }
#endif

    private static void ChangeAndroidBuildNumber(long correctBuildNumber, string buildPath)
    {

    }
}
