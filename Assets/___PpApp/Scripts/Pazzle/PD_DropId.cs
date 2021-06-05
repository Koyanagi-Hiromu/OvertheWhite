using System;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

namespace PPD
{
    public class PD_DropId : ScriptableObject
    {
        public int id;
        public Sprite looks;
        public Color debugColor;
        public string debugSimbol;
        public bool IsEmpty() => id == 0;
    }

    [Serializable]
    public class PD_DropIdList
    {
        [SerializeField] PD_DropId[] dropIdArray;
        Dictionary<int, PD_DropId> dictionary;
        public void Init()
        {
            dictionary = new Dictionary<int, PD_DropId>();
            foreach (var dropId in dropIdArray)
            {
                dictionary[dropId.id] = dropId;
            }
            PD_DropData.SetEmptyData(dictionary[0]);
        }

        public PD_DropId Get(int id)
        {
            PD_DropId ret;
            if (dictionary.TryGetValue(id, out ret))
            {
                return ret;
            }
            else
            {
                return Data.Ins.dropId_error;
            }
        }
    }
}
