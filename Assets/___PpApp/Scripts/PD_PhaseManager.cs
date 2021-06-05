using System;
using UnityEngine;

namespace PPD
{
    public class PD_PhaseManager : SingletonMonoBehaviour<PD_PhaseManager>
    {
        internal PD_Phase current { get; private set; }
        protected override void UnityAwake()
        {
        }

        private void Start()
        {
            var firstPhase = GetComponentInChildren<PD_Phase>();
            if (firstPhase != null)
            {
                firstPhase.SetCurrent();
            }
        }

        internal void SetCurrent(PD_Phase phase)
        {
            var prev = current;
            current = phase;
            if (prev != null)
            {
                prev.OnUncurrent();
            }
            current.OnCurrent();
        }

        internal void SetUncurrent(PD_Phase phase)
        {
            if (current = phase)
            {
                var prev = current;
                current = null;
                prev.OnUncurrent();
            }
        }
    }
}
