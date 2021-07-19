using Sirenix.OdinInspector;

namespace PPD
{
    public enum Zone
    {
        _N_A_, Hand, Discard, Draw, Exhaust, Effect,
    }

    public class CardZoneController : CardComponent
    {
        [ShowInInspector, ReadOnly] Zone currentZone = Zone._N_A_;

        public void UnityUpdate()
        {

        }

        public void SetZone(Zone next)
        {
            // currentZone.OnExit();
            // currentZone = next;
            // currentZone.OnEnter();
        }
    }
}
