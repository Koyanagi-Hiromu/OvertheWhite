using System.IO;
using UnityEngine;

namespace SR
{
    public class SRGlobalSettings //: ASceneSharedChild<SRGlobalSettings>
    {
        public bool IsAdminOnly { get; set; }
        public bool IsAdminOrDevBuild => IsAdminOnly || Debug.isDebugBuild;
        
        private const string ADMIN_KEY = "F2Yv09TWI1VHps1Ucl477wA2J3AOyiL4PLlO5FLP7vAg";
        protected void UnityAwake()
        {
            ConfigureIsAdmin();
            QualitySettings.skinWeights = SkinWeights.FourBones;
        }

        private void ConfigureIsAdmin() {
            var path = Application.persistentDataPath + "/AdminKey.txt";
            if (File.Exists(path))
            {
                var t = File.ReadAllText(path);
                if (t == ADMIN_KEY)
                {
                    IsAdminOnly = true;
                }
            }
        }
    }
}
