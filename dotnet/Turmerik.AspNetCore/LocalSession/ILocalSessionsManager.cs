using Blazored.LocalStorage;
using Blazored.SessionStorage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Turmerik.AspNetCore.LocalSession
{
    public interface ILocalSessionsManager
    {
        Task<ILocalSessionData> TryAddOrUpdateLocalSessionAsync(Guid localSessionGuid);
        Task<ILocalSessionData> TryRemoveLocalSessionAsync(Guid localSessionGuid);
    }

    public class LocalSessionsManager : ILocalSessionsManager
    {
        private readonly ILocalSessionsDictnr localSessionsDictnr;
        private readonly ILocalStorageService localStorage;
        private readonly ISessionStorageService sessionStorage;

        public LocalSessionsManager(
            ILocalSessionsDictnr localSessionsDictnr,
            ILocalStorageService localStorage,
            ISessionStorageService sessionStorage)
        {
            this.localSessionsDictnr = localSessionsDictnr ?? throw new ArgumentNullException(nameof(localSessionsDictnr));
            this.localStorage = localStorage ?? throw new ArgumentNullException(nameof(localStorage));
            this.sessionStorage = sessionStorage ?? throw new ArgumentNullException(nameof(sessionStorage));
        }

        public async Task<ILocalSessionData> TryAddOrUpdateLocalSessionAsync(Guid localSessionGuid)
        {
            var localSessionData = await localSessionsDictnr.TryAddOrUpdateLocalSessionAsync(
                localStorage, sessionStorage, localSessionGuid);

            return localSessionData;
        }

        public async Task<ILocalSessionData> TryRemoveLocalSessionAsync(Guid localSessionGuid)
        {
            var localSessionData = await localSessionsDictnr.TryRemoveLocalSessionAsync(
                localStorage, sessionStorage, localSessionGuid);

            return localSessionData;
        }
    }
}
