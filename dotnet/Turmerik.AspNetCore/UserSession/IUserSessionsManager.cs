using Blazored.LocalStorage;
using Blazored.SessionStorage;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Turmerik.AspNetCore.UserSession
{
    public interface IUserSessionsManager
    {
        Task<IAppUserSessionData> TryAddOrUpdateUserSessionAsync();
        Task<IAppUserSessionData> TryRemoveUserSessionAsync();
    }

    public class UserSessionsManager : IUserSessionsManager
    {
        private readonly IUserSessionsDictnr userSessionsDictnr;
        private readonly IHttpContextAccessor httpContextAccessor;
        private readonly ILocalStorageService localStorage;
        private readonly ISessionStorageService sessionStorage;

        public UserSessionsManager(
            IUserSessionsDictnr userSessionsDictnr,
            IHttpContextAccessor httpContextAccessor,
            ILocalStorageService localStorage,
            ISessionStorageService sessionStorage)
        {
            this.userSessionsDictnr = userSessionsDictnr ?? throw new ArgumentNullException(nameof(userSessionsDictnr));
            this.httpContextAccessor = httpContextAccessor ?? throw new ArgumentNullException(nameof(httpContextAccessor));

            this.localStorage = localStorage ?? throw new ArgumentNullException(nameof(localStorage));
            this.sessionStorage = sessionStorage ?? throw new ArgumentNullException(nameof(sessionStorage));
        }

        public async Task<IAppUserSessionData> TryAddOrUpdateUserSessionAsync()
        {
            var appUserSessionData = await userSessionsDictnr.TryAddOrUpdateUserSessionAsync(
                httpContextAccessor,
                localStorage);

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
