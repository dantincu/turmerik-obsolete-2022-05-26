using System.Collections.Concurrent;
using Turmerik.AspNetCore.Infrastructure;
using Turmerik.AspNetCore.Services.LocalSessionStorage;
using Turmerik.Core.Cloneable;

namespace Turmerik.AspNetCore.LocalSession
{
    public interface ILocalSessionsDictnr
    {
        Task<ILocalSessionData> TryAddOrUpdateLocalSessionAsync(
            ILocalStorageSvc localStorage,
            ISessionStorageSvc sessionStorage,
            Guid localSessionGuid);

        Task<ILocalSessionData> TryRemoveLocalSessionAsync(
            ILocalStorageSvc localStorage,
            ISessionStorageSvc sessionStorage,
            Guid localSessionGuid);
    }

    public class LocalSessionsDictnr : ILocalSessionsDictnr
    {
        private readonly ICloneableMapper mapper;

        private readonly ConcurrentDictionary<Guid, ILocalSessionData> dictnr;

        public LocalSessionsDictnr(ICloneableMapper mapper)
        {
            this.mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            dictnr = new ConcurrentDictionary<Guid, ILocalSessionData>();
        }

        public async Task<ILocalSessionData> TryAddOrUpdateLocalSessionAsync(
            ILocalStorageSvc localStorage,
            ISessionStorageSvc sessionStorage,
            Guid localSessionGuid)
        {
            var localSessionData = dictnr.GetOrAdd(
                localSessionGuid,
                key => new LocalSessionDataImmtbl(
                    mapper,
                    new LocalSessionDataMtbl
                    {
                        LocalSessionGuid = localSessionGuid
                    }));

            await sessionStorage.SetItemAsync(
                LocalStorageKeys.LocalSessionId,
                localSessionGuid);

            await localStorage.SetItemAsync(
                LocalStorageKeys.LocalSessionId,
                localSessionGuid);

            return localSessionData;
        }

        public async Task<ILocalSessionData> TryRemoveLocalSessionAsync(
            ILocalStorageSvc localStorage,
            ISessionStorageSvc sessionStorage,
            Guid localSessionGuid)
        {
            ILocalSessionData localSessionData;
            
            dictnr.TryRemove(
                localSessionGuid,
                out localSessionData);

            await sessionStorage.RemoveItemAsync(LocalStorageKeys.LocalSessionId);
            await localStorage.RemoveItemAsync(LocalStorageKeys.LocalSessionId);

            return localSessionData;
        }
    }
}
