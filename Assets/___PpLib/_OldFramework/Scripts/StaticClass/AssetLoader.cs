using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SR
{
    public static class AssetLoader
    {
        public static List<GameObject> LoadGameObject(string folderName)
        {
            List<GameObject> prefabs;
            var resources = Resources.LoadAll(folderName);
            prefabs = new List<GameObject>(resources.Length);
            foreach (var resource in resources)
            {
                if (resource is GameObject)
                {
                    prefabs.Add(resource as GameObject);
                }
            }
            return prefabs;
        }
    }
}