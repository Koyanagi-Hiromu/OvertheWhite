using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace PPD
{
    public class AutoTransition : PPD_MonoBehaviour
    {
        public float sec;
        public string nextSceneName;
        private void Awake()
        {
            this.StartCoroutine(Er());
        }

        IEnumerator Er()
        {
            yield return new WaitForSeconds(sec);
            SceneManager.LoadScene(nextSceneName);
        }
    }
}
