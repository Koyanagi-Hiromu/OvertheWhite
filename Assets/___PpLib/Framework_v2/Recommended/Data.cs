using UnityEngine;

namespace PPD
{
    public class Data : ScriptableObject
    {
        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
        static void InitializeOnLoad()
        {
            Application.targetFrameRate = 30;
            var o = new GameObject("DontDestroyOnload Object");
            CoroutineHandler = o.AddComponent<CoroutineHandler>();
            GameObject.DontDestroyOnLoad(o);
        }

        public static CoroutineHandler CoroutineHandler { get; private set; }

        static Data _Ins;
        public static Data Ins
        {
            get
            {
                if (_Ins == null)
                {
                    _Ins = Resources.Load<Data>("DataSO");
                }
                return _Ins;
            }
        }

        public float emitDuration = 3;
        public float deadDuration = 3;

        public float nextSceneLoadHandler_shakeSec = 0.5f;
        public float nextSceneLoadHandler_fadeoutSec = 0.15f;
        public float nextSceneLoadHandler_fadeinSec = 0.4f;
        public Color nextSceneLoadHandler_defaultColor = new Color(0, 0, 0, 0);
        public Color nextSceneLoadHandler_peakColor = new Color(0, 0, 0, 1);
        public float walkSpeed;
        public float walkForce;
    }
}