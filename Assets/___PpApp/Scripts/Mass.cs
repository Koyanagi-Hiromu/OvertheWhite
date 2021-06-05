using UnityEngine;

namespace PPD
{
    public class Mass : PPD_MonoBehaviour
    {
        public SpriteRenderer spriteRenderer;
        internal Vector2Int pos;
        internal bool selected;
        public void Repaint(PD_DropData data)
        {
            spriteRenderer.sprite = data.dropId.looks;
            spriteRenderer.color = selected ? new Color(1, 1, 1, .5f) : Color.white;
        }
    }
}
