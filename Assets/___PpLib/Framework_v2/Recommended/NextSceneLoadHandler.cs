using DG.Tweening;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace PPD
{
    public class NextSceneLoadHandler : SingletonMonoBehaviour<NextSceneLoadHandler>
    {
        public static Tweener ShakeCamera() => Camera.main.DOShakePosition(0.3f, 0.3f, 10);
        static Sequence seq;
        public Image blackoutImage;
        public Canvas canvas;

        [ShowInInspector, DisplayAsString] internal int currentLevel;
        bool isTestScene;

        protected override void UnityAwake()
        {
            var sceneName = SceneManager.GetActiveScene().name;
            // currentLevel = int.Parse(sceneName.Substring(6));

            var name = SceneManager.GetActiveScene().name;
            var number = name.Substring(name.LastIndexOf("_") + 1);
            var success = int.TryParse(number, out currentLevel);
            if (!success)
            {
                isTestScene = true;
            }
        }

        public void BeginRetry(bool shake) => Begin(shake, RetryLevel_WithoutAnim);
        public void BeginNextLevel() => Begin(false, GotoNextLevel_WithoutAnim);
        void Begin(bool shake, TweenCallback callback)
        {
            if (seq != null && seq.IsActive() && seq.IsPlaying())
            {
                seq.Kill();
            }

            Time.timeScale = 0;

            blackoutImage.color = Data.Ins.nextSceneLoadHandler_defaultColor;
            canvas.transform.SetParent(null, false);
            DontDestroyOnLoad(canvas.gameObject);
            canvas.SetActive(true);

            seq = DOTween.Sequence().SetUpdate(true);

            if (shake)
            {
                seq.Append(ShakeCamera())
                .AppendInterval(Data.Ins.nextSceneLoadHandler_shakeSec);
            }

            seq.Append(blackoutImage.DOColor(Data.Ins.nextSceneLoadHandler_peakColor, Data.Ins.nextSceneLoadHandler_fadeoutSec))
            .AppendCallback(() => Time.timeScale = 1)
            .AppendCallback(callback)
            .Append(blackoutImage.DOColor(Data.Ins.nextSceneLoadHandler_defaultColor, Data.Ins.nextSceneLoadHandler_fadeinSec))
            .OnKill(() =>
            {
                if (canvas)
                {
                    canvas.DestroyInstance();
                }
            });
        }

        void RetryLevel_WithoutAnim()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }

        void GotoNextLevel_WithoutAnim()
        {
            var name = SceneManager.GetActiveScene().name;
            var head = name.Substring(0, name.LastIndexOf("_"));

            string sceneName;
            if (isTestScene)
                sceneName = $"{head}_00001";
            else
                sceneName = $"{head}_{currentLevel + 1:00000}";

            if (Application.CanStreamedLevelBeLoaded(sceneName))
                SceneManager.LoadScene(sceneName);
            else
                SceneManager.LoadScene(0);
        }
    }
}