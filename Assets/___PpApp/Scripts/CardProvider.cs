namespace PPD
{
    /// <summary>
    /// CardBody
    /// </summary>
    public class CardProvider : PPD_MonoBehaviour
    {
        public CardZoneController zone;

        private void Update()
        {
            zone.UnityUpdate();
        }
    }
}
