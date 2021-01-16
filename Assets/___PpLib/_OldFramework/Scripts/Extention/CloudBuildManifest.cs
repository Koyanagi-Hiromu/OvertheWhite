using UnityEngine;
using System;

namespace SR
{
    [Serializable]
    public class CloudBuildManifest
    {
        public static CloudBuildManifest Load()
        {
            var json = Resources.Load<TextAsset>("UnityCloudBuildManifest.json");
            if (json != null)
            {
                return JsonUtility.FromJson<CloudBuildManifest>(json.text);
            }

            json = Resources.Load<TextAsset>("UnityCloudBuildManifest");
            if (json != null)
            {
                return JsonUtility.FromJson<CloudBuildManifest>(json.text);
            }

            return null;
        }

        void ShowAppVersion()
        {
            var manifest = CloudBuildManifest.Load();
            if (manifest != null)
            {
                SLog.System.Warning("=================================");
                SLog.System.Warning("ScmCommitId: " + manifest.ScmCommitId); //最初の6桁これも隣に
                SLog.System.Warning("ScmBranch: " + manifest.ScmBranch);
                SLog.System.Warning("BuildNumber: " + manifest.BuildNumber); // 1.0.これ
                SLog.System.Warning("BuildStartTime: " + manifest.BuildStartTime);
                SLog.System.Warning("ProjectId: " + manifest.ProjectId);
                SLog.System.Warning("BundleId: " + manifest.BundleId);
                SLog.System.Warning("UnityVersion: " + manifest.UnityVersion);
                SLog.System.Warning("XCodeVersion: " + manifest.XCodeVersion);
                SLog.System.Warning("CloudBuildTargetName: " + manifest.CloudBuildTargetName);
                SLog.System.Warning("=================================");
            }
            else
            {
                // これも気をつけてね
                SLog.System.Warning("=================================");
                SLog.System.Warning("Manifest not found.");
                SLog.System.Warning("=================================");
            }
        }

        [SerializeField]
        string scmCommitId;
        public string ScmCommitId { get { return scmCommitId; } }

        [SerializeField]
        string scmBranch;
        public string ScmBranch { get { return scmBranch; } }

        [SerializeField]
        string buildNumber;
        public string BuildNumber { get { return buildNumber; } }

        [SerializeField]
        string buildStartTime;
        public string BuildStartTime { get { return buildStartTime; } }

        [SerializeField]
        string projectId;
        public string ProjectId { get { return projectId; } }

        [SerializeField]
        string bundleId;
        public string BundleId { get { return bundleId; } }

        [SerializeField]
        string unityVersion;
        public string UnityVersion { get { return unityVersion; } }

        [SerializeField]
        string xcodeVersion;
        public string XCodeVersion { get { return xcodeVersion; } }

        [SerializeField]
        string cloudBuildTargetName;
        public string CloudBuildTargetName { get { return cloudBuildTargetName; } }
    }
}
