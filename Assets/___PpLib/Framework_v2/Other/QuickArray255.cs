using System.Collections;
using System.Collections.Generic;

namespace PPD
{
    ///<summary>
    /// indexが挿入される番号が保証されません。
    /// 
    /// 例えば Add(10) Add(20) と入力しても、[20, 10] となりえます。
    ///</summary>
    public class QuickArray255<T> : IReadOnlyList<T>
    {
        public const int MAX = 255;
        T[] array = new T[MAX];
        int count;
        public int Count => count;

        T IReadOnlyList<T>.this[int index] => array[index];

        ///<summary>
        /// 最もindex番号が小さいnull以外のものを返します。
        ///</summary>
        public T FastTake()
        {
            for (var i = 0; i < MAX; i++)
            {
                if (array[i] != null)
                {
                    return array[i];
                }
            }
            return default(T);
        }

        public bool IsEmpty() => count == 0;

        public int Add(T e)
        {
            for (var i = count; i < MAX; i++)
            {
                if (array[i] == null)
                {
                    array[i] = e;
                    count++;
                    return i;
                }
            }
            for (var i = 0; i < count; i++)
            {
                if (array[i] == null)
                {
                    array[i] = e;
                    count++;
                    return i;
                }
            }
            throw new System.Exception("Creation上限に達したので追加を無視しました。");
        }

        public bool Remove(T e)
        {
            for (var i = 0; i < MAX; i++)
            {
                if (array[i].Equals(e))
                {
                    var ret = array[i];
                    array[i] = default(T);
                    count--;
                    return true;
                }
            }
            return false;
        }

        public T RemoveAndTake(T e)
        {
            for (var i = 0; i < MAX; i++)
            {
                if (e.Equals(array[i]))
                {
                    var ret = array[i];
                    array[i] = default(T);
                    count--;
                    return ret;
                }
            }
            return default(T);
        }

        public void Clear()
        {
            for (var i = 0; i < MAX; i++)
            {
                array[i] = default(T);
            }
            count = 0;
        }

        public IEnumerator<T> GetEnumerator()
        {
            for (var i = 0; i < MAX; i++)
            {
                yield return array[i];
            }
        }

        IEnumerator IEnumerable.GetEnumerator() => array.GetEnumerator();
    }
}