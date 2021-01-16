using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

namespace SR
{
    public class TimeScaleManager : SingletonMonoBehaviour<TimeScaleManager>
    {
        private Coroutine currentCoroutine;
        public float currentScale { get; private set; }
        private Tween curTween;
        protected override void UnityAwake()
        {
            currentScale = Time.timeScale;
        }

        public void Reset()
        {
            if (currentCoroutine != null)
            {
                StopCoroutine(currentCoroutine);
                currentCoroutine = null;
            }
            if (curTween != null)
            {
                curTween.Kill(true);
                curTween = null;
            }
            // Time.timeScale = 1.0f;
            Time.timeScale = GetBattleSpeed();
            currentScale = Time.timeScale;
            curTween = null;
        }

        /// <summary>
        /// 他の入力を受け付けない
        /// </summary>
        public void Fade(float endValue, float duration, bool unlock)
        {
            if (currentCoroutine != null)
            {
                StopCoroutine(currentCoroutine);
                currentCoroutine = null;
            }
            if (curTween != null)
            {
                curTween.Kill(true);
                curTween = null;
            }

            curTween = DOTween.To
            (
                getter: () => currentScale,
                setter: scale => Time.timeScale = scale,
                endValue: endValue,
                duration: duration
            )
            .OnComplete(() =>
            {
                currentScale = endValue;
                if (unlock)
                {
                    curTween = null;
                }
            })
            .SetUpdate(true);
        }

        /// <summary>
        /// ☆現在のタイムスケールより大きな値が渡された場合、無視します。
        /// ☆そうでない場合、直前のタイムスケールは破棄され、新しいものに変更されます
        /// </summary>
        /// <param name="scale">０以下でエラー</param>
        /// <param name="durationRealSec"></param>
        public void Set(float scale, float durationRealSec)
        {
            if (curTween != null)
            {
                // ロック中につき無効
            }
            else if (scale <= 0)
            {
                Assert.UnReachable("timeScaleが負になっています");
            }
            else
            {
                StartCoroutine(scale, durationRealSec);
            }
        }

        private void StartCoroutine(float scale, float durationRealSec)
        {
            if (currentCoroutine != null)
            {
                if (scale > currentScale)
                {
                    return;
                }
                StopCoroutine(currentCoroutine);
            }
            currentCoroutine = StartCoroutine(ChangeTimeScale(scale, durationRealSec));
        }

        private IEnumerator ChangeTimeScale(float scale, float durationRealSec)
        {
            Assert.IsTrue(currentScale == Time.timeScale);
            currentScale = scale;
            Time.timeScale = currentScale;

            yield return new WaitForSecondsRealtime(durationRealSec);

            Assert.IsTrue(currentScale == Time.timeScale);
            // currentScale /= scale;
            // Time.timeScale = currentScale;
            currentScale = GetBattleSpeed();
            Time.timeScale = currentScale;
            currentCoroutine = null;
        }

        /// <summary>
        /// 時間のんびり中にホーム画面に戻るときのケア
        /// </summary>
        protected override void OnDestroy()
        {
            if (curTween != null)
            {
                curTween.Kill(false);
            }
            if (currentCoroutine != null)
            {
                StopCoroutine(currentCoroutine);
            }
            currentScale = 1.0f;
            Time.timeScale = currentScale;
            base.OnDestroy();
        }

        private float GetBattleSpeed() => 1.0f;
        public void RefreshBattleSpeed()
        {
            if (currentCoroutine == null && currentScale == Time.timeScale)
            {
                currentScale = GetBattleSpeed();
                Time.timeScale = currentScale;
            }
        }
    }
}
