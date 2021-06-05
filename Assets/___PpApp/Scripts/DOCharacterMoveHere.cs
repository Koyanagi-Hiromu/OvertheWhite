using DG.Tweening;

namespace PPD
{
    public class DOCharacterMoveHere : DOPhaseComponent
    {
        public Character character;
        public float moveDuration;

        public override void EditorTransition()
        {
            character.transform.position = transform.position;
        }

        public override void OnUnityDisable() { }

        public override void OnUnityEnable()
        {
            character.transform.DOMove(transform.position, moveDuration);
        }
    }
}
