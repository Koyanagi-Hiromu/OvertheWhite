using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.Collections;

namespace PPD
{
    public static class EX_Linq
    {
        public static bool ContainsIndex(this Array array, int value)
        {
            return value.IsWithin(0, array.Length);
        }

        public static bool ContainsIndex<T>(this ICollection list, int value)
        {
            return value.IsWithin(0, list.Count);
        }

        public static bool IsWithin(this int value, int min, int max)
        {
            return min <= value && value < max;
        }

        static System.Random rng = new System.Random();
        /// <summary>
        /// Fisher–Yates shuffle
        /// see https://ja.wikipedia.org/wiki/%E3%83%95%E3%82%A3%E3%83%83%E3%82%B7%E3%83%A3%E3%83%BC%E2%80%93%E3%82%A4%E3%82%A7%E3%83%BC%E3%83%84%E3%81%AE%E3%82%B7%E3%83%A3%E3%83%83%E3%83%95%E3%83%AB
        /// </summary>
        public static void Shuffle_ChangeTo<T>(this IList<T> list)
        {
            int n = list.Count;
            while (n > 1)
            {
                n--;
                int k = rng.Next(n + 1);
                T value = list[k];
                list[k] = list[n];
                list[n] = value;
            }
        }

        public static List<T> Shuffle_Generate<T>(this List<T> list)
        {
            return list.OrderBy(a => Guid.NewGuid()).ToList();
        }

        /// <summary>
        /// 最小値を持つ要素を返します
        /// </summary>
        public static TSource FindMin<TSource, TResult>(this IEnumerable<TSource> self, Func<TSource, TResult> selector)
        {
            return self.FirstOrDefault(c => selector(c).Equals(self.Min(selector)));
        }

        /// <summary>
        /// 最大値を持つ要素を返します
        /// </summary>
        public static TSource FindMax<TSource, TResult>(this IEnumerable<TSource> self, Func<TSource, TResult> selector)
        {
            return self.FirstOrDefault(c => selector(c).Equals(self.Max(selector)));
        }

        public static TResult MaxOrDefault<TSource, TResult>(this IEnumerable<TSource> self, Func<TSource, TResult> selector, TResult defaultValue)
        {
            if (self.Empty())
            {
                return defaultValue;
            }
            else
            {
                return self.Max(selector);
            }
        }

        public static int MaxOrDefault<TSource>(this IEnumerable<TSource> self, Func<TSource, int> selector)
        {
            if (self.Empty())
            {
                return 0;
            }
            else
            {
                return self.Max(selector);
            }
        }

        public static float MaxOrDefault<TSource>(this IEnumerable<TSource> self, Func<TSource, float> selector)
        {
            if (self.Empty())
            {
                return 0;
            }
            else
            {
                return self.Max(selector);
            }
        }
        public static NativeArray<T> ToNativeArray<T>(this IEnumerable<T> ie)
        where T : struct
        {
            var array = ie.ToArray();
            var nativeArray = new NativeArray<T>(array.Length, Allocator.TempJob);
            for (int i = 0; i < array.Length; i++)
            {
                nativeArray[i] = array[i];
            }
            return nativeArray;
        }

        /// <summary>
        /// nullを除外します。
        /// </summary>
        /// <param name="ie">this</param>
        /// <returns>nullが除外されたIEnumerable</returns>
        public static IEnumerable<T> NotNull<T>(this IEnumerable<T> ie)
        {
            return ie.Where(e => e != null);
        }

        public static IEnumerable<T> Concat<T>(this IEnumerable<T> ie, params T[] elements)
        {
            return Enumerable.Concat(ie, elements);
        }

        public static IEnumerable<T> InsertHead<T>(this IEnumerable<T> ie, params T[] elements)
        {
            return Enumerable.Concat(elements, ie);
        }

        public static IEnumerable<T> WhereIf<T>(this IEnumerable<T> ie, bool flg, Func<T, bool> predicate)
        {
            if (flg)
            {
                return ie.Where(predicate);
            }
            else
            {
                return ie;
            }
        }

        /// <summary>
        /// ラムダ式を各要素に実行する
        /// </summary>
        /// <param name="ie">this</param>
        /// <param name="action">ラムダ式</param>
        public static IEnumerable<T> ForEach<T>(this IEnumerable<T> ie, Action<T> action)
        {
            if (action == null) return ie;

            foreach (var element in ie)
            {
                action(element);
            }

            return ie;
        }

        /// <summary>
        /// ラムダ式を各要素に実行する
        /// </summary>
        /// <param name="ie">this</param>
        /// <param name="action">ラムダ式</param>
        public static IEnumerable<T> ForEach<T>(this IEnumerable<T> ie, Action<T, int> action)
        {
            int i = 0;
            foreach (var element in ie)
            {
                action(element, i);
                i++;
            }
            return ie;
        }

        /// <summary>
        /// ラムダ式を各要素に’逆順で’実行する
        /// </summary>
        /// <param name="ie">this</param>
        /// <param name="action">ラムダ式</param>
        public static IEnumerable<T> RForEach<T>(this IEnumerable<T> ie, Action<T> action)
        {
            var count = ie.Count();
            for (int i = count - 1; i >= 0; i--)
            {
                action(ie.ElementAt(i));
            }
            return ie;
        }

        /// <summary>
        /// ラムダ式を各要素に’逆順で’実行する
        /// </summary>
        /// <param name="ie">this</param>
        /// <param name="action">ラムダ式</param>
        public static IEnumerable<T> RForEach<T>(this IEnumerable<T> ie, Action<T, int> action)
        {
            var count = ie.Count();
            for (int i = count - 1; i >= 0; i--)
            {
                action(ie.ElementAt(i), i);
            }
            return ie;
        }

        /// <summary>
        /// まずはRForEachを試しましょう
        /// 
        /// 要素の追加と削除に耐えるForEachです。
        /// 内部ではToArray()を呼んでいるだけです。
        /// </summary>
        /// <param name="ie">this</param>
        /// <param name="action">ラムダ式</param>
        public static IEnumerable<T> SafeEach<T>(this IEnumerable<T> ie, Action<T> action)
        {
            return ie.ToArray().ForEach(action);
        }

        /// <summary>
        /// 要素の追加と削除に耐えるForEachです。
        /// 内部ではToArray()を呼んでいるだけです。
        /// </summary>
        /// <param name="ie">this</param>
        /// <param name="action">ラムダ式</param>
        public static IEnumerable<T> SafeEach<T>(this IEnumerable<T> ie, Action<T, int> action)
        {
            return ie.ToArray().ForEach(action);
        }

        /// <summary>
        /// 要素の追加と削除に耐えるForEachです。
        /// 内部ではToArray()を呼んでいるだけです。
        /// </summary>
        /// <param name="ie">this</param>
        /// <param name="action">ラムダ式</param>
        public static void SafeEach<T>(this List<T> ie, Action<T> action)
        {
            new List<T>(ie).ToArray().ForEach(action);
        }

        public static void Delete<T>(this IEnumerable<T> ie, List<T> list, Func<T, bool> predicate)
        {
            ie
            .Where(predicate)
            .SafeEach(e => list.Remove(e));
        }

        public static void Delete<T>(this List<T> list, Func<T, bool> predicate)
        {
            list
            .Where(predicate)
            .SafeEach(e => list.Remove(e));
        }

        /// <summary>
        /// nullを削除します。
        /// </summary>
        public static void Trim<T>(this List<T> list)
        {
            list
            .Where(e => e == null)
            .SafeEach(e => list.Remove(e));
        }


        /// <summary>
        /// 中身が空か。
        /// Anyの否定です。
        /// predicate（条件設定）を使用する場合は All(predicate)かAny(predicate)を使用してください。
        /// </summary>
        /// <param name="ie">this</param>
        /// <returns>中身が空ならtrue</returns>
        public static bool Empty<T>(this IEnumerable<T> ie)
        {
            return ie.Any() == false;
        }

        public static bool Empty(this string ie)
        {
            return ie.IsNullOrEmpty();
        }

        public static void Clear<T>(this IEnumerable<T> ie, List<T> from)
        {
            ie
            .Reverse()
            .ForEach((e) => from.Remove(e));
        }

        public static T Random<T>(this IEnumerable<T> ie)
        {
            return ie.ElementAtOrDefault(UnityEngine.Random.Range(0, ie.Count()));
        }

        public static T RandomAtExcept<T>(this IEnumerable<T> array, ref int exceptIndex)
        {
            int count = array.Count();
            int id;
            if (exceptIndex.IsWithin(0, count))
            {
                id = UnityEngine.Random.Range(0, count);
            }
            else
            {
                id = UnityEngine.Random.Range(0, count - 1);
                if (id >= exceptIndex)
                {
                    id++;
                }
            }
            exceptIndex = id;
            return array.ElementAt(id);
        }

        public static T RandomAtExcept<T>(this T[] array, ref int exceptIndex)
        {
            int count = array.Length;
            int id;
            if (exceptIndex.IsWithin(0, count))
            {
                id = UnityEngine.Random.Range(0, count - 1);
                if (id >= exceptIndex)
                {
                    id++;
                }
            }
            else
            {
                id = UnityEngine.Random.Range(0, count);
            }
            exceptIndex = id;
            return array.ElementAt(id);
        }

        public static T First<T>(this ICollection<T> array)
        {
            return array.ElementAt(0);
        }

        public static T Last<T>(this ICollection<T> array)
        {
            return array.ElementAt(array.Count() - 1);
        }

        public static T RemoveHead<T>(this ICollection<T> ie)
        {
            var ret = ie.First();
            ie.Remove(ret);
            return ret;
        }

        public static T RemoveTail<T>(this ICollection<T> ie)
        {
            var ret = ie.Last();
            ie.Remove(ret);
            return ret;
        }

        public static void AddTail<T>(this ICollection<T> ie, T e)
        {
            ie.Add(e);
        }

        public static void AddHead<T>(this IList<T> ie, T e)
        {
            ie.Insert(0, e);
        }

        public static T[] array<T>(this int times, T e)
        {
            var ret = new T[times];
            for (int i = 0; i < times; i++)
            {
                ret[i] = e;
            }
            return ret;
        }

        public static T[] array<T>(this int times, Func<int, T> func)
        {
            var ret = new T[times];
            for (int i = 0; i < times; i++)
            {
                ret[i] = func(i);
            }
            return ret;
        }

        public static void times(this int times, Action<int> act, int start)
        {
            for (int i = start; i < times; i++)
            {
                act(i);
            }
        }

        public static void times(this int times, Action act, int start)
        {
            for (int i = start; i < times; i++)
            {
                act();
            }
        }

        public static void times(this int times, Action<int> act)
        {
            for (int i = 0; i < times; i++)
            {
                act(i);
            }
        }

        public static void times(this int times, Action act)
        {
            for (int i = 0; i < times; i++)
            {
                act();
            }
        }

        public static int IndexOf<T>(this T[] array, T value)
        {
            return Array.IndexOf(array, value);
        }

        public static T GetNext<T>(this T[] array, T value, int by = 1)
        {
            if (by > array.Length)
            {
                return default(T);
            }

            var id = array.IndexOf(value);
            if (id == -1)
            {
                return default(T);
            }

            var next_id = id + by;
            if (0 <= next_id && next_id < array.Length)
            {
                return array[next_id];
            }
            else
            {
                return default(T);
            }
        }
    }
}
