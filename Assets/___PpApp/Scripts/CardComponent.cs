namespace PPD
{
    public abstract class CardComponent : PPD_MonoBehaviour
    {
        public CardProvider provider { get; private set; }
        private void Awake()
        {
            provider = GetComponent<CardProvider>();
        }
    }
}