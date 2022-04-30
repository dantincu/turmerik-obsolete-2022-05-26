using Blazored.LocalStorage;
using Blazored.SessionStorage;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Turmerik.AspNetCore.Infrastructure;
using Turmerik.Core.Cloneable;

namespace Turmerik.AspNetCore.LocalSession
{
    public interface ILocalSessionsDictnr
    {
        Task<ILocalSessionData> TryAddOrUpdateLocalSessionAsync(
            ILocalStorageService localStorage,
            ISessionStorageService sessionStorage,
            Guid localSessionGuid);

        Task<ILocalSessionData> TryRemoveLocalSessionAsync(
            ILocalStorageService localStorage,
            ISessionStorageService sessionStorage,
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
            ILocalStorageService localStorage,
            ISessionStorageService sessionStorage,
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
                LocalStorageKeys.LocalSession,
                localSessionGuid);

            return localSessionData;
        }

        public async Task<ILocalSessionData> TryRemoveLocalSessionAsync(
            ILocalStorageService localStorage,
            ISessionStorageService sessionStorage,
            Guid localSessionGuid)
        {
            ILocalSessionData localSessionData;
            
            dictnr.TryRemove(
                localSessionGuid,
                out localSessionData);

            return localSessionData;
        }
    }
}
