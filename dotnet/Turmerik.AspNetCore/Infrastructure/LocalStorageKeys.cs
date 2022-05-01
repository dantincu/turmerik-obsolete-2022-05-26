namespace Turmerik.AspNetCore.Infrastructure
{
    public static class LocalStorageKeys
    {
        public const string TRMRK = "trmrk";
        public const string SESSION = "trmrk";

        public const string LOCAL = "local";
        public const string USER = "user";

        public static readonly string LocalSession = GetKey(LOCAL, SESSION);
        public static readonly string UserSession = GetKey(USER, SESSION);

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
