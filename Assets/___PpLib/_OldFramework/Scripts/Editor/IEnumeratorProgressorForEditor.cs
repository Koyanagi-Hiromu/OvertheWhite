using System.Collections;

namespace SR
{
    public static class IEnumeratorProgressorForEditor
    {
        public static IEnumerator current { private get; set; }
        public static void UnityUpdate()
        {
            if (current != null)
            {
                var hasNext = ProgressEr(current, null);
                if (!hasNext)
                {
                    current = null;
                }
            }
        }

        private static bool ProgressEr(IEnumerator currEr, IEnumerator prevEr)
        {
            var nextEr = currEr.Current as IEnumerator;
            if (nextEr != null)
            {
                var hasNext = ProgressEr(nextEr, currEr);
                if (!hasNext)
                {
                    hasNext = MoveNext(currEr, prevEr);
                }
                return hasNext;
            }
            else
            {
                return MoveNext(currEr, prevEr);
            }
        }

        private static bool MoveNext(IEnumerator currEr, IEnumerator prevEr)
        {
            if (currEr.MoveNext())
            {
                var nextEr = currEr.Current as IEnumerator;
                if (nextEr != null)
                {
                    var hasNext = MoveNext(nextEr, currEr);
                    while (!hasNext)
                    {
                        hasNext = MoveNext(currEr, prevEr);
                    }
                }
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
