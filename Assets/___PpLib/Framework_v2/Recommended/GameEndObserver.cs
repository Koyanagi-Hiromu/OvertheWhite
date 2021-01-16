using UnityEngine;

namespace PPD
{
    /// <summary>
    /// 各プロジェクトで上書きしてください
    /// </summary>
    public class GameEndObserver : PPD_MonoBehaviour
    {
        private void Awake()
        {
        }

        private void Update()
        {
            if (Application.isEditor)
            {
                ObserveShortcut();
            }
        }

        private static void ObserveShortcut()
        {
            if (Input.GetKeyDown(KeyCode.R))
            {
                Debug.Log("Rを押されたので再読込します");
                NextSceneLoadHandler.Ins.BeginRetry(true);
            }
            if (Input.GetKeyDown(KeyCode.N))
            {
                Debug.Log("Nを押されたので次のステージへ進みます");
                NextSceneLoadHandler.Ins.BeginNextLevel();
            }
            if (Input.GetKeyDown(KeyCode.C))
            {
                Debug.Log("Cを押されたのでクリアしたことになります");
                GameClearScreen.Ins.Show();
            }
        }
    }
}