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
        public static readonly string GetDomElValue;
        public static readonly string GetDomElInnerText;
        public static readonly string GetDomElInnerHTML;
        public static readonly string GetDomElOuterHTML;
        public static readonly string TextBoxWrapperSetEditable;
        public static readonly string InitDateTimeUserFriendlyLabels;

        static JsH()
        {
            SelectDomEl = nameof(SelectDomEl).DecapitalizeFirstLetter();
            AddCssClass = nameof(AddCssClass).DecapitalizeFirstLetter();
            RemoveCssClass = nameof(RemoveCssClass).DecapitalizeFirstLetter();
            GetDomElValue = nameof(GetDomElValue).DecapitalizeFirstLetter();
            GetDomElInnerText = nameof(GetDomElInnerText).DecapitalizeFirstLetter();
            GetDomElInnerHTML = nameof(GetDomElInnerHTML).DecapitalizeFirstLetter();
            GetDomElOuterHTML = nameof(GetDomElOuterHTML).DecapitalizeFirstLetter();
            TextBoxWrapperSetEditable = nameof(TextBoxWrapperSetEditable).DecapitalizeFirstLetter();
            InitDateTimeUserFriendlyLabels = nameof(InitDateTimeUserFriendlyLabels).DecapitalizeFirstLetter();
        }

        public static string Get(params string[] segments)
        {
            segments = segments.Prepend(TRMRK).Prepend(WINDOW).ToArray();

            string key = string.Join('.', segments);
            return key;
        }

        public static class WebStorage
        {
            public static readonly string BasePrefix = typeof(WebStorage).Name.DecapitalizeFirstLetter();

            public static readonly string GetItem;
            public static readonly string SetItem;
            public static readonly string GetBigItemChunksCount;
            public static readonly string GetBigItemChunk;
            public static readonly string ClearBigItemChunks;

            static WebStorage()
            {
                GetItem = GetMethodName(nameof(GetItem).DecapitalizeFirstLetter());
                SetItem = GetMethodName(nameof(SetItem).DecapitalizeFirstLetter());
                GetBigItemChunksCount = GetMethodName(nameof(GetBigItemChunksCount).DecapitalizeFirstLetter());
                GetBigItemChunk = GetMethodName(nameof(GetBigItemChunk).DecapitalizeFirstLetter());
                ClearBigItemChunks = GetMethodName(nameof(ClearBigItemChunks).DecapitalizeFirstLetter());
            }

            private static string GetMethodName(string methodName)
            {
                string jsMethodName = string.Join(".", BasePrefix, methodName);
                return jsMethodName;
            }
        }
    }
}
