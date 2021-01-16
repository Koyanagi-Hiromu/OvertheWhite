using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace SR
{
    public class DeleteIfAnotherSceneExists : MonoBehaviour
    {
        private void Start()
        {
            if (SceneManager.sceneCount > 1)
            {
                this.DestroyInstance();
            }
        }
    }
}
