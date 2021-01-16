using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace SR.Save.Manager
{
    public static class SaveManager
    {
        private static int currentSlot = 0;
        public static int CurrentSlot
        {
            get => currentSlot;
            set
            {
                if (value != 0)
                {
                    return;
                }

                currentSlot = value;
            }
        }

        public static string GetDirectory()
        {
#if UNITY_EDITOR
            return "Save";
#else
            return Application.persistentDataPath;
#endif
        }
        public static string GetTempDirectory()
        {
#if UNITY_EDITOR
            return "SRTemp";
#else
            return Application.temporaryCachePath;
#endif
        }

        public static string GetTempVideoDirectory()
        {
            string tempPath;
#if UNITY_EDITOR
            tempPath = "SRTemp";
#else
            tempPath = Application.temporaryCachePath;
#endif
            var path = _PrepareDirectory(tempPath, "Video");
            return path;
        }


        public static void ClearTempVideoDirectory()
        {
            try
            {
                var path = GetTempVideoDirectory();
                System.IO.DirectoryInfo di = new DirectoryInfo(path);
                foreach (FileInfo file in di.GetFiles())
                {
                    file.Delete();
                }
            }
            catch (System.Exception e)
            {
                // Assert.UnReachable(e.ToString());
            }
        }

        private static string GetDirectory(int slot)
        {
#if UNITY_EDITOR
            return $"Save/{slot}";
#else
            return $"{Application.persistentDataPath}/{slot}";
#endif
        }

        private static string GetScreenshotDirectory(int slot)
        {
#if UNITY_EDITOR
            return $"Save/Screenshot/{slot}";
#else
            return $"{Application.persistentDataPath}/Screenshot/{slot}";
#endif
        }

        private static string GetScreenshotDirectoryRelative(int slot)
        {
#if UNITY_EDITOR
            return $"Save/Screenshot/{slot}";
#else
            return $"Screenshot/{slot}";
#endif
        }

        private static string PreparePath(string name)
        {
            var path = GetDirectory();
            return _PreparePath(path, name);
        }

        private static string _PreparePath(string path, string name)
        {
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
            return $"{path}/{name}";
        }

        private static string _PrepareDirectory(string path, string name)
        {
            var resultPath = $"{path}/{name}";
            if (!Directory.Exists(resultPath))
            {
                Directory.CreateDirectory(resultPath);
            }
            return resultPath;
        }

        private static string PreparePath(int slot, string name)
        {
            var path = GetDirectory(slot);
            return _PreparePath(path, name);
        }

        private static string PrepareScreenshotPath(int slot, string name)
        {
            var path = GetScreenshotDirectory(slot);
            return _PreparePath(path, name);
        }

        private static void EncryptAndWriteJson<T>(T obj, string path, string name)
        {
            var json = JsonUtility.ToJson(obj);

            SLog.System.Info($"EncryptAndWriteJson<{typeof(T)}>[{name}]:{json}");

#if UNITY_EDITOR
            File.WriteAllText($"{path}.json", json);
#endif

            try
            {
                var jsonEncrypted = StringEncryptor.Encrypt(json);

                File.WriteAllText(path, jsonEncrypted);
            }
            catch (Exception e)
            {
                SLog.System.Info($"EncryptAndWriteJson:{e}");
                SLog.System.Info($"failEncryptAndWriteJson:{json}");

                // AnalyticsManager.SendEvent("failEncryptAndWriteJson", new Dictionary<string, object>()
                // {
                //     { "exception", e.ToString() },
                //     { "name", name },
                // });
            }
        }

        public static void SaveCommon<T>(T obj, string name)
        {
            var path = PreparePath(name);

            EncryptAndWriteJson(obj, path, name);
        }

        public static void SaveInSlot(SaveData obj)
        {
            SaveCurrentSlot(obj, obj.GetType().Name);
        }

        [Obsolete]
        public static void SaveInSlot<T>(T obj, string name)
        {
            SaveCurrentSlot<T>(obj, name);
        }

        private static void SaveCurrentSlot<T>(T obj, string name)
        {
            var path = PreparePath(CurrentSlot, name);

            EncryptAndWriteJson<T>(obj, path, $"{CurrentSlot}/{name}");
        }

        /// <summary>
        /// slot共通
        /// </summary>
        public static T LoadCommon<T>(string name)
        where T : new()
        {
            var json = GetDecryptedJsonCommon(name);

            return GetFromJson<T>(json, name);
        }

        public static T LoadInSlot<T>()
        where T : SaveData, new()
        {
            var name = typeof(T).Name;
            var json = GetDecryptedJson(name);

            return GetFromJson<T>(json, $"{CurrentSlot}/{name}");
        }

        private static T GetFromJson<T>(string json, string name)
        where T : new()
        {
            if (json == null)
            {
                return new T();
            }

            try
            {
                // SLog.System.Info($"GetFromJson<{typeof(T)}>[{name}]:{json}");

                return JsonUtility.FromJson<T>(json);
            }
            catch (Exception e)
            {
                SLog.System.Info($"GetFromJson:{e}");
                SLog.System.Info($"failGetFromJson:{json}");

                // AnalyticsManager.SendEvent("failGetFromJson", new Dictionary<string, object>()
                // {
                //     { "exception", e.ToString() },
                //     { "name", name },
                // });

                return new T();
            }
        }

        private static string ReadAndDecryptJson(string path, string name)
        {
            if (!File.Exists(path))
            {
                return null;
            }

            var test = File.ReadAllText(path);

            try
            {
                return StringEncryptor.Decrypt(test);
            }
            catch (Exception e)
            {
                SLog.System.Info($"ReadAndDecryptJson:{e}");
                SLog.System.Info($"failDecryptJson:{test}");

                // AnalyticsManager.SendEvent("failDecryptJson", new Dictionary<string, object>()
                // {
                //     { "exception", e.ToString() },
                //     { "name", name },
                // });

                return null;
            }
        }

        private static string GetDecryptedJsonCommon(string name)
        {
            var path = PreparePath(name);

            return ReadAndDecryptJson(path, name);
        }

        private static string GetDecryptedJson(string name)
        {
            var path = PreparePath(CurrentSlot, name);

            return ReadAndDecryptJson(path, $"{CurrentSlot}/{name}");
        }

        private static string ReadJson(string path)
        {
            if (!File.Exists(path))
            {
                return null;
            }

            return File.ReadAllText(path);
        }

        private static string GetJsonCommon(string name)
        {
            var path = $"{PreparePath(name)}.json";

            return ReadJson(path);
        }

        private static string GetJson(string name)
        {
            var path = $"{PreparePath(CurrentSlot, name)}.json";

            return ReadJson(path);
        }
    }

    public abstract class SaveData
    {
        public void Save()
        {
            SaveManager.SaveInSlot(this);
        }
    }

    public abstract class SaveData<TSelf> : SaveData
    where TSelf : SaveData<TSelf>, new()
    {
        public static TSelf Load()
        {
            return SaveManager.LoadInSlot<TSelf>();
        }
    }
}
