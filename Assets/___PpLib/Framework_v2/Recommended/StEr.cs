using System;
using System.Collections;
using UnityEngine;

namespace PPD
{
    //TODO: Chronos 対応で DelayCall に Root に依存する Timeline を設定できていない
    public static class StEr
    {
        public static readonly WaitForEndOfFrame WaitForEndOfFrame = new WaitForEndOfFrame();
        public static Coroutine StartCoroutine(IEnumerator er) => Data.CoroutineHandler.StartCoroutine(er);

        public static Coroutine DelayCall(float delaySec, Action action) => StartCoroutine(ErDelayCall(delaySec, action));
        private static IEnumerator ErDelayCall(float delaySec, Action action)
        {
            yield return new WaitForSeconds(delaySec);
            action();
        }

        public static Coroutine DelayCallRealtime(float delaySec, Action action) => StartCoroutine(ErDelayCallRealtime(delaySec, action));
        private static IEnumerator ErDelayCallRealtime(float delaySec, Action action)
        {
            yield return new WaitForSecondsRealtime(delaySec);
            action();
        }

        public static Coroutine DelayCall_1F(Action action) => StartCoroutine(ErDelayCall_1F(action));
        private static IEnumerator ErDelayCall_1F(Action action)
        {
            yield return null;
            action();
        }

        public static Coroutine DelayCall_2F(Action action) => StartCoroutine(ErDelayCall_2F(action));
        private static IEnumerator ErDelayCall_2F(Action action)
        {
            yield return null;
            yield return null;
            action();
        }
    }
}
