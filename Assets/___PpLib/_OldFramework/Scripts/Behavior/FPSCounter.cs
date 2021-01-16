using UnityEngine;

// http://smartgames.hatenablog.com/entry/2017/05/15/233905

namespace SR
{
    /// <summary>
    /// ~  Fps counter for unity  ~
    /// Brief : Calculate the FPS and display it on the screen 
    /// HowTo : Create empty object at initial scene and attach this script!!!
    /// </summary>
    public class FPSCounter : MonoBehaviour
    {
        public enum Anchor
        {
            TOP_LEFT,
            TOP_RIGHT,
            BOTTOM_LEFT,
            BOTTOM_RIGHT,
        }
        public Anchor anchor;
        // for ui.
        private int screenLongSide;
        private Rect boxRect;
        private GUIStyle style = new GUIStyle();

        // for fps calculation.
        private int frameCount;
        private float elapsedTime;
        private double frameRate;
        private float prevDeltaMSec;
        private float prevTrueDeltaMSec;

        /// <summary>
        /// Initialization
        /// </summary>
        private void Awake()
        {
            UpdateUISize();
        }

        /// <summary>
        /// Monitor changes in resolution and calcurate FPS
        /// </summary>
        private void Update()
        {
            // FPS calculation
            frameCount++;
            prevTrueDeltaMSec = Time.deltaTime * 1000;
            if (elapsedTime > 0.5f)
            {
                frameRate = System.Math.Round(frameCount / elapsedTime, 1, System.MidpointRounding.AwayFromZero);
                frameCount = 0;
                elapsedTime = 0;

                // Update the UI size if the resolution has changed
                if (screenLongSide != Mathf.Max(Screen.width, Screen.height))
                {
                    UpdateUISize();
                }
                prevDeltaMSec = prevTrueDeltaMSec;
            }
            elapsedTime += Time.deltaTime;
        }

        /// <summary>
        /// Resize the UI according to the screen resolution
        /// </summary>
        private void UpdateUISize()
        {
            screenLongSide = Mathf.Max(Screen.width, Screen.height);
            var rectLongSide = screenLongSide - 2;

            var w = Screen.width / 2;
            var h = 16;
            float x = 0;
            float y = 0;
            switch (anchor)
            {
                case Anchor.TOP_LEFT:
                case Anchor.TOP_RIGHT:
                    y = 1;
                    break;
                case Anchor.BOTTOM_LEFT:
                case Anchor.BOTTOM_RIGHT:
                    y = Screen.height - h - 1;
                    break;
            }
            switch (anchor)
            {
                case Anchor.TOP_LEFT:
                case Anchor.BOTTOM_LEFT:
                    x = 1;
                    break;
                case Anchor.TOP_RIGHT:
                case Anchor.BOTTOM_RIGHT:
                    x = Screen.width - w - 1;
                    break;
            }
            boxRect = new Rect(x, y, w, h);
            style.fontSize = (int)(screenLongSide / 36.8);
            style.normal.textColor = Color.white;
        }

        /// <summary>
        /// Display FPS
        /// </summary>
        private void OnGUI()
        {
            GUI.Box(boxRect, "");
            GUI.Label(boxRect, " " + frameRate.ToF1() + "/" + Application.targetFrameRate + " | x" + Time.timeScale.ToF1() + " | " + prevDeltaMSec.ToF1() + " ms" + " | " + prevTrueDeltaMSec.ToF1() + " ms" , style);
        }

        private string ToF1(float f)
        {
            var str = f.ToString("F1");
            if (!str.Contains("."))
            {
                return str + ".0";
            }
            else
            {
                return str;
            }
        }
    }
}