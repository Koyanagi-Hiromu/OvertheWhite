using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace PPD
{
    public class GameClearScreen : SingletonMonoBehaviour<GameClearScreen>
    {
        public TextMeshProUGUI newRecord;
        public TextMeshProUGUI score;
        public GameObject screen;


        [ShowInInspector, DisplayAsString]
        internal int currentLevel;
        bool isTestScene;

        protected override void UnityAwake()
        {
        }

        public void Show()
        {
            screen.SetActive(true);
        }

        public void Show(bool newRecord, string scoreText)
        {
            this.newRecord.SetActive(newRecord);
            screen.SetActive(true);
            this.score.text = scoreText;
        }
    }
}