using UnityEngine;

namespace PPD
{
    public abstract class _Data_ForReadability : ScriptableObject
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
                    _Ins = Resources.Load<Data>("_DataSO");
                }
                return _Ins;
            }
        }
    }
}