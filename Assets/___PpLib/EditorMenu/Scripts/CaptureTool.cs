using System;
using System.IO;
using System.Collections;
using UnityEngine;
using Sirenix.OdinInspector;

namespace PPD
{
    public class CaptureTool : MonoBehaviour
    {
        [ContextMenu("キャプチャー")]
        [Button("キャプチャー")]
        private void Caputure()
        {
            this.StartCoroutine(this.LoadScreenshot());
        }

        private IEnumerator LoadScreenshot()
        {
            yield return new WaitForEndOfFrame();

            var texture = new Texture2D(Screen.width, Screen.height);

            texture.ReadPixels(new Rect(0, 0, Screen.width, Screen.height), 0, 0);

            texture.Apply();

            var bytes = texture.EncodeToPNG();
            var now = DateTime.Now.ToString("yyyyMMdd-HHmmss-fff");

            File.WriteAllBytes($"{Application.dataPath}/../Capture/{now}.png", bytes);
        }
    }
}