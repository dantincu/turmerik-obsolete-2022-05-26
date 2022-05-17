using Turmerik.AspNetCore.Services.LocalSessionStorage;
using Turmerik.Core.Data;

namespace Turmerik.AspNetCore.LocalSession
{
    public interface ILocalSessionsManager
    {
        Task<LocalSessionData> TryAddOrUpdateLocalSessionAsync(Guid? localSessionGuid);

        Task<LocalSessionData> TryRemoveLocalSessionAsync(Guid localSessionGuid);
    }

    public class LocalSessionsManager : ILocalSessionsManager
    {
        private readonly ILocalSessionsDictnr localSessionsDictnr;
        private readonly ILocalStorageWrapper localStorage;
        private readonly ISessionStorageWrapper sessionStorage;

        public LocalSessionsManager(
            ILocalSessionsDictnr localSessionsDictnr,
            ILocalStorageWrapper localStorage,
            ISessionStorageWrapper sessionStorage)
        {
            this.localSessionsDictnr = localSessionsDictnr ?? throw new ArgumentNullException(nameof(localSessionsDictnr));
            this.localStorage = localStorage ?? throw new ArgumentNullException(nameof(localStorage));
            this.sessionStorage = sessionStorage ?? throw new ArgumentNullException(nameof(sessionStorage));
        }

        public async Task<LocalSessionData> TryAddOrUpdateLocalSessionAsync(Guid? localSessionGuid)
        {
            var localSessionData = await localSessionsDictnr.TryAddOrUpdateLocalSessionAsync(
                localStorage, sessionStorage, localSessionGuid);

            return localSessionData;
        }

        public async Task<LocalSessionData> TryRemoveLocalSessionAsync(Guid localSessionGuid)
        {
            var localSessionData = await localSessionsDictnr.TryRemoveLocalSessionAsync(
                localStorage, sessionStorage, localSessionGuid);

            return localSessionData;
        }
    }
}
