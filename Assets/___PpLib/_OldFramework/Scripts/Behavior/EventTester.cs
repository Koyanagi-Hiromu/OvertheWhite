using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
namespace SR
{
    public class EventTester : MonoBehaviour
    {
        public void SayHello()
        {
            SLog.System.Info("Hello");
        }

        public void SayGoodBye()
        {
            SLog.System.Info("GoodBye");
        }

        public void SayILoveYou()
        {
            SLog.System.Info("I dont think you love me.");
        }

        public void WriteError(string text)
        {
            SLog.Event.Error(text);
        }

        public void WriteLog(string text)
        {
            SLog.Event.Info(text);
        }

        public void WriteLog_Temporary(string text)
        {
            SLog.Temporary.Info(text);
        }

        public void WriteError_Temporary(string text)
        {
            SLog.Temporary.Error(text);
        }

        /// <summary>
        /// prefabからオブジェクトを生成します
        /// </summary>
        /// <param name="pf"></param>
        public void Instantiate(GameObject pf)
        {
            pf.Instantiate();
        }

        public void LoadScene_Add(string name)
        {
            SceneManager.LoadScene(name, LoadSceneMode.Additive);
        }

        public void LoadScene_Replace(string name)
        {
            // 問題なければ this.LoadSceneAsync_Replace を採用
            //SceneManager.LoadScene(name, LoadSceneMode.Single);
            StartCoroutine(this.LoadSceneAsync_Replace(name));
        }

        private IEnumerator LoadSceneAsync_Replace(string name)
        {
            yield return SceneManager.LoadSceneAsync(name, LoadSceneMode.Single);
        }
    }
}
