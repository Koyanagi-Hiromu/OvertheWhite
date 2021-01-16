using System.Collections;
using Sirenix.OdinInspector;
using UnityEngine;

namespace SR
{
    [RequireComponent(typeof(Canvas))]
    public class CanvasSortOrder : MonoBehaviour
    {
        NamedSortOrder namedSortOrder => CanvasSortOrderSO.Get(id);
        [SerializeField, HideLabel, ValueDropdown("EnId")] int id;
        IEnumerable EnId => CanvasSortOrderSO.SortOrderList;
        [ShowInInspector, HideLabel] CanvasSortOrderSO so => CanvasSortOrderSO.Ins;

        [SerializeField, MinValue(0), MaxValue(9)] int plus;

        void Awake() => SetCanvas();

        [OnInspectorGUI]
        private void SetCanvas()
        {
            var owner = GetComponent<Canvas>();
            owner.overrideSorting = true;
            owner.sortingOrder = namedSortOrder.sortOrder + plus;
        }
    }
}