﻿using Sirenix.OdinInspector;

namespace PPD
{
    public class PD_Phase : PPD_MonoBehaviour
    {
        public DOPhaseComponent[] doPhaseComponents;

        [Button(ButtonSizes.Large), GUIColor(1, 1, .5f, 1)]
        public void EditorMove()
        {
            foreach (var doMoveHere in GetComponentsInChildren<DOPhaseComponent>())
            {
                doMoveHere.EditorTransition();
            }
        }

        private void Awake()
        {
            doPhaseComponents = GetComponentsInChildren<DOPhaseComponent>();
        }

        [Button(ButtonSizes.Gigantic), GUIColor(.5f, .5f, 1, 1)]
        public void SetActive()
        {
            this.gameObject.SetActive(true);
        }

        private void OnEnable()
        {
            PD_PhaseManager.Ins.OnPhaseEnable(this);
            foreach (var doPhaseComponent in doPhaseComponents)
            {
                doPhaseComponent.OnUnityEnable();
            }
        }
    }
}
