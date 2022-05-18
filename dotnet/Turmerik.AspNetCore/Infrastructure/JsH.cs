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
        public static readonly string OpenModal;
        public static readonly string CloseModal;
        public static readonly string ShowPopover;
        public static readonly string HidePopover;
        public static readonly string ReadFromClipboardAsync;
        public static readonly string WriteToClipboardAsync;
        public static readonly string ShowUIBlockingOverlay;
        public static readonly string HideUIBlockingOverlay;

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
            OpenModal = nameof(OpenModal).DecapitalizeFirstLetter();
            CloseModal = nameof(CloseModal).DecapitalizeFirstLetter();
            ShowPopover = nameof(ShowPopover).DecapitalizeFirstLetter();
            HidePopover = nameof(HidePopover).DecapitalizeFirstLetter();
            ReadFromClipboardAsync = nameof(ReadFromClipboardAsync).DecapitalizeFirstLetter();
            WriteToClipboardAsync = nameof(WriteToClipboardAsync).DecapitalizeFirstLetter();
            ShowUIBlockingOverlay = nameof(ShowUIBlockingOverlay).DecapitalizeFirstLetter();
            HideUIBlockingOverlay = nameof(HideUIBlockingOverlay).DecapitalizeFirstLetter();
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

            public static readonly string ContainsKey;
            public static readonly string RemoveItem;
            public static readonly string RemoveItems;
            public static readonly string Keys;
            public static readonly string Clear;
            public static readonly string GetItem;
            public static readonly string SetItem;
            public static readonly string GetBigItemChunksCount;
            public static readonly string GetBigItemChunk;
            public static readonly string ClearBigItemChunks;

            static WebStorage()
            {
                ContainsKey = GetMethodName(nameof(ContainsKey).DecapitalizeFirstLetter());
                RemoveItem = GetMethodName(nameof(RemoveItem).DecapitalizeFirstLetter());
                RemoveItems = GetMethodName(nameof(RemoveItems).DecapitalizeFirstLetter());
                Keys = GetMethodName(nameof(Keys).DecapitalizeFirstLetter());
                Clear = GetMethodName(nameof(Clear).DecapitalizeFirstLetter());
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
