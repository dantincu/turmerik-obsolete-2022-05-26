using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace Turmerik.Core.Helpers
{
    public static class EncodeH
    {
        public static byte[] EncodeSha1(string input)
        {
            byte[] hash;

            using (SHA1Managed sha1 = new SHA1Managed())
            {
                hash = sha1.ComputeHash(Encoding.UTF8.GetBytes(input));
            }

            return hash;
        }
    }
}
