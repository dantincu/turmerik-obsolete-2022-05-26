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
        public const string TABS = "tabs";
        public const string ADDRESS = "address";
        public const string HISTORY = "history";
        public const string STACK = "stack";

        public static readonly string LocalSessionId = GetKey(LOCAL, SESSION, ID);
        public static readonly string UserSessionId = GetKey(USER, SESSION, ID);

        public static string AddressHistoryStackKey(Guid localSessionGuid)
        {
            string key = GetLocalStorageKey(localSessionGuid, ADDRESS, HISTORY, STACK);
            return key;
        }

        public static string RootDriveFolderKey(Guid localSessionGuid)
        {
            string key = GetLocalStorageKey(localSessionGuid, ROOT, DRIVE, FOLDER);
            return key;
        }

        public static string DriveFoldersKey(Guid localSessionGuid, string pathOrId)
        {
            string key = GetLocalStorageKey(localSessionGuid, DRIVE, FOLDER);
            key = $"{key}|{pathOrId}|";

            return key;
        }

        public static string CurrentDriveFoldersKey(Guid localSessionGuid)
        {
            string key = GetLocalStorageKey(localSessionGuid, CURRENT, DRIVE, FOLDERS);
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

        private static string GetKey(params string[] segments)
        {
            segments = segments.Prepend(TRMRK).ToArray();

            string key = string.Join('-', segments);
            return key;
        }
    }
}
