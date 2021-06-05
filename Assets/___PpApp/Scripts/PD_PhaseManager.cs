using System;

namespace PPD
{
    public class PD_PhaseManager : SingletonMonoBehaviour<PD_PhaseManager>
    {
        internal PD_Phase current;
        protected override void UnityAwake()
        {
        }

        internal void OnPhaseEnable(PD_Phase phase)
        {
            if (current != null)
            {
                current.SetActive(false);
            }
            current = phase;
        }

        internal void OnPhaseDisable(PD_Phase phase)
        {
            if (current = phase)
            {
                current = null;
            }
        }
    }
}
