using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace PPD
{
    public static class ED_Duplicator
    {
        [MenuItem("Pocketpair/Topに複製 %#d")]
        private static void DuplicateAtTop()
        {
            if (Selection.gameObjects != null && Selection.gameObjects.Length > 0)
            {
                var array = new Object[Selection.gameObjects.Length];

                for (var i = 0; i < Selection.gameObjects.Length; i++)
                {
                    var n = Selection.gameObjects[i];
                    var clone = GameObject.Instantiate(n);
                    // var parent = n.transform.parent;
                    clone.transform.SetParent(null);
                    clone.transform.SetSiblingIndex(0);
                    clone.transform.localPosition = n.transform.localPosition;
                    clone.transform.localRotation = n.transform.localRotation;
                    clone.transform.localScale = n.transform.localScale;
                    clone.name = n.name;
                    // clone.name = GameObjectUtility.GetUniqueNameForSibling(parent, clone.name);
                    array[i] = clone;
                }

                Selection.objects = array;
            }
        }
    }
}
