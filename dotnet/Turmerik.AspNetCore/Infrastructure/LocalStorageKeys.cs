namespace Turmerik.AspNetCore.Infrastructure
{
    public static class LocalStorageKeys
    {
        public const string TRMRK = "trmrk";
        public const string SESSION = "trmrk";
        public const string ID = "id";

        public const string LOCAL = "local";
        public const string USER = "user";

        public const string SELECTED = "selected";
        public const string CURRENT = "current";
        public const string CURRENTLY = "currently";
        public const string ROOT = "root";
        public const string DRIVE = "drive";
        public const string ITEM = "item";
        public const string ITEMS = "items";
        public const string FOLDER = "folder";
        public const string FOLDERS = "folders";
        public const string OPEN = "open";
        public const string TAB = "tab";
        public const string TABS = "tabs";
        public const string PAGE = "page";
        public const string HEADS = "heads";
        public const string ADDRESS = "address";
        public const string HISTORY = "history";
        public const string STACK = "stack";
        public const string VIEW = "view";
        public const string STATE = "state";

        public static readonly string LocalSessionId = GetKey(LOCAL, SESSION, ID);
        public static readonly string UserSessionId = GetKey(USER, SESSION, ID);

        public static string AddressHistoryStackKey(Guid localSessionGuid, Guid tabPageGuid)
        {
            string key = GetLocalStorageKey(localSessionGuid, tabPageGuid, ADDRESS, HISTORY, STACK);
            return key;
        }

        public static string RootDriveFolderKey(Guid localSessionGuid)
        {
            string key = GetLocalStorageKey(localSessionGuid, ROOT, DRIVE, FOLDER);
            return key;
        }

        public static string DriveFolderKey(Guid localSessionGuid, string driveItemId)
        {
            string key = GetLocalStorageKey(localSessionGuid, DRIVE, FOLDER);
            key = $"{key}|{driveItemId}|";

            return key;
        }

        public static string DriveItemsViewStateKey(Guid localSessionGuid)
        {
            string key = GetLocalStorageKey(localSessionGuid, DRIVE, ITEMS, VIEW, STATE);
            return key;
        }

        public static string GetLocalStorageKey(
            Guid localSessionGuid,
            params string[] segments)
        {
            string baseKey = $"{TRMRK}[{localSessionGuid.ToString("N")}]";
            segments = segments.Prepend(TRMRK).ToArray();

            string key = string.Join('-', segments);
            key = string.Concat(baseKey, key);

            return key;
        }

        public static string GetLocalStorageKey(
           Guid localSessionGuid,
           Guid tabPageGuid,
           params string[] segments)
        {
            string baseKey = GetKey(localSessionGuid, tabPageGuid);

            string key = string.Join('-', segments);
            key = string.Concat(baseKey, key);

            return key;
        }

        private static string GetKey(params Guid[] guids)
        {
            string[] segments = guids.Select(x => x.ToString("N")).ToArray();
            string key = GetKey(segments);

            return key;
        }

        private static string GetKey(params string[] segments)
        {
            segments = segments.Prepend(TRMRK).ToArray();

            string key = string.Join('-', segments);
            return key;
        }
    }
}
