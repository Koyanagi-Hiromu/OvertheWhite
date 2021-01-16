
using System.Linq;

namespace SR
{
    /// <summary>
    /// 自動で登録されるシングルトンです。
    /// バージョン情報などゲーム起動時から終了時まで保持し続ける情報を扱うために使用してください。
    /// 該当ファイル(scriptableObject.asset)の生成注意：
    /// ① Resourcesフォルダの下で生成してください。
    /// ② Project内でファイルを１つだけにしてください。
    /// </summary>
    public abstract class AutoSingletonScriptableObject<T> : SploveScriptableObject
     where T : SploveScriptableObject
    {
        private static T ins;
        public static T Ins
        {
            get
            {
                if (!ins)
                {
                    var list = UnityEngine.Resources.FindObjectsOfTypeAll<T>();
                    Assert.IsTrue(list.Count() == 1);
                    ins = list[0];
                }
                return ins;
            }
        }

    }

}
