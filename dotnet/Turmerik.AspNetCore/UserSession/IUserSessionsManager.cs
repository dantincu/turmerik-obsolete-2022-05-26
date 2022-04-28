using Blazored.LocalStorage;
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

        private readonly Lazy<HttpContext> httpContext;

        public UserSessionsManager(
            IUserSessionsDictnr userSessionsDictnr,
            IHttpContextAccessor httpContextAccessor,
            ILocalStorageService localStorage)
        {
            this.userSessionsDictnr = userSessionsDictnr ?? throw new ArgumentNullException(nameof(userSessionsDictnr));
            this.httpContextAccessor = httpContextAccessor ?? throw new ArgumentNullException(nameof(httpContextAccessor));

            this.localStorage = localStorage ?? throw new ArgumentNullException(nameof(localStorage));
            httpContext = new Lazy<HttpContext>(() => httpContextAccessor.HttpContext);
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
                localStorage);

            return appUserSessionData;
        }
    }
}
