﻿namespace Turmerik.AspNetCore.Infrastructure
{
    public static class LocalStorageKeys
    {
        public const string TRMRK = "trmrk";

        public const string USER_SESSION = "user-session";

        public static readonly string UserSession = GetKey(USER_SESSION);

        private static string GetKey(params string[] segments)
        {
            segments = segments.Prepend(TRMRK).ToArray();

            string key = string.Join('-', segments);
            return key;
        }
    }
}
