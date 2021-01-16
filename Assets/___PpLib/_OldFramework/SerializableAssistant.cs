using Sirenix.OdinInspector;
using UnityEngine;

namespace SR
{
    public class SerializableAssistant<T>
    where T : SploveBehaviour
    {
        public T owner { get; private set; }

        //TODO: sealed
        public virtual void SetOwner(T owner)
        {
            this.owner = owner;
            OnSetOwner();
        }

        //TODO: abstract
        protected virtual void OnSetOwner() { }
    }

    public abstract class SAssistant<T>
    where T : MonoBehaviour
    {
        public T owner { get; private set; }
        public void SetOwner(T owner)
        {
            this.owner = owner;
            OnSetOwner();
        }
        protected abstract void OnSetOwner();
    }

    public abstract class Assistant<T>
    {
        [ShowInInspector, ReadOnly]
        public T owner { get; private set; }
        public Assistant(T owner)
        {
            this.owner = owner;
        }
    }

    public abstract class AssistantBehaviour<T> : SploveBehaviour
    where T : Component
    {
        [SerializeField] private T _owner;
        public T owner { get => _owner; }

        [HideIf("IsAnyOwner")]
        [Button("オーナーセット", ButtonSizes.Large)]
        public void SetOwnerAutomatically()
        {
            this._owner = this.GetComponent<T>();
        }

        public bool IsAnyOwner()
        {
            return this._owner != null;
        }
    }
}
