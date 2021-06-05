using System.Diagnostics;
using DG.Tweening;
using Sirenix.OdinInspector;

namespace PPD
{
    public class DOCharacterMoveHere : DOPhaseComponent
    {
        public Character character;
        public float moveDuration;

        public override void Init() { }

        [Conditional("DEVELOPMENT_BUILD"), Conditional("UNITY_EDITOR")]
        [Button(ButtonSizes.Gigantic)]
        public void EDITOR_ResetTransform()
        {
            UnityEditor.Undo.RecordObject(transform, "ResetTransform");
            transform.position = character.transform.position;
        }

        public override void FlashMove()
        {
            character.transform.position = transform.position;
            character.billBoard.LookInInspector();
        }

        public override void DOKill() { }

        public override void DOStart()
        {
            character.transform.DOMove(transform.position, moveDuration);
        }
    }
}
