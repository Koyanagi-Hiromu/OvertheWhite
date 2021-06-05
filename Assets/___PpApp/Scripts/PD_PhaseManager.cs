using System;

namespace PPD
{
    public class PD_PhaseManager : SingletonMonoBehaviour<PD_PhaseManager>
    {
        internal PD_Phase current;
        protected override void UnityAwake()
        {
        }

        internal void OnPhaseEnable(PD_Phase pD_PhaseTransition)
        {
            if (current != null)
            {
                current.SetActive(false);
            }
            current = pD_PhaseTransition;
        }
    }
}
