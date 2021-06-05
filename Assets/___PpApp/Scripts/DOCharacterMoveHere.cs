using DG.Tweening;

namespace PPD
{
    public class DOCharacterMoveHere : PPD_MonoBehaviour, IDOPhaseComponent
    {
        public Character character;
        public float moveDuration;

        public void EditorTransition()
        {
            character.transform.position = transform.position;
        }

        public void OnEnable()
        {
            character.transform.DOMove(transform.position, moveDuration);
        }
    }
}
