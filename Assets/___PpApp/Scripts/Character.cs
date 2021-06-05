using SR;
using UnityEngine;

namespace PPD
{
    public class Character : PPD_MonoBehaviour
    {
        public Billboard billBoard;
        public Character_SpriteRenderer spriteRenderer;
        public SpeechBabble speechBabble;
    }

    [RequireComponent(typeof(Character))]
    public abstract class CharacterComponent : PPD_MonoBehaviour
    {
        protected Character owner { get; private set; }
        private void Awake()
        {
            this.owner = GetComponent<Character>();
        }
    }
}
