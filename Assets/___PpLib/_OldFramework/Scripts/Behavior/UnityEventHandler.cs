using UnityEngine;
using UnityEngine.Events;

namespace SR.Commons
{
    public class UnityEventHandler : MonoBehaviour
    {
        public UnityEvent onAwake;
        public UnityEvent onStart;
        public UnityEvent onUpdate;
        
        protected virtual void Awake()
        {
            onAwake.Invoke();
        }
        
        protected virtual void Start()
        {
            onStart.Invoke();
        }
        
        protected virtual void Update()
        {
            onUpdate.Invoke();
        }

    }

}
