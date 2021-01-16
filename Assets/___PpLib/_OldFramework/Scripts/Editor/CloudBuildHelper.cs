#if UNITY_CLOUD_BUILD
using UnityEngine;
using UnityEngine.CloudBuild;
using UnityEditor;
using SR;
using SREditor;

public class CloudBuildHelper : MonoBehaviour
{
#if UNITY_STANDALONE
    public const int MainVersion = 1;
    public const int SubVersion = 1;

    public static void PreExport(BuildManifestObject manifest)
    {
        Debug.Log("CloudBuildHelper:PreExport");

        PlayerSettings.bundleVersion = $"{MainVersion}.{SubVersion}.{manifest.GetValue("buildNumber")}";
    }
#elif UNITY_IOS
    public static void PreExport(BuildManifestObject manifest)
    {
        Debug.Log("CloudBuildHelper:PreExport");

        var buildNumber = manifest.GetValue("buildNumber");

        Debug.Log($"buildNumber: {buildNumber}");

        if (buildNumber != null)
        {
            PlayerSettings.iOS.buildNumber = buildNumber;
        }
    }
#else
    public static void PreExport(BuildManifestObject manifest)
    {
        Debug.Log("CloudBuildHelper:PreExport");
    }
#endif
}

#endif
