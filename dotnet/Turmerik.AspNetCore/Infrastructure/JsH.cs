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
        public const string WINDOW = "window";
        public const string TRMRK = "Trmrk";

        public static readonly string SelectDomEl;
        public static readonly string AddCssClass;
        public static readonly string RemoveCssClass;

        static JsH()
        {
            SelectDomEl = nameof(SelectDomEl).DecapitalizeFirstLetter();
            AddCssClass = nameof(AddCssClass).DecapitalizeFirstLetter();
            RemoveCssClass = nameof(RemoveCssClass).DecapitalizeFirstLetter();
        }

        public static string Get(params string[] segments)
        {
            segments = segments.Prepend(TRMRK).Prepend(WINDOW).ToArray();

            string key = string.Join('.', segments);
            return key;
        }
    }
}
