using System.Collections.Concurrent;
using System.Security.Cryptography;
using Turmerik.AspNetCore.Infrastructure;
using Turmerik.AspNetCore.Services.LocalSessionStorage;
using Turmerik.Core.Cloneable;
using Turmerik.Core.Helpers;

namespace Turmerik.AspNetCore.LocalSession
{
    public interface ILocalSessionsDictnr
    {
        Task<LocalSessionData> TryAddOrUpdateLocalSessionAsync(
            ILocalStorageSvc localStorage,
            ISessionStorageSvc sessionStorage,
            Guid localSessionGuid,
            bool getExtended = false);

        Task<LocalSessionData> TryRemoveLocalSessionAsync(
            ILocalStorageSvc localStorage,
            ISessionStorageSvc sessionStorage,
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
            ILocalStorageSvc localStorage,
            ISessionStorageSvc sessionStorage,
            Guid localSessionGuid,
            bool getExtended = false)
        {
            var localSessionData = dictnr.GetOrAdd(
                localSessionGuid,
                key =>
                {
                    //Generate a public/private key pair.  
                    RSA rsa = RSA.Create();
                    //Save the public key information to an RSAParameters structure.  
                    var publicKey = rsa.ExportRSAPublicKey().RdnlC();
                    var privateKey = rsa.ExportRSAPrivateKey().RdnlC();

                    var mtbl = new LocalSessionDataMtbl
                    {
                        LocalSessionGuid = localSessionGuid
                    };

                    var immtbl = new LocalSessionDataImmtbl(mapper, mtbl);
                    var retObj = new ExtendedLocalSessionData(immtbl, publicKey, privateKey);

                    return retObj;
                });

            await sessionStorage.SetItemAsync(
                LocalStorageKeys.LocalSessionId,
                localSessionGuid);

            await localStorage.SetItemAsync(
                LocalStorageKeys.LocalSessionId,
                localSessionGuid);

            var retData = GetLocalSessionData(localSessionData);
            return localSessionData;
        }

        public async Task<LocalSessionData> TryRemoveLocalSessionAsync(
            ILocalStorageSvc localStorage,
            ISessionStorageSvc sessionStorage,
            Guid localSessionGuid,
            bool getExtended = false)
        {
            ExtendedLocalSessionData localSessionData;
            
            dictnr.TryRemove(
                localSessionGuid,
                out localSessionData);

            await sessionStorage.RemoveItemAsync(LocalStorageKeys.LocalSessionId);
            await localStorage.RemoveItemAsync(LocalStorageKeys.LocalSessionId);

            var retData = GetLocalSessionData(localSessionData);
            return localSessionData;
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
                retData = new LocalSessionData(data.Data, data.PublicKey);
            }
            else
            {
                retData = null;
            }

            return retData;
        }
    }
}
