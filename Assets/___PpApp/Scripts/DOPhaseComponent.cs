namespace PPD
{
    public abstract class DOPhaseComponent : PPD_MonoBehaviour
    {
        internal bool ignoreOnEnable;
        public abstract void EditorTransition();
        public abstract void OnUnityEnable();
        public abstract void OnUnityDisable();

        private void OnEnable()
        {
            if (!ignoreOnEnable)
            {
                OnUnityEnable();
            }
        }

        private void OnDisable()
        {
            OnUnityDisable();
        }
    }
}
