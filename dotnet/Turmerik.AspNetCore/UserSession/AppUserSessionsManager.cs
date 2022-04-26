using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Blazored.LocalStorage;
using Turmerik.Core.Cloneable;
using Turmerik.Core.Helpers;

namespace Turmerik.AspNetCore.UserSession
{
    public interface IAppUserSessionsManagerCore
    {
        Task AssureUserSessionRegisteredAsync(IAppUserSession appUserSession);
        Task RemoveUserSession(IAppUserSession appUserSession);
    }

    public interface IAppUserSessionsManager : IAppUserSessionsManagerCore
    {
    }

    public class AppUserSessionsManager : IAppUserSessionsManager, IDisposable
    {
        private readonly ICloneableMapper mapper;

        public AppUserSessionsManager(ICloneableMapper mapper)
        {
            this.mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public void Dispose()
        {
        }

        public async Task AssureUserSessionRegisteredAsync(IAppUserSession appUserSession)
        {
            var immtbl = new AppUserSessionImmtbl(
                mapper, appUserSession);
        }

        public async Task RemoveUserSession(IAppUserSession appUserSession)
        {

        }
    }
}
