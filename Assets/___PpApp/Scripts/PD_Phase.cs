using System;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

namespace PPD
{
    [TypeInfoBox("直下のアクティブな子しか取得しないので注意")]
    public class PD_Phase : PPD_MonoBehaviour
    {
        bool inited;
        List<DOPhaseComponent> doPhaseComponents;

        private void Init()
        {
            inited = true;
            doPhaseComponents = GetComponentsInChildrenJustBelow<DOPhaseComponent>();

            foreach (var d in this.doPhaseComponents)
            {
                d.Init();
            }
        }

        [Button(ButtonSizes.Large), GUIColor(1, 1, .5f, 1)]
        public void FlashMove()
        {
            GetComponentsInChildrenJustBelow<DOPhaseComponent>().ForEach(d => d.FlashMove());
        }

        [Button(ButtonSizes.Gigantic), GUIColor(1, .5f, 1, 1)]
        public void SetCurrent() => PD_PhaseManager.Ins.SetCurrent(this);

        [Button(ButtonSizes.Large), GUIColor(.5f, 1, 1, 1)]
        public void SetUncurrent() => PD_PhaseManager.Ins.SetUncurrent(this);

        /// <summary>
        /// prev.OnUncurrent() -> next.OnCurrent()
        /// </summary>
        public void OnUncurrent()
        {
            this.gameObject.SetActive(false);
            foreach (var d in doPhaseComponents)
            {
                d.OnPhaseUncurrent();
            }
        }

        /// <summary>
        /// prev.OnUncurrent() -> next.OnCurrent()
        /// </summary>
        public void OnCurrent()
        {
            if (!inited) Init();

            this.gameObject.SetActive(true);
            foreach (var d in doPhaseComponents)
            {
                d.OnPhaseCurrent();
            }
        }
    }
}
