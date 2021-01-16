using Sirenix.Utilities.Editor;
using UnityEditor;
using UnityEngine;

namespace SR.Nite
{
    [CustomEditor(typeof(AudioSource), true)]
    public sealed class AudioSourceEditor : Editor
    {
        AudioSource audioSource;

        private bool foldOutDefaultInspector;
        private bool isSplove;

        void OnEnable()
        {
            isSplove = this.target.name.StartsWith("slv-");
            if (isSplove)
            {
                audioSource = (AudioSource)serializedObject.targetObject;
            }
        }

        public override void OnInspectorGUI()
        {
            if (isSplove)
            {
                // var desc = "";
                GUILayout.BeginHorizontal();
                {
                    //EditorGUILayout.LabelField( "Begin" );
                    if (GUILayout.Button("▶", GUILayout.Width(30)))
                    {
                        audioSource.PlayOneShot(audioSource.clip);
                    }
                    if (GUILayout.Button("↷", GUILayout.Width(30)))
                    {
                        audioSource.Play();
                    }
                    if (GUILayout.Button("■", GUILayout.Width(30)))
                    {
                        audioSource.Stop();
                    }
                    if (audioSource.clip != null)
                    {
                        if (GUILayout.Button(audioSource.clip.name, GUILayout.Width(200)))
                        {
                            Clipboard.Copy(audioSource.clip.name);
                            EditorUtility.DisplayDialog("クリップボードにコピーしたよ!", audioSource.clip.name, "OK");
                        }
                    }
                    else
                    {
                    }
                }
                if (audioSource != null && audioSource.outputAudioMixerGroup)
                {
                    GUILayout.Label(audioSource.outputAudioMixerGroup.name);
                }
                GUILayout.EndHorizontal();

                GUILayout.BeginHorizontal();
                {
                    //EditorGUILayout.LabelField( "Begin" );
                    if (GUILayout.Button("－", GUILayout.Width(30)))
                    {
                        audioSource.volume -= 0.1f;
                        EditorUtility.SetDirty(this);
                    }
                    if (GUILayout.Button("♪ " + (audioSource.volume).ToF1(), GUILayout.Width(50)))
                    {
                        audioSource.volume = 0.5f;
                        EditorUtility.SetDirty(this);
                    }
                    if (GUILayout.Button("+", GUILayout.Width(30)))
                    {
                        audioSource.volume += 0.1f;
                        EditorUtility.SetDirty(this);
                    }
                }
                GUILayout.EndHorizontal();

                // if (clip != null) {
                //     EditorGUILayout.PropertyField(clip);
                // }
                foldOutDefaultInspector = EditorGUILayout.Foldout(foldOutDefaultInspector, "AudioSource");
                if (foldOutDefaultInspector)
                {
                    DrawDefaultInspector();
                }
                //EditorGUILayout.LabelField( "End" );
            }
            else
            {
                DrawDefaultInspector();
            }
        }
    }
}