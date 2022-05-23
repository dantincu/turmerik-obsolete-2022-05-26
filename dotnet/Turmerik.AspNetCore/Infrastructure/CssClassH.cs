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
        public const string DARK = "dark";
        public const string OPTIONS = "options";
        public const string SETTINGS = "settings";
        public const string PREFERENCES = "preferences";
        public const string PROFILE = "profile";
        public const string USER = "user";

        public const string ICON = "icon";
        public const string LINK = "link";
        public const string BLOCK = "block";
        public const string TOP = "top";
        public const string BAR = "bar";
        public const string BRAND = "brand";
        public const string NAME = "name";
        public const string LABEL = "label";
        public const string TITLE = "title";
        public const string EXT = "ext";
        public const string ITEM = "item";
        public const string ITEMS = "items";
        public const string KEYBOARD = "keyboard";
        public const string TEXT = "text";

        public const string LARGE = "large";
        public const string MEDIUM = "medium";
        public const string SMALL = "small";
        public const string BTN = "btn";
        public const string ROW = "row";
        public const string COL = "col";
        public const string CELL = "cell";
        public const string TABLE = "table";
        public const string CONTAINER = "container";
        public const string WRAPPER = "wrapper";
        public const string SPACER = "spacer";
        public const string SHRINK = "shrink";
        public const string ENLARGE = "enlarge";
        public const string TAB = "tab";
        public const string VIEW = "view";
        public const string HEAD = "head";
        public const string HEADER = "header";
        public const string PAGE = "page";
        public const string GRID = "grid";
        public const string MODAL = "modal";
        public const string CONTENT = "content";
        public const string TEXTBOX = "textbox";
        public const string SET = "set";
        public const string API = "api";
        public const string BASE = "base";
        public const string URI = "uri";
        public const string URIS = "uris";
        public const string MAP = "map";

        public const string NAVBAR = "navbar";
        public const string GREET = "greet";
        public const string DATE = "date";
        public const string TIME = "time";

        public const string DRIVE = "drive";
        public const string FOLDER = "folder";
        public const string FOLDERS = "folders";
        public const string ADDRESS = "address";

        public const string ROTATE90DEG = "rotate90deg";
        public const string OPAQUE = "opaque";
        public const string UI = "ui";
        public const string BLOCKING = "blocking";
        public const string OVERLAY = "overlay";
        public const string POPOVER = "popover";

        public static readonly string Hidden = Class(HIDDEN);
        public static readonly string Current = Class(CURRENT);
        public static readonly string Selected = Class(SELECTED);
        public static readonly string Name = Class(NAME);
        public static readonly string Title = Class(TITLE);
        public static readonly string Loading = Class(LOADING);
        public static readonly string ErrorBlock = Class(ERROR, BLOCK);
        public static readonly string ErrorBlockSmall = Class(ERROR, BLOCK, SMALL);
        public static readonly string ErrorBlockMedium = Class(ERROR, BLOCK, MEDIUM);
        public static readonly string ErrorBlockLarge = Class(ERROR, BLOCK, LARGE);
        public static readonly string ErrorDetails = Class(ERROR, DETAILS);
        public static readonly string Dark = Class(DARK);
        public static readonly string Options = Class(OPTIONS);
        public static readonly string Preferences = Class(PREFERENCES);
        public static readonly string Settings = Class(SETTINGS);
        public static readonly string UserProfile = Class(USER, PROFILE);

        public static readonly string Icon = Class(ICON);
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
        public static readonly string CellName = Class(CELL, NAME);
        public static readonly string CellLabel = Class(CELL, LABEL);
        public static readonly string Container = Class(CONTAINER);
        public static readonly string Wrapper = Class(WRAPPER);
        public static readonly string Spacer = Class(SPACER);
        public static readonly string ModalContent = Class(MODAL, CONTENT);
        public static readonly string SetApiBaseUriView = Class(SET, API, BASE, URI, VIEW);

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

        public static readonly string GridKeyboardGrid = Class(GRID, KEYBOARD, GRID);

        public static readonly string Address = Class(ADDRESS);
        public static readonly string AddressBar = Class(ADDRESS, BAR);
        public static readonly string AddressBarWrapper = Class(ADDRESS, BAR, WRAPPER);
        public static readonly string AddressBarContainer = Class(ADDRESS, BAR, CONTAINER);
        public static readonly string AddressBarReadonly = Class(ADDRESS, BAR, READONLY);

        public static readonly string Textbox = Class(TEXTBOX);
        public static readonly string TextboxWrapper = Class(TEXTBOX, WRAPPER);
        public static readonly string TextboxReadonly = Class(TEXTBOX, READONLY);

        public static readonly string Rotate90Deg = Class(ROTATE90DEG);
        public static readonly string Opaque = Class(OPAQUE);
        public static readonly string Popover = Class(POPOVER);

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
