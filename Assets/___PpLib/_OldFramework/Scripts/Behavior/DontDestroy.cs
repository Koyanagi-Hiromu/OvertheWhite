using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace SR
{
    public class DontDestroy : MonoBehaviour
    {
        public MonoBehaviour singletonType;
        private static List<Type> list = new List<Type>();
        void Awake()
        {
            if (singletonType)
            {
                if (list.Contains(singletonType.GetType()))
                {
                    this.DestroyInstance();
                    return;
                }
                else
                {
                    list.Add(singletonType.GetType());
                }
            }
            DontDestroyOnLoad(this.gameObject);
        }
    }
}
