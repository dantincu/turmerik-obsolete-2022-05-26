using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

namespace Turmerik.Core.Helpers
{
    public class BytesEqCompr : IEqualityComparer<byte[]>
    {
        /// <summary>
        /// All prime numbers smaller than 100 (24 in total), taken from
        /// Taken from https://thirdspacelearning.com/blog/what-is-a-prime-number/
        /// </summary>
        private static readonly ReadOnlyCollection<int> hashCodeFactors = new int[]
        {
            2, 3, 5, 7, 11, 13, 17, 19, 23, 29, 31, 37, 41, 43, 47, 53, 59, 61, 67, 71, 73, 79, 83, 89, 97
        }.RdnlC();

        private static readonly int hasCodeFactorsCount = hashCodeFactors.Count;

        public bool Equals(byte[] x, byte[] y)
        {
            bool retVal = x.Length == y.Length;
            retVal = retVal && x.All((val, idx) => val == y[idx]);

            return retVal;
        }

        public int GetHashCode(byte[] obj)
        {
            /* int hashCode = obj.Select(
                (val, idx) => val * hashCodeFactors[idx % hasCodeFactorsCount]).ToArray().Sum();*/

            /*
             * Just comparing bytes one by one is surely faster than making more computing operations just to calculate the hashCode
             */

            int hashCode = 0;
            return hashCode;
        }
    }
}
