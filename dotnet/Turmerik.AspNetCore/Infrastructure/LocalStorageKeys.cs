namespace Turmerik.AspNetCore.Infrastructure
{
    public static class LocalStorageKeys
    {
        public const string TRMRK = "trmrk";

        public const string LOCAL_SESSION = "local-session";
        public const string USER_SESSION = "user-session";

        public static readonly string LocalSession = GetKey(LOCAL_SESSION);
        public static readonly string UserSession = GetKey(USER_SESSION);

        public static string GetLocal(
            string baseKey,
            Guid localSessionGuid)
        {
            string key = $"{baseKey}[{localSessionGuid.ToString("N")}]";
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
