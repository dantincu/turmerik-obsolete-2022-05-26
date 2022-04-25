using Turmerik.Core.Helpers;

namespace Turmerik.AspNetCore.UserSession
{
    public delegate Task RegisterLoginEvent(byte[] usernameHash, byte[] authTokenHash);

    public interface ITrmrkUserSessionsManager
    {
        event RegisterLoginEvent OnRegisterLogin;
        Task<UserIdentifier> RegisterLogin(string username, string authToken);
    }

    public readonly struct UserIdentifier
    {
        public readonly string UsernameHash;
        public readonly string AuthTokenHash;

        public UserIdentifier(string usernameHash, string authTokenHash)
        {
            UsernameHash = usernameHash ?? throw new ArgumentNullException(nameof(usernameHash));
            AuthTokenHash = authTokenHash ?? throw new ArgumentNullException(nameof(authTokenHash));
        }
    }

    public class TrmrkUserSessionsManager : ITrmrkUserSessionsManager
    {
        private RegisterLoginEvent registerLoginEventHandler;

        public event RegisterLoginEvent OnRegisterLogin
        {
            add
            {
                registerLoginEventHandler += value;
            }

            remove
            {
                registerLoginEventHandler -= value;
            }
        }

        public async Task<UserIdentifier> RegisterLogin(string username, string authToken)
        {
            byte[] usernameSha1 = EncodeH.EncodeSha1(username);
            byte[] authTokenSha1 = EncodeH.EncodeSha1(authToken);

            if (registerLoginEventHandler != null)
            {
                await registerLoginEventHandler.Invoke(usernameSha1, authTokenSha1);
            }

            return new UserIdentifier(
                username, authToken);
        }
    }
}
