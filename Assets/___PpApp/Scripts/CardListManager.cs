using System.Collections.Generic;

namespace PPD
{
    public class CardListManager : SingletonMonoBehaviour<CardListManager>
    {
        public List<CardProvider> list = new List<CardProvider>();
        protected override void UnityAwake()
        {
            throw new System.NotImplementedException();
        }
    }
}
