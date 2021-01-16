using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace SR
{
    public class Splash : MonoBehaviour
    {
#if ZH
        private void Awake()
        {
            LanguageManager.ChangeLanguage("Chinese (Simplified)");
        }
#endif

        private void Start()
        {
            this.StartCoroutine(LoadHomeScene());
        }

        private void OnOpenUrl(string url)
        {
            Debug.Log("Splash:OnOpenUrl");
            Debug.Log(url);
        }

        private　IEnumerator LoadHomeScene()
        {

            Debug.Log("Splash:Start");
            
            yield return new WaitForSeconds(1);

            var async = SceneManager.LoadSceneAsync("SR_HomeScene");

            async.allowSceneActivation = false;

            while (async.progress < 0.9f)
            {
                Debug.Log(async.progress);
                
                yield return 0;
            }

            Debug.Log("Splash:ロード完了");
            
            async.allowSceneActivation = true;

            yield return async;
        }
    }
}
