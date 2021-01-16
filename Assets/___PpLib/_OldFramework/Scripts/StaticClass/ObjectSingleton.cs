using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SR
{

    public abstract class ObjectSingleton
    {
        public abstract void Destroy();
    }

    public abstract class ObjectSingleton<T> : ObjectSingleton
    where T : ObjectSingleton<T>, new()
    {
        private static T ins;
        public static T Ins
        {
            get
            {
                if (ins == null)
                {
                    ins = new T();
                    ins.OnGenerate();
                }
                return ins;
            }
        }

        protected abstract void OnGenerate();

        public override void Destroy() => Reset();
        public static void Reset() => ins = null;
    }
}