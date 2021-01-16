using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using Sirenix.OdinInspector;
using System.Linq;
using DG.Tweening;
using System;

namespace SR
{
    public class AudioManager : DontDestroyManager<AudioManager>
    {
        private const float COOL_SEC = 0.05f;
        private const int MAX_COUNT = 8;

        private const float DEFAULT_MAX_VOLUME = 1.0f;
        public const float DEFAULT_FADING_TIME = 0.5f;

        public AudioMixer audioMixer;

        [SerializeField]
        private Dictionary<string, AudioSource> audioSourceMap = null;

        [NonSerialized]
        public AudioSource playingBgmAudioSource = null;

        [ShowInInspector] private Dictionary<AudioSource, BattleSEHandler> battleSEDic = null;
        [ShowInInspector] private Dictionary<AudioSource, float> coolingDic = null;

        class BattleSEHandler
        {
            /// <summary>
            /// -2 : 無効
            /// -1 : 有効
            ///  n : array[n]が空いてる
            /// </summary>
            public int number = -1;
            public float[] cooling = new float[MAX_COUNT];
            public void FPlay(AudioSource audioSource)
            {
                audioSource.pitch = UnityEngine.Random.Range(0.96f, 1.03f);
                audioSource.PlayOneShot(audioSource.clip, 1.0f);
                if (number == -1)
                {
                    number = 0;
                }
                cooling[number] = audioSource.clip.length;

                number = -2;
                for (var i = 0; i < MAX_COUNT; i++)
                {
                    if (cooling[i] <= 0)
                    {
                        number = i;
                        break;
                    }
                }
            }
        }

        protected override void UnityAwake()
        {
            //20171101 もう挙動もよくわからないし面倒だからEditorでは毎回再生成する
            //if (audioSourceMap != null) return;
            this.GenerateAudioSourceMap();
        }

        private void Start()
        {
            // var data = SaveManager.LoadCommon<AudioSettingData>("AudioSettingData");

            // if (data != null)
            // {
            //     this.SetBgmVolumeLog(data.volumeBgm / 100f);
            //     this.SetSeVolumeLog(data.volumeSe / 100f);
            // }
        }

        private void SetAuidoMixerVolume(string key, float value)
        {
            this.audioMixer.SetFloat(key, Mathf.Lerp(-80, 0, value));
        }

        public void SetMasterVolume(float value)
        {
            this.SetAuidoMixerVolume("Master", value);
        }

        public void SetBgmVolume(float value)
        {
            this.SetAuidoMixerVolume("BGM", value);
        }

        public void SetSeVolume(float value)
        {
            this.SetAuidoMixerVolume("SE", value);
        }

        private void SetAuidoMixerVolumeLog(string key, float value)
        {
            var log = 40f * Mathf.Log10(value);

            this.audioMixer.SetFloat(key, Mathf.Clamp(log, -80, 0));
        }

        public void SetMasterVolumeLog(float value)
        {
            this.SetAuidoMixerVolumeLog("Master", value);
        }

        public void SetBgmVolumeLog(float value)
        {
            this.SetAuidoMixerVolumeLog("BGM", value);
        }

        public void SetSeVolumeLog(float value)
        {
            this.SetAuidoMixerVolumeLog("SE", value);
        }

        public void SwitchSnapshot(string FromSnapshot, string ToSnapshot)
        {
            var baseSnapshot = this.audioMixer.FindSnapshot(FromSnapshot);
            var snapshot = this.audioMixer.FindSnapshot(ToSnapshot);

            this.audioMixer.TransitionToSnapshots(
                new AudioMixerSnapshot[] { baseSnapshot, snapshot },
                new float[] { 0, 1 },
                3
            );
        }

        public void GenerateAudioSourceMap()
        {
            this.audioSourceMap = new Dictionary<string, AudioSource>();

            var components = GetComponentsInChildren<AudioSource>();

            components.ForEach(c =>
            {
                audioSourceMap[c.clip.name] = c;
            });

            battleSEDic = new Dictionary<AudioSource, BattleSEHandler>();
            coolingDic = new Dictionary<AudioSource, float>();
        }

        public static bool bgmStop;

        // FIXME: isFadeInを導入したら壊れたので諦めます 2018/11/12
        ///
        /// if (playingBgmAudioSource == null)
        // {
        //     // 最初はFadeInしない
        //     playingBgmAudioSource = willFadeInAudioSource;
        //     if (playingBgmAudioSource == null)
        //     {
        //         return false;
        //     }
        //     else
        //     {
        //         if (isFadeIn)
        //         {
        //             playingBgmAudioSource.volume = 0;
        //             playingBgmAudioSource.DOKill();
        //             playingBgmAudioSource.DOFade(DEFAULT_MAX_VOLUME, fadeTime);
        //             playingBgmAudioSource.Play();
        //             SLog.Say.Info("fadeIn");
        //         }
        //         else
        //         {
        //             playingBgmAudioSource.volume = 1;
        //             playingBgmAudioSource.Play();
        //             SLog.Say.Info("NoFadeIn");
        //         }

        //     }
        // }
        // else
        // {
        //     // 同じなら面倒なので何もしない
        //     if (playingBgmAudioSource == willFadeInAudioSource)
        //     {
        //         return false;
        //     }

        //     // 2回目以降はFadeIn, FadeOutする
        //     var willFadeOutAudioSource = playingBgmAudioSource;
        //     playingBgmAudioSource = willFadeInAudioSource;

        //     if (isFadeIn)
        //     {
        //         willFadeOutAudioSource.DOKill();
        //         willFadeOutAudioSource.DOFade(0, fadeTime);

        //         playingBgmAudioSource.volume = 0;
        //         playingBgmAudioSource.DOKill();
        //         playingBgmAudioSource.DOFade(DEFAULT_MAX_VOLUME, fadeTime);
        //         playingBgmAudioSource.Play();
        //         SLog.Say.Info("cross");
        //     }
        //     else
        //     {
        //         willFadeOutAudioSource.DOKill();
        //         playingBgmAudioSource.DOKill();
        //         DOTween.Sequence()
        //         .Append(willFadeOutAudioSource.DOFade(0, fadeTime))
        //         .AppendCallback(() => playingBgmAudioSource.volume = 1)
        //         .AppendCallback(playingBgmAudioSource.Play)
        //         .Play();
        //         SLog.Say.Info("noCross");
        //     }
        // }
        // return true;
        /// 
        public bool TryPlayBgmWithFade(string name, bool isFadeIn, float fadeTime = DEFAULT_FADING_TIME)
        {
            if (bgmStop)
            {
                SLog.Say.Info("bgmStop");
                return false;
            }

            var willFadeInAudioSource = GetAudioSource(name);
            if (willFadeInAudioSource == null)
            {
                SLog.Audio.Info("AudioSourceが見つからなかったため、再生されません");
                return false;
            }
            if (playingBgmAudioSource == null)
            {
                // 最初はFadeInしない
                playingBgmAudioSource = willFadeInAudioSource;
                if (playingBgmAudioSource == null)
                {
                }
                else
                {
                    playingBgmAudioSource.volume = 1;
                    playingBgmAudioSource.Play();
                }
            }
            else
            {
                // 同じなら面倒なので何もしない
                if (playingBgmAudioSource == willFadeInAudioSource)
                {
                    return false;
                }

                // 2回目以降はFadeIn, FadeOutする
                var willFadeOutAudioSource = playingBgmAudioSource;
                playingBgmAudioSource = willFadeInAudioSource;

                willFadeOutAudioSource.DOKill();
                willFadeOutAudioSource.DOFade(0, fadeTime);

                playingBgmAudioSource.volume = 0;
                playingBgmAudioSource.DOKill();
                playingBgmAudioSource.DOFade(DEFAULT_MAX_VOLUME, fadeTime);
                playingBgmAudioSource.Play();
            }
            return true;
        }


        public void StopBgm(float fadeTime = DEFAULT_FADING_TIME)
        {
            playingBgmAudioSource.DOKill();
            playingBgmAudioSource.DOFade(0, fadeTime);
            playingBgmAudioSource = null;
        }

        public void StopBgm(Action<AudioSource> action, float fadeTime = DEFAULT_FADING_TIME)
        {
            playingBgmAudioSource.DOKill();
            float time;
            var playing = playingBgmAudioSource;
            playingBgmAudioSource = null;
            DOTween.Sequence()
            .Append(playing.DOFade(0, fadeTime))
            .AppendCallback(() => playing.Stop())
            .AppendCallback(() => time = playing.time)
            .AppendCallback(() =>
            {
                if (action != null)
                {
                    action(playing);
                }
            })
            .Play();
        }

        /// <summary>
        /// 音を鳴らすメソッドです。nameは必ず存在するAudioSourceを指定してください。音を鳴らさない場合はこのメソッドを呼ばないでください。
        /// </summary>
        /// <param name="name"></param>
        /// 
        public virtual void Play(string name)
        {
            var audioSource = GetAudioSource(name);
            Play(audioSource, checkIsPlaying: false);
        }

        public virtual void Play(string name, float pitch)
        {
            var audioSource = GetAudioSource(name);
            audioSource.pitch = pitch;
            Play(audioSource, checkIsPlaying: false);
        }

        public virtual void PlayIfNotPlaying(string name)
        {
            var audioSource = GetAudioSource(name);
            Play(audioSource, checkIsPlaying: true);
        }

        private void Update()
        {
            UpdateCoolingDic();
            UpdateBattleSEDic();
        }

        private void UpdateCoolingDic()
        {
            if (coolingDic == null) return;

            var array = coolingDic.Keys.ToArray();
            foreach (var key in array)
            {
                var current = coolingDic[key];
                if (current > 0)
                    coolingDic[key] = current - Time.deltaTime;
                else
                    coolingDic.Remove(key);
            }
        }

        private void UpdateBattleSEDic()
        {
            if (battleSEDic == null) return;

            foreach (var pair in battleSEDic)
            {
                var audio = pair.Key;
                var data = pair.Value;
                if (audio.isPlaying)
                {
                    data.number = -2;
                    for (var i = MAX_COUNT - 1; i >= 0; i--)
                    {
                        var c = data.cooling[i];
                        if (c <= 0)
                        {
                            data.number = i;
                        }
                        else
                        {
                            data.cooling[i] -= Time.deltaTime;
                        }
                    }
                }
                else if (data.number != -1)
                {
                    data.number = -1;
                    for (var i = 0; i < MAX_COUNT; i++)
                    {
                        data.cooling[i] = 0;
                    }
                }

                // Watch(audio, data);
            }
        }

        private static void Watch(AudioSource audio, BattleSEHandler data)
        {
            string text;
            if (data.number == -1)
                text = "O";
            else if (data.number == -2)
                text = "!";
            else
                text = data.number.ToString();

            SLog.Check.Watch(audio.clip.name, $"{text} {audio.clip.length:F4} {data.cooling[0]:F4} {data.cooling[1]:F4} {data.cooling[2]:F4} {audio.name}");
        }

        private void Play(AudioSource audioSource, bool checkIsPlaying = false)
        {
            if (IsInvalidAudioSource(audioSource))
                return;
            else if (checkIsPlaying && audioSource.isPlaying)
                return;
            else if (IsCooling(audioSource))
                return;

            if (IsBattleSE(audioSource))
            {
                BattleSEHandler handler;
                if (!battleSEDic.TryGetValue(audioSource, out handler))
                {
                    handler = new BattleSEHandler();
                    battleSEDic[audioSource] = handler;
                }

                if (handler.number != -2)
                {
                    coolingDic[audioSource] = COOL_SEC;
                    handler.FPlay(audioSource);
                }
            }
            else
            {
                coolingDic[audioSource] = COOL_SEC;
                audioSource.PlayOneShot(audioSource.clip, 1.0f);
            }
        }

        private bool IsCooling(AudioSource audioSource)
         => coolingDic.ContainsKey(audioSource) && coolingDic[audioSource] > 0;

        private bool IsBattleSE(AudioSource audioSource) => audioSource.name.StartsWith("Battle");

        private static bool IsInvalidAudioSource(AudioSource audioSource)
        {
            if (!audioSource || audioSource.clip == null || audioSource.clip.name.IsNullOrEmpty())
            {
                SLog.Audio.Info("audio sourceが空です。");
                return true;
            }
            return false;
        }

        public virtual void Stop(string name)
        {
            var audioSource = GetAudioSource(name);

            if (audioSource)
            {
                audioSource.Stop();
            }
        }

        public bool isAudioExist(string name)
        {
            if (name == null)
            {
                SLog.Audio.Info("Name is null");
                return false;
            }
            return audioSourceMap.ContainsKey(name);
        }

        private AudioSource GetAudioSource(string name)
        {
            if (isAudioExist(name))
            {
                return audioSourceMap[name];
            }
            else
            {
                SLog.Audio.Info("keyが見つかりません。key: " + name);

                return null;
            }
        }

        [Button("再生テスト")]
        public void OnPlayTest()
        {
            OnGenerateOnEditor();

            Play(audioSourceMap.Keys.First());
        }

        [Button("再生テスト2")]
        public void OnPlayTest2()
        {
            OnGenerateOnEditor();

            TryPlayBgmWithFade("OnPaintedBonus1", false);
        }

        [Button("再生テスト3")]
        public void OnPlayTest3()
        {
            OnGenerateOnEditor();

            TryPlayBgmWithFade("se_EnemyFireDamaged_00", false);
        }

        [Button("再生テスト4")]
        public void OnPlayTest4()
        {
            OnGenerateOnEditor();

            var sourceName = "BgmBoss";
            var willFadeInAudioSource = GetAudioSource(sourceName);
            willFadeInAudioSource.volume = 0.1f;
            willFadeInAudioSource.Play();
            willFadeInAudioSource.DOKill();
            willFadeInAudioSource.DOFade(DEFAULT_MAX_VOLUME, DEFAULT_FADING_TIME);
        }

        [Button("変更を反映")]
        public void OnRefresh()
        {
            audioSourceMap = null;

            OnGenerateOnEditor();
        }

        [Button("PlayOnAwakeがONになってないかチェック")]
        public void OnCheckAllPlayOnAwake()
        {
            OnGenerateOnEditor();

            audioSourceMap.ForEach((pair) =>
            {
                var audioSource = pair.Value;

                if (audioSource.playOnAwake)
                {
                    if (audioSource.clip)
                    {
                        SLog.Audio.Error("PlayOnAwakeがOnになっています。 AudioSouce: " + audioSource.clip.name);
                    }
                }
            });

            SLog.Audio.Info("PlayOnAwakeのチェックが完了しました。");
        }

        [Button("AudioMixerが設定されてるかチェック")]
        public void OnCheckAllOutputAudioMixerGroup()
        {
            OnGenerateOnEditor();

            audioSourceMap.ForEach((pair) =>
            {
                var audioSource = pair.Value;

                if (!audioSource.outputAudioMixerGroup)
                {
                    if (audioSource.clip)
                    {
                        SLog.Audio.Error("outputAudioMixerGroupが空です AudioSouce: " + audioSource.clip.name);
                    }
                }
            });

            SLog.Audio.Info("OutputAudioMixerGroupのチェックが完了しました。");
        }

        [Button("音量が0.5かチェック")]
        public void OnCheckAllVolume()
        {
            OnGenerateOnEditor();

            audioSourceMap.ForEach((pair) =>
            {
                var audioSource = pair.Value;

                if (audioSource.volume != 0.5f)
                {
                    if (audioSource.clip)
                    {
                        SLog.Audio.Error("音量が0.5fじゃないです AudioSouce: " + audioSource.clip.name);
                    }
                }
            });

            SLog.Audio.Info("Volumeのチェックが完了しました。");
        }

        public void PlayOnEditor(string name)
        {
            OnGenerateOnEditor();

            Play(name);
        }

        public void StopOnEditor(string name)
        {
            //OnGenerateOnEditor();
            Stop(name);
        }

        public void OnGenerateOnEditor()
        {
            GenerateAudioSourceMap();
        }

        public bool IsPlaying(AudioSource audioSource)
        {
            return !audioSource.isPlaying && audioSource.time == 0.0f;
        }

        public AudioSource[] GetAllAudioSource()
        {
            GenerateAudioSourceMap();

            return audioSourceMap.Values.ToArray();
        }
    }
}