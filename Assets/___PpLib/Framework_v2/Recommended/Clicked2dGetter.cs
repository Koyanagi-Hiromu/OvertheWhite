using UnityEngine;

namespace PPD
{
    public class Clicked2dGetter : MonoBehaviour
    {
        public LayerMask layerMask;
        void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
                var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit2D[] results = new RaycastHit2D[1];
                var count = Physics2D.RaycastNonAlloc(ray.origin, ray.direction, results, 100, layerMask);

                if (count > 0)
                {
                    results[0].transform.gameObject.SetActive(false);
                }
            }
        }
    }
}