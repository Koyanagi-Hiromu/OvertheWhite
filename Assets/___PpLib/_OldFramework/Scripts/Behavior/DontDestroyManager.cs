namespace SR
{
    public abstract class DontDestroyManager : SploveBehaviour
    {
        public abstract void InitOnGenerate();
    }

    public abstract class DontDestroyManager<T> : DontDestroyManager
    where T : DontDestroyManager<T>
    {
        public static T Ins { get; private set; }

        private void Awake() { }
        public sealed override void InitOnGenerate()
        {
            if (enabled == false)
            {
                SLog.System.Info("Manager should be enabled.　:" + this);
                return;
            }
            if (Ins == null)
            {
                //ゲーム開始時にGameManagerをinstanceに指定ß
                Ins = this as T;
                // SLog.System.Info($"=========================================");
                // SLog.System.Info($"{Ins.GetType().FullName} UnityAwake()");
                UnityAwake();
                // SLog.System.Info($"{Ins.GetType().FullName} UnityAwake() completed");
            }
            else if (Ins != this)
            {
                SLog.System.Info(Ins);
                Assert.UnReachable("ジェネリック間違えてない？ コンポーネントが２つあるかも？");
                this.DestroyComponent();
            }
            else
            {
                // Do Nothing
            }
        }

        protected abstract void UnityAwake();

        protected virtual void OnDestroy()
        {
            Ins = null;
        }
    }
}
