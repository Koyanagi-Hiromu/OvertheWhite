using System;
using System.Linq;

namespace SR
{
    public static class FuncExtention
    {
        public static bool All(this Func<bool> self)
        {
            return self
                .GetInvocationList()
                .All(c => (bool)c.DynamicInvoke());
        }

        public static Func<bool> Compress(this Func<bool> self)
        {
            if (self.ContainsOne())
            {
                return self;
            }
            else
            {
                return self.All;
            }
        }

        public static bool ContainsOne(this Func<bool> self)
        {
            return self.GetInvocationList().Length == 1;
        }
    }
}
