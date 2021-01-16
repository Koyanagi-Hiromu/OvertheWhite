

namespace SR
{
    /// <summary>
    /// 手動で登録することで使用可能になるシングルトンです。
    /// ① シーンにManualSingletonResisteeをもつGameObjectを置いてください。
    /// ② ManualSingletonResisteeに使用するファイルをに登録してください。
    /// AutoSingletonScriptableObjectとは異なり、Project内にファイルをいくつつくっても構いません。
    /// </summary>
    public abstract class ManualSingletonScriptableObject<T> : MSSO
     where T : MSSO
    {
        public static T Ins { get; private set; }

        public override void OnResister()
        {
            Ins = this as T;
        }

    }

    public abstract class MSSO : SploveScriptableObject
    {
        public abstract void OnResister();

    }

}
