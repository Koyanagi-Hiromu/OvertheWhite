using System;

namespace PPD
{
    public static class EX_Enum
    {
        public static void ForEach<T>(Action<T> action)
        {
            foreach (var e in (T[])System.Enum.GetValues(typeof(T)))
            {
                action(e);
            }
        }
    }
}
