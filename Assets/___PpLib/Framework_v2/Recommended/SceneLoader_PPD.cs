using UnityEngine.SceneManagement;

namespace PPD
{
    public class SceneLoader_PPD : PPD_MonoBehaviour
    {
        public void Reload()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }
}