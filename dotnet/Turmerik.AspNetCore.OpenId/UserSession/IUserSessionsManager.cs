using Microsoft.AspNetCore.Http;
using Turmerik.AspNetCore.Services.LocalSessionStorage;
using Turmerik.Core.Data;

namespace Turmerik.AspNetCore.OpenId.UserSession
{
    public interface IUserSessionsManager
    {
        Task<IAppUserSessionData> TryAddOrUpdateUserSessionAsync(
            Guid localSessionGuid);

        Task<IAppUserSessionData> TryRemoveUserSessionAsync();
    }

    public class UserSessionsManager : IUserSessionsManager
    {
        private readonly IUserSessionsDictnr userSessionsDictnr;
        private readonly IHttpContextAccessor httpContextAccessor;
        private readonly ILocalStorageWrapper localStorage;
        private readonly ISessionStorageWrapper sessionStorage;

        public UserSessionsManager(
            IUserSessionsDictnr userSessionsDictnr,
            IHttpContextAccessor httpContextAccessor,
            ILocalStorageWrapper localStorage,
            ISessionStorageWrapper sessionStorage)
        {
            this.userSessionsDictnr = userSessionsDictnr ?? throw new ArgumentNullException(nameof(userSessionsDictnr));
            this.httpContextAccessor = httpContextAccessor ?? throw new ArgumentNullException(nameof(httpContextAccessor));

            this.localStorage = localStorage ?? throw new ArgumentNullException(nameof(localStorage));
            this.sessionStorage = sessionStorage ?? throw new ArgumentNullException(nameof(sessionStorage));
        }

        public async Task<IAppUserSessionData> TryAddOrUpdateUserSessionAsync(
            Guid localSessionGuid)
        {
            var appUserSessionData = await userSessionsDictnr.TryAddOrUpdateUserSessionAsync(
                httpContextAccessor,
                localStorage,
                sessionStorage,
                localSessionGuid);

            return appUserSessionData;
        }

        public async Task<IAppUserSessionData> TryRemoveUserSessionAsync()
        {
            var appUserSessionData = await userSessionsDictnr.TryRemoveUserSessionAsync(
                httpContextAccessor,
                localStorage,
                sessionStorage);

            return appUserSessionData;
        }
    }
}
