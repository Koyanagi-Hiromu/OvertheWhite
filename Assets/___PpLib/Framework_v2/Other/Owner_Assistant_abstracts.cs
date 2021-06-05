using PPD.Hidden;
using Sirenix.OdinInspector;
using UnityEngine;

namespace PPD
{
    public class AssistantHolder : MonoBehaviour { }

    namespace Hidden
    {
        public abstract class OwnerMonoBehaviour : MonoBehaviour
        {
            public abstract object ___AH___ { get; }
        }
    }

    public abstract class OwnerMonoBehaviour<T> : OwnerMonoBehaviour
    where T : AssistantHolder
    {
        [SerializeField, ReadOnly, HideInInlineEditors] T _aHolder;
        public T aHolder => _aHolder;
        public override sealed object ___AH___
        {
            get
            {
                TrySetC();
                return _aHolder;
            }
        }

#if UNITY_EDITOR
        bool noC => _aHolder == null;
#endif

        [Button("Holderを登録"), ShowIf("noC")]
        private void TrySetC()
        {
            if (_aHolder == null)
            {
                _aHolder = this.GetComponent<T>();
                if (_aHolder == null)
                {
                    _aHolder = gameObject.AddComponent<T>();
                    _aHolder.transform.SetSiblingIndex(this.transform.GetSiblingIndex() + 1);
                }
            }
        }

        [Button("Noneを直下に生成"), HideIf("noC"), HideInInlineEditors]
        void Generate()
        {
            var fields = aHolder.GetType().GetFields();
            foreach (var field in fields)
            {
                if (field.FieldType.IsSubclassOf(typeof(AssistantMonoBehaviour)))
                {
                    var value = field.GetValue(aHolder) as AssistantMonoBehaviour;
                    if (value == null)
                    {
                        var c = gameObject.AddComponent(field.FieldType) as AssistantMonoBehaviour;
                        field.SetValue(aHolder, c);
                        c.SetOwner(this);
                    }
                }
            }
        }
    }

    public abstract class SingletonOwnerMonoBehaviour<T, K> : OwnerMonoBehaviour<K>
    where T : SingletonOwnerMonoBehaviour<T, K>
    where K : AssistantHolder
    {
        public static T Ins { get; private set; }

        protected abstract void UnityAwake();

        private void Awake()
        {
            if (enabled == false)
            {
                Log.Check.Info("Manager should be enabled.　:" + this);
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

    public abstract class AssistantMonoBehaviour : MonoBehaviour
    {
        public abstract void SetOwner(OwnerMonoBehaviour owner);
    }

    public abstract class AssistantMonoBehaviour<T> : AssistantMonoBehaviour
    where T : OwnerMonoBehaviour
    {
        [SerializeField, ReadOnly] T _owner;
        public T owner => _owner;
        public override void SetOwner(OwnerMonoBehaviour owner) => _owner = owner as T;

#if UNITY_EDITOR
        bool RequireRegister
        {
            get
            {
                if (_owner == null)
                    return true;

                if (_owner.gameObject == this.gameObject)
                    return false;

                return true;
            }
        }

        [Button("自動登録", ButtonSizes.Gigantic), ShowIf("RequireRegister"), HideInInlineEditors]
        public void Register()
        {
            var owner = FindOwner(this.transform);

            if (owner == null)
            {
                EX_Pop.ED_Error("親が見つかりませんでした。\n親階層か自分と同じ階層に用意してください。");
            }
            else
            {
                this._owner = owner;
                Register(owner.___AH___);
                UnityEditor.Selection.activeObject = owner;
            }
        }

        T FindOwner(Transform t)
        {
            if (t == null)
            {
                return null;
            }
            else
            {
                var gate = t.GetComponent<T>();
                if (gate != null)
                {
                    return gate;
                }
                else
                {
                    return FindOwner(t.parent);
                }
            }
        }

        bool Register(object c)
        {
            var fields = c.GetType().GetFields();
            foreach (var field in fields)
            {
                if (field.FieldType == this.GetType())
                {
                    field.SetValue(c, this);
                    return true;
                }
            }

            EX_Pop.ED_Error("親にコンポーネントホルダーが見つかりませんでした。\n親階層か自分と同じ階層に用意してください。");
            return false;
        }
#endif
    }
}
