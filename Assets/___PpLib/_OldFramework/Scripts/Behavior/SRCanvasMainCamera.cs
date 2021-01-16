using UnityEngine;

namespace SR
{
    public class SRCanvasMainCamera : AssistantBehaviour<Canvas>
    {
        private void Awake()
        {
            owner.worldCamera = Camera.main;
        }
    }
}