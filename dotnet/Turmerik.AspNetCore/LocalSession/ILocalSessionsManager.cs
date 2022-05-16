using Turmerik.AspNetCore.Services.LocalSessionStorage;

namespace Turmerik.AspNetCore.LocalSession
{
    public interface ILocalSessionsManager
    {
        Task<ILocalSessionData> TryAddOrUpdateLocalSessionAsync(Guid? localSessionGuid);
        Task<ILocalSessionData> TryRemoveLocalSessionAsync(Guid localSessionGuid);
    }

    public class LocalSessionsManager : ILocalSessionsManager
    {
        private readonly ILocalSessionsDictnr localSessionsDictnr;
        private readonly ILocalStorageSvc localStorage;
        private readonly ISessionStorageSvc sessionStorage;

        public LocalSessionsManager(
            ILocalSessionsDictnr localSessionsDictnr,
            ILocalStorageSvc localStorage,
            ISessionStorageSvc sessionStorage)
        {
            this.localSessionsDictnr = localSessionsDictnr ?? throw new ArgumentNullException(nameof(localSessionsDictnr));
            this.localStorage = localStorage ?? throw new ArgumentNullException(nameof(localStorage));
            this.sessionStorage = sessionStorage ?? throw new ArgumentNullException(nameof(sessionStorage));
        }

        public async Task<ILocalSessionData> TryAddOrUpdateLocalSessionAsync(Guid? localSessionGuid)
        {
            localSessionGuid = localSessionGuid ?? Guid.NewGuid();

            var localSessionData = await localSessionsDictnr.TryAddOrUpdateLocalSessionAsync(
                localStorage, sessionStorage, localSessionGuid.Value);

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
