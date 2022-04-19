using System;
using System.Collections.Generic;
using System.Text;

namespace Turmerik.Core.Helpers
{
    public static class ColctnH
    {
        public static bool All<T>(this IEnumerable<T> nmrbl, Func<T, int, bool> predicate)
        {
            bool retVal = true;
            int idx = 0;

            foreach (T item in nmrbl)
            {
                if (!(retVal = retVal && predicate(item, idx++)))
                {
                    break;
                }
            }

            return retVal;
        }

        public static bool Any<T>(this IEnumerable<T> nmrbl, Func<T, int, bool> predicate)
        {
            bool retVal = false;
            int idx = 0;

            foreach (T item in nmrbl)
            {
                if (retVal = retVal && predicate(item, idx++))
                {
                    break;
                }
            }

            return retVal;
        }
    }
}
