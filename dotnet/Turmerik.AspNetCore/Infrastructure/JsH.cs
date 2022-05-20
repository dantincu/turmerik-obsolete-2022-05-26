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
        public static readonly string ClearTimeout;

        static JsH()
        {
            ClearTimeout = nameof(ClearTimeout).DecapitalizeFirstLetter();
        }

        public static string Get(params string[] segments)
        {
            string key = string.Join('.', segments);
            return key;
        }
    }
}
