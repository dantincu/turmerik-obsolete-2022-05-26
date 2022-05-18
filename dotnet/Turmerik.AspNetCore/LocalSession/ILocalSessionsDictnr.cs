using System.Collections.Concurrent;
using System.Security.Cryptography;
using Turmerik.AspNetCore.Infrastructure;
using Turmerik.AspNetCore.Services.LocalSessionStorage;
using Turmerik.Core.Cloneable;
using Turmerik.Core.Data;
using Turmerik.Core.Helpers;

namespace Turmerik.AspNetCore.LocalSession
{
    public interface ILocalSessionsDictnr
    {
        Task<LocalSessionData> TryAddOrUpdateLocalSessionAsync(
            ILocalStorageWrapper localStorage,
            ISessionStorageWrapper sessionStorage,
            Guid? localSessionGuid,
            bool getExtended = false);

        Task<LocalSessionData> TryRemoveLocalSessionAsync(
            ILocalStorageWrapper localStorage,
            ISessionStorageWrapper sessionStorage,
            Guid localSessionGuid,
            bool getExtended = false);
    }

    public class LocalSessionsDictnr : ILocalSessionsDictnr
    {
        private readonly ICloneableMapper mapper;

        private readonly ConcurrentDictionary<Guid, ExtendedLocalSessionData> dictnr;

        public LocalSessionsDictnr(ICloneableMapper mapper)
        {
            this.mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            dictnr = new ConcurrentDictionary<Guid, ExtendedLocalSessionData>();
        }

        public async Task<LocalSessionData> TryAddOrUpdateLocalSessionAsync(
            ILocalStorageWrapper localStorage,
            ISessionStorageWrapper sessionStorage,
            Guid? localSessionGuid,
            bool getExtended = false)
        {
            string existsKey;

            if (localSessionGuid.HasValue)
            {
                existsKey = LocalStorageKeys.LocalSessionExistsKey(localSessionGuid.Value);
                var existsTpl = await sessionStorage.TryGetValueAsync<bool>(existsKey);

                if (!existsTpl.Item1 || !existsTpl.Item2)
                {
                    localSessionGuid = Guid.NewGuid();
                }
            }
            else
            {
                localSessionGuid = Guid.NewGuid();
                existsKey = LocalStorageKeys.LocalSessionExistsKey(localSessionGuid.Value);

                await sessionStorage.Service.SetItemAsync(existsKey, true);
            }

            var localSessionData = dictnr.GetOrAdd(
                localSessionGuid.Value,
                key =>
                {
                    var mtbl = new LocalSessionDataMtbl
                    {
                        LocalSessionGuid = localSessionGuid.Value
                    };

                    var immtbl = new LocalSessionDataImmtbl(mapper, mtbl);
                    var retObj = new ExtendedLocalSessionData(immtbl);

                    return retObj;
                });

            await sessionStorage.Service.SetItemAsync(
                LocalStorageKeys.LocalSessionId,
                localSessionGuid);

            var retData = GetLocalSessionData(localSessionData, getExtended);
            return retData;
        }

        public async Task<LocalSessionData> TryRemoveLocalSessionAsync(
            ILocalStorageWrapper localStorage,
            ISessionStorageWrapper sessionStorage,
            Guid localSessionGuid,
            bool getExtended = false)
        {
            ExtendedLocalSessionData localSessionData;
            
            dictnr.TryRemove(
                localSessionGuid,
                out localSessionData);

            await sessionStorage.Service.RemoveItemAsync(LocalStorageKeys.LocalSessionId);

            var retData = GetLocalSessionData(localSessionData, getExtended);
            return retData;
        }

        private LocalSessionData GetLocalSessionData(
            ExtendedLocalSessionData data,
            bool getExtended = false)
        {
            LocalSessionData retData;

            if (getExtended)
            {
                retData = data;
            }
            else if (data != null)
            {
                retData = new LocalSessionData(data.Data);
            }
            else
            {
                retData = null;
            }

            return retData;
        }
    }
}
