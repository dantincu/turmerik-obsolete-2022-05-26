using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Turmerik.AspNetCore.Infrastructure
{
    public static class CssClassH
    {
        public const string TRMRK = "trmrk";

        public const string HIDDEN = "hidden";
        public const string READONLY = "readonly";
        public const string EDITABLE = "editable";

        public const string TOP = "top";
        public const string BAR = "bar";
        public const string BRAND = "brand";

        public const string LARGE = "large";
        public const string SMALL = "small";
        public const string BTN = "btn";
        public const string ROW = "row";
        public const string TABLE = "table";
        public const string CONTAINER = "container";
        public const string SHRINK = "shrink";
        public const string ENLARGE = "enlarge";

        public const string NAVBAR = "navbar";
        public const string GREET = "greet";

        public const string ADDRESS = "address";

        public static readonly string Hidden = Class(HIDDEN);

        public static readonly string Btn = Class(BTN);
        public static readonly string Table = Class(TABLE);
        public static readonly string TopRow = Class(TOP, ROW);

        public static readonly string Large = Class(LARGE);
        public static readonly string Small = Class(SMALL);
        public static readonly string ShrinkEnlarge = Class(SHRINK, ENLARGE);

        public static readonly string Navbar = Class(NAVBAR);
        public static readonly string NavbarBrand = Class(Navbar, BRAND);
        public static readonly string Greet = Class(GREET);

        public static readonly string Address = Class(ADDRESS);
        public static readonly string AddressBar = Class(false, Address, BAR);
        public static readonly string AddressBarContainer = Class(false, AddressBar, CONTAINER);
        public static readonly string AddressBarReadonly = Class(false, AddressBar, READONLY);

        public static string CssClsSel(this string cssClass)
        {
            string selector = $".{cssClass}";
            return selector;
        }

        public static string Class(params string[] segments)
        {
            string cssClass = Class(true, segments);
            return cssClass;
        }

        public static string Class(bool prependBasePrefix, params string[] segments)
        {
            if (prependBasePrefix)
            {
                segments = segments.Prepend(TRMRK).ToArray();
            }
            
            string cssClass = string.Join('-', segments);
            return cssClass;
        }

        public static string ClassSel(params string[] segments)
        {
            string cssClass = Class(segments);
            string selector = cssClass.CssClsSel();

            return selector;
        }

        public static string ClassesSel(params string[] classes)
        {
            string[] selectorsArr = classes.Select(cls => cls.CssClsSel()).ToArray();
            string selector = string.Concat(selectorsArr);

            return selector;
        }
    }
}
