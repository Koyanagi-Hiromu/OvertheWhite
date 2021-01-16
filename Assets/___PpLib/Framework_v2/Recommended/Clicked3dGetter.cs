using UnityEngine;

namespace PPD
{
    public class Clicked3dGetter : MonoBehaviour
    {
        public LayerMask layerMask;
        void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
                var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit[] results = new RaycastHit[1];
                var count = Physics.RaycastNonAlloc(ray, results, 100, layerMask);

                if (count > 0)
                {
                    results[0].transform.gameObject.SetActive(false);
                }
            }
        }
    }
}