using System;
using Sirenix.OdinInspector;
using UnityEngine;

namespace SR
{
    public abstract class SingletonMonoBehaviour<T> : SploveBehaviour
     where T : SingletonMonoBehaviour<T>
    {
        public static T Ins { get; private set; }

        protected abstract void UnityAwake();

        private void Awake()
        {
            if (enabled == false)
            {
                SLog.System.Info("Manager should be enabled.　:" + this);
                return;
            }
            if (Ins == null)
            {
                //ゲーム開始時にGameManagerをinstanceに指定ß
                Ins = this as T;
                UnityAwake();
            }
            else if (Ins != this)
            {
                Assert.UnReachable("ジェネリック間違えてない？ コンポーネントが２つあるかも？: " + Ins);
                this.DestroyComponent();
            }
            else
            {
                // Do Nothing
            }
        }

        protected virtual void OnDestroy()
        {
            Ins = null;
        }

#if UNITY_EDITOR
        public static T InsEditor()
        {
            if (Ins != null)
            {
                return Ins;
            }
            else
            {
                Ins = GameObject.FindObjectOfType<T>();
                return Ins;
            }
        }
#endif
    }
}
