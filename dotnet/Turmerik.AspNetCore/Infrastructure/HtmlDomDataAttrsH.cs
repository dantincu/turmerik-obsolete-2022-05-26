using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Turmerik.AspNetCore.Infrastructure
{
    public static class HtmlDomDataAttrsH
    {
        public const string DATA = "data";
        public const string TRMRK = "trmrk";

        public const string REF = "ref";
        public const string COL = "col";
        public const string DATE = "date";
        public const string TIME = "time";

        public static readonly string RefDateTime = Get(REF, DATE, TIME);
        public static readonly string DateTime = Get(DATE, TIME);
        public static readonly string DateTimeCol = Get(DATE, TIME);

        public static string Get(params string[] segments)
        {
            segments = segments.Prepend(TRMRK).Prepend(DATA).ToArray();

            string key = string.Join('-', segments);
            return key;
        }
    }
}
