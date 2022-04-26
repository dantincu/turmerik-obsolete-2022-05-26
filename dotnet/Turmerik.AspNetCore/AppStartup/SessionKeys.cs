namespace Turmerik.AspNetCore.AppStartup
{
    public static class SessionKeys
    {
        public const string TRMRK = "trmrk";

        public const string USER_NAME = "user-name";
        public const string USER_SESSION_GUID = "user-session-guid";

        public static readonly string UserName = GetKey(USER_NAME);
        public static readonly string UserSessionGuid = GetKey(USER_SESSION_GUID);

        private static string GetKey(params string[] segments)
        {
            segments = segments.Prepend(TRMRK).ToArray();

            string key = string.Join('-', segments);
            return key;
        }
    }
}
