namespace PPD
{
    public class CommandPhaseManager : SingletonMonoBehaviour<CommandPhaseManager>
    {

        protected override void UnityAwake()
        {
        }
    }

    /// <summary>
    /// 戦闘前に山札を生成するために保持している情報
    /// </summary>
    public class PlayerDeck : PPD_MonoBehaviour { }

}
