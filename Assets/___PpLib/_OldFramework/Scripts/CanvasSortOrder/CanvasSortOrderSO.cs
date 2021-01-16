using System;
using System.Collections;
using Sirenix.OdinInspector;
using UnityEngine;

namespace SR
{
    public class CanvasSortOrderSO : ScriptableObject
    {
        static CanvasSortOrderSO _Ins;
        public static CanvasSortOrderSO Ins
        {
            get
            {
                if (_Ins == null)
                {
                    _Ins = Resources.Load<CanvasSortOrderSO>("CanvasSortOrderSO");
                }
                return _Ins;
            }
        }

        [SerializeField, OnValueChanged("OnValueChange")]
        [ListDrawerSettings(Expanded = true, NumberOfItemsPerPage = int.MaxValue)]
        NamedSortOrder[] listMinus;

        [SerializeField, OnValueChanged("OnValueChange")]
        [ListDrawerSettings(Expanded = true, NumberOfItemsPerPage = int.MaxValue)]
        NamedSortOrder[] listPlus;

        void OnValueChange()
        {
            for (int i = 0; i < listMinus.Length; i++)
            {
                if (listMinus[i].id == 0)
                {
                    listMinus[i].id = DecideId();
                }
                listMinus[i].sortOrder = -10000 + (listMinus.Length - i) * -10;
            }

            for (int i = 0; i < listPlus.Length; i++)
            {
                if (listPlus[i].id == 0)
                {
                    listPlus[i].id = DecideId();
                }
                listPlus[i].sortOrder = 10000 + i * 10;
            }
        }

        int DecideId()
        {
            while (true)
            {
                var id = UnityEngine.Random.Range(int.MinValue, int.MaxValue);

                for (int i = 0; i < listMinus.Length; i++)
                {
                    if (id == listMinus[i].id) continue;
                }

                if (id == NamedSortOrder.DEFAULT.id) continue;

                for (int i = 0; i < listPlus.Length; i++)
                {
                    if (id == listPlus[i].id) continue;
                }

                if (id == 0) continue;

                return id;
            }
        }

        public static IEnumerable SortOrderList => Ins._SortOrderList();
        private IEnumerable _SortOrderList()
        {
            var list = new ValueDropdownList<int>();
            for (int i = 0; i < listMinus.Length; i++)
            {
                var e = listMinus[i];
                list.Add($"({e.sortOrder:00000}) {e.name}", e.id);
            }

            list.Add($"(______) {NamedSortOrder.DEFAULT.name}", 0);

            for (int i = 0; i < listPlus.Length; i++)
            {
                var e = listPlus[i];
                list.Add($"(+{e.sortOrder:00000}) {e.name}", e.id);
            }
            return list;
        }

        internal static NamedSortOrder Get(int hashId)
        {
            for (int i = 0; i < Ins.listPlus.Length; i++)
            {
                var e = Ins.listPlus[i];
                if (e.id == hashId)
                {
                    return e;
                }
            }
            for (int i = 0; i < Ins.listMinus.Length; i++)
            {
                var e = Ins.listMinus[i];
                if (e.id == hashId)
                {
                    return e;
                }
            }
            return NamedSortOrder.DEFAULT;
        }
    }

    [Serializable]
    public struct NamedSortOrder
    {
        public readonly static NamedSortOrder DEFAULT = new NamedSortOrder
        {
            name = "DEFAULT",
            sortOrder = 0,
            id = 0,
        };

        [HorizontalGroup(50), DisplayAsString, HideLabel, LabelWidth(50)] public int sortOrder;
        [HorizontalGroup, HideLabel] public string name;
        [HideInEditorMode, HideInPlayMode] public int id;
    }
}