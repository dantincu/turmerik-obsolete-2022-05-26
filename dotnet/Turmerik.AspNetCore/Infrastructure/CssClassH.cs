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

        public const string ERROR = "error";
        public const string HIDDEN = "hidden";
        public const string READONLY = "readonly";
        public const string EDITABLE = "editable";
        public const string CURRENT = "current";
        public const string SELECTED = "selected";
        public const string LOADING = "loading";
        public const string DATA = "data";
        public const string DETAILS = "details";
        public const string DEFAULT = "default";

        public const string LINK = "link";
        public const string BLOCK = "block";
        public const string TOP = "top";
        public const string BAR = "bar";
        public const string BRAND = "brand";
        public const string NAME = "name";
        public const string EXT = "ext";
        public const string ITEM = "item";
        public const string ITEMS = "items";

        public const string LARGE = "large";
        public const string MEDIUM = "medium";
        public const string SMALL = "small";
        public const string BTN = "btn";
        public const string ROW = "row";
        public const string COL = "col";
        public const string CELL = "cell";
        public const string TABLE = "table";
        public const string CONTAINER = "container";
        public const string SPACER = "spacer";
        public const string SHRINK = "shrink";
        public const string ENLARGE = "enlarge";
        public const string TAB = "tab";
        public const string VIEW = "view";
        public const string HEAD = "head";
        public const string HEADER = "header";
        public const string PAGE = "page";
        public const string GRID = "grid";

        public const string NAVBAR = "navbar";
        public const string GREET = "greet";
        public const string DATE = "date";
        public const string TIME = "time";

        public const string DRIVE = "drive";
        public const string FOLDER = "folder";
        public const string FOLDERS = "folders";
        public const string ADDRESS = "address";

        public static readonly string Hidden = Class(HIDDEN);
        public static readonly string Current = Class(CURRENT);
        public static readonly string Selected = Class(SELECTED);
        public static readonly string Name = Class(NAME);
        public static readonly string Loading = Class(LOADING);
        public static readonly string ErrorBlock = Class(ERROR, BLOCK);
        public static readonly string ErrorBlockSmall = Class(ERROR, BLOCK, SMALL);
        public static readonly string ErrorBlockMedium = Class(ERROR, BLOCK, MEDIUM);
        public static readonly string ErrorBlockLarge = Class(ERROR, BLOCK, LARGE);
        public static readonly string ErrorDetails = Class(ERROR, DETAILS);

        public static readonly string DefaultLink = Class(DEFAULT, LINK);
        public static readonly string Btn = Class(BTN);
        public static readonly string Table = Class(TABLE);
        public static readonly string DataGrid = Class(DATA, GRID);
        public static readonly string DataGridCell = Class(DATA, GRID, CELL);
        public static readonly string DataGridRow = Class(DATA, GRID, ROW);
        public static readonly string DataGridHeaderRow = Class(DATA, GRID, HEADER, ROW);
        public static readonly string TopRow = Class(TOP, ROW);
        public static readonly string ColName = Class(COL, NAME);
        public static readonly string ColExt = Class(COL, EXT);
        public static readonly string ColDateTime = Class(COL, DATE, TIME);
        public static readonly string Container = Class(CONTAINER);
        public static readonly string Spacer = Class(SPACER);

        public static readonly string Large = Class(LARGE);
        public static readonly string Small = Class(SMALL);
        public static readonly string ShrinkEnlarge = Class(SHRINK, ENLARGE);

        public static readonly string Navbar = Class(NAVBAR);
        public static readonly string NavbarBrand = Class(NAVBAR, BRAND);
        public static readonly string Greet = Class(GREET);

        public static readonly string TabView = Class(TAB, VIEW);
        public static readonly string TabViewHeader = Class(TAB, VIEW, HEADER);
        public static readonly string TabPage = Class(TAB, PAGE);
        public static readonly string TabPageHead = Class(TAB, PAGE, HEAD);

        public static readonly string DriveTabViewPage = Class(DRIVE, TAB, VIEW, PAGE);
        public static readonly string DriveFoldersGrid = Class(DRIVE, FOLDERS, GRID);
        public static readonly string DriveItemsGrid = Class(DRIVE, ITEMS, GRID);

        public static readonly string Address = Class(ADDRESS);
        public static readonly string AddressBar = Class(ADDRESS, BAR);
        public static readonly string AddressBarContainer = Class(ADDRESS, BAR, CONTAINER);
        public static readonly string AddressBarReadonly = Class(ADDRESS, BAR, READONLY);

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
