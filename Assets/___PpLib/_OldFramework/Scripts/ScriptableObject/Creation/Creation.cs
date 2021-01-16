using System.Collections.Generic;
using System.Linq;
using Sirenix.OdinInspector;

namespace SR
{
    public interface ICreation
    {
        void OnAdd();
        void OnRemove();
        void OnManagerDestroy();
        int ListCount { get; }
        Creation[] ListForUnityEditor { get; }
    }

    public abstract class Creation { }
    public abstract class Creation<T_Self, T_Owner, TA, TB, TC> : Creation, ICreation
    where T_Self : Creation
    where T_Owner : CreationSource
    where TA : struct
    where TB : struct
    where TC : struct
    {
        public static T_Owner source { get; private set; }
        public static void SetOwnerSource(T_Owner value) => source = value;

        static QuickArray255<T_Self> array = new QuickArray255<T_Self>();
        static T_Self ins;
        public static T_Self Ins => ins;
        public static IReadOnlyList<T_Self> list => array;
        public static int Count => array.Count;
        public static bool IsActive => Ins != null;

        [ShowInInspector, ReadOnly] public abstract TA X { get; }
        [ShowInInspector, ReadOnly] public abstract TB Y { get; }
        [ShowInInspector, ReadOnly] public abstract TC Z { get; }

        public Creation[] ListForUnityEditor => array.Where(e => e != null).ToArray();
        public int ListCount => array.Count;
        void ICreation.OnAdd()
        {
            ins = this as T_Self;
            array.Add(ins);
        }

        void ICreation.OnRemove()
        {
            array.Remove(this as T_Self);
            if (Count == 0)
                ins = null;
            else
                ins = array.FastTake();
        }

        void ICreation.OnManagerDestroy()
        {
            array.Clear();
        }
    }

    public struct Empty { }
}