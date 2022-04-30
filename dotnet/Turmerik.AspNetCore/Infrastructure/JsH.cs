using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Turmerik.Core.Helpers;

namespace Turmerik.AspNetCore.Infrastructure
{
    public static class JsH
    {
        public const string TRMRK = "Trmrk";

        public static readonly string SelectSelector;

        static JsH()
        {
            SelectSelector = nameof(SelectSelector).DecapitalizeFirstLetter();
        }

        public static string Get(params string[] segments)
        {
            segments = segments.Prepend(TRMRK).ToArray();

            string key = string.Join('.', segments);
            return key;
        }
    }
}
