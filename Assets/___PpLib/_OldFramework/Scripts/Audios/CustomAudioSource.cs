using UnityEngine;
using System;


#if UNITY_EDITOR

using Sirenix.OdinInspector.Editor;
using Sirenix.Utilities.Editor;
using UnityEditor;
using Sirenix.Utilities;

#endif

namespace SR.Nite
{
    [Serializable]
    public struct CustomAudioSource
    {
        public string keyName;

        public void Play(bool option_PlayOnlyIfStopped = false)
        {
            if (keyName.IsNullOrEmpty()) return;
            if (!AudioManager.Ins) return;

            if (Application.isPlaying)
            {
                if (option_PlayOnlyIfStopped)
                {
                    AudioManager.Ins.PlayIfNotPlaying(keyName);
                }
                else
                {
                    AudioManager.Ins.Play(keyName);
                }
            }
            else
            {
                var goAm = GameObject.Find("[AudioManager]");
                AudioManager am = goAm ? goAm.GetComponent<AudioManager>() : null;
                if (am)
                {
                    am.OnGenerateOnEditor();
                    am.PlayOnEditor(keyName);
                }
            }
        }

        public void PlayBgm(float fadeTime = AudioManager.DEFAULT_FADING_TIME)
        {
            if (Application.isPlaying)
            {
                AudioManager.Ins.TryPlayBgmWithFade(keyName, false, fadeTime);
            }
            else
            {
                SLog.Editor.Info("Playモードではないので、音楽は再生されません");
            }
        }

        public void StopBgm(float fadeTime = AudioManager.DEFAULT_FADING_TIME)
        {
            if (Application.isPlaying)
            {
                if (AudioManager.Ins)
                {
                    AudioManager.Ins.StopBgm(fadeTime);
                }
            }
            else
            {
                SLog.Editor.Info("Playモードではないので、音楽は停止出来ません");
            }
        }
    }
}

#if UNITY_EDITOR
namespace SR.Nite
{
    [OdinDrawer]
    public class CustomAudioSourceDrawer : OdinValueDrawer<CustomAudioSource>
    {
        protected override void DrawPropertyLayout(GUIContent label)
        {
            var entry = ValueEntry;
            CustomAudioSource cas = entry.SmartValue;
            var goAm = GameObject.FindGameObjectWithTag("AudioManager");
            AudioManager am = goAm ? goAm.GetComponent<AudioManager>() : null;
            if (am)
            {
                am.OnGenerateOnEditor();
                if (cas.keyName != null)
                {
                    if (!am.isAudioExist(cas.keyName))
                    {
                        SirenixEditorGUI.ErrorMessageBox("指定されたkeyの音は見つかりません");
                    }
                }
            }
            else
            {
                // SirenixEditorGUI.WarningMessageBox("AudioManagerが見つかりません");
            }

            Rect rect = EditorGUILayout.GetControlRect();
            if (cas.keyName == null || cas.keyName == "")
            {
                var _rect = rect.AlignLeft(rect.width - 60);
                var RectObjectSelector = new Rect(_rect.x + 130, _rect.y, _rect.width - 130, _rect.height);
                var RectLabel = new Rect(_rect.x, _rect.y, 130, _rect.height);
                GUI.Label(RectLabel, label.text);
                var selectedClip = (AudioClip)SirenixEditorFields.PolymorphicObjectField(RectObjectSelector, null, null, typeof(AudioClip), false);
                if (selectedClip != null)
                {
                    cas.keyName = selectedClip.name;
                }
            }
            else
            {
                cas.keyName = SirenixEditorFields.TextField(rect.AlignLeft(rect.width - 60), label, cas.keyName);
            }
            if (GUI.Button(rect.AlignRight(30).AddX(-30), "■"))
            {
                //entry.SmartValue.Play();
                if (am)
                {
                    am.StopOnEditor(entry.SmartValue.keyName);
                }
                else
                {
                    SLog.Audio.Warning("AudioManagerが見つかりません。音は停止出来ません。");
                }
            }
            if (GUI.Button(rect.AlignRight(30), "▶"))
            {
                //entry.SmartValue.Play();
                if (am)
                {
                    am.PlayOnEditor(entry.SmartValue.keyName);
                }
                else
                {
                    UnityEngine.Object pPrefab = AssetDatabase.LoadAssetAtPath("Assets/_SR/Common/Audios/DD_AudioManager.prefab", typeof(GameObject));
                    GameObject tempAM = GameObject.Instantiate(pPrefab, Vector3.zero, Quaternion.identity) as GameObject;
                    tempAM.name = "AudioManager(Temp)";
                    Util.PopMessage(title: "AudioManager(Temp)を生成しました。", nameStr: "AudioManagerが見つかりません。AudioManager(Temp)をシーンに生成しました。");
                    SLog.Audio.Warning("AudioManagerが見つかりません。音は再生出来ません。");
                }
            }
            entry.SmartValue = cas;
        }
    }
}
#endif