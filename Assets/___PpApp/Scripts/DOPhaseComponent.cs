using System;

namespace PPD
{
    public abstract class DOPhaseComponent : PPD_MonoBehaviour
    {
        public abstract void Init();
        public abstract void FlashMove();
        public abstract void DOStart();
        public abstract void DOKill();

        /// <summary>
        /// prev.OnUncurrent() -> next.OnCurrent()
        /// </summary>
        internal void OnPhaseCurrent() => DOStart();

        /// <summary>
        /// prev.OnUncurrent() -> next.OnCurrent()
        /// </summary>
        internal void OnPhaseUncurrent() => DOKill();
    }
}
