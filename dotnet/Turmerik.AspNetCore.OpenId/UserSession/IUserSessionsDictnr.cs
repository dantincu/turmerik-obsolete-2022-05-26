using Microsoft.AspNetCore.Http;
using System.Collections.Concurrent;
using Turmerik.AspNetCore.AppStartup;
using Turmerik.AspNetCore.Infrastructure;
using Turmerik.AspNetCore.Services.LocalSessionStorage;
using Turmerik.Core.Cloneable;
using Turmerik.Core.Cloneable.Nested;
using Turmerik.Core.Components;
using Turmerik.Core.Helpers;

namespace Turmerik.AspNetCore.OpenId.UserSession
{
    public interface IUserSessionsDictnr
    {
        Task<IAppUserSessionData> TryAddOrUpdateUserSessionAsync(
            IHttpContextAccessor httpContextAccessor,
            ILocalStorageSvc localStorage,
            ISessionStorageSvc sessionStorage,
            Guid localSessionGuid);

        Task<IAppUserSessionData> TryRemoveUserSessionAsync(
            IHttpContextAccessor httpContextAccessor,
            ILocalStorageSvc localStorage,
            ISessionStorageSvc sessionStorage);
    }

    public class UserSessionsDictnr : IUserSessionsDictnr
    {
        private readonly ICloneableMapper mapper;

        private readonly ConcurrentDictionary<IReadOnlyCollection<byte>, IAppUserData> usersDataDictnr;
        private readonly ConcurrentDictionary<IReadOnlyCollection<byte>, ConcurrentDictionary<Guid, IAppUserSessionData>> userSessionsDictnr;

        public UserSessionsDictnr(ICloneableMapper mapper)
        {
            this.mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            usersDataDictnr = new ConcurrentDictionary<IReadOnlyCollection<byte>, IAppUserData>();

            userSessionsDictnr = new ConcurrentDictionary<IReadOnlyCollection<byte>, ConcurrentDictionary<Guid, IAppUserSessionData>>(
                new BytesRdnlClctnEqCompr());
        }

        public async Task<IAppUserSessionData> TryAddOrUpdateUserSessionAsync(
            IHttpContextAccessor httpContextAccessor,
            ILocalStorageSvc localStorage,
            ISessionStorageSvc sessionStorage,
            Guid localSessionGuid)
        {
            var sessionProps = GetSessionProps(httpContextAccessor);
            IAppUserSessionData immtbl = null;

            if (sessionProps != null)
            {
                var utcNow = DateTime.UtcNow;
                var usernameHash = sessionProps.Item2;

                var usernameHashBytes = sessionProps.Item3.RdnlC();
                var usernameHashBytesList = sessionProps.Item3.ToList();

                var userSessionGuid = sessionProps.Item1;

                usersDataDictnr.GetOrAdd(
                    usernameHashBytes,
                    key => SetAppUserDataProps(
                        new AppUserDataMtbl(),
                        usernameHash,
                        usernameHashBytesList));

                userSessionsDictnr.AddOrUpdateValue(
                    usernameHashBytes,
                    key => new ConcurrentDictionary<Guid, IAppUserSessionData>(),
                    (key, isUpdate, dictnr) =>
                    {
                        immtbl = dictnr.AddOrUpdateClnbl<Guid, IAppUserSessionData, AppUserSessionDataImmtbl, AppUserSessionDataMtbl>(
                        mapper,
                        userSessionGuid,
                        k => SetAppUserDataProps(
                            new AppUserSessionDataMtbl
                            {
                                UserSessionGuid = userSessionGuid,
                                LoginDateTimeUtc = utcNow,
                                LocalSessionGuids = new NestedObjNmrbl<Guid>(null, new List<Guid>())
                            },
                        usernameHash,
                        usernameHashBytesList),
                        (k, isUpdate, data) =>
                        {
                            data.LastActiveDateTimeUtc = utcNow;
                            data.LocalSessionGuids.Mtbl.Add(localSessionGuid);

                            return data;
                        });

                        return dictnr;
                    });

                var mtbl = new AppUserSessionDataMtbl(mapper, immtbl);

                await localStorage.SetItemAsync(
                    LocalStorageKeys.UserSessionId,
                    mtbl);
            }

            return immtbl;
        }

        public async Task<IAppUserSessionData> TryRemoveUserSessionAsync(
            IHttpContextAccessor httpContextAccessor,
            ILocalStorageSvc localStorage,
            ISessionStorageSvc sessionStorage)
        {
            var sessionProps = GetSessionProps(httpContextAccessor);
            IAppUserSessionData immtbl = null;

            var usernameHashBytes = sessionProps.Item3.RdnlC();
            var userSessionGuid = sessionProps.Item1;

            if (sessionProps != null)
            {
                ConcurrentDictionary<Guid, IAppUserSessionData> dictnr;

                if (userSessionsDictnr.TryGetValue(
                    usernameHashBytes,
                    out dictnr))
                {
                    dictnr.TryRemove(userSessionGuid, out immtbl);
                }
            }

            await ClearStorage(localStorage, sessionStorage);
            return immtbl;
        }

        private bool IsTrmrkKey(string key)
        {
            bool retVal = key.StartsWithChr(
                LocalStorageKeys.TRMRK,
                true,
                '-',
                '[');

            return retVal;
        }

        private async Task ClearStorage(
            ILocalStorageSvc localStorage,
            ISessionStorageSvc sessionStorage)
        {
            if (await localStorage.ContainKeyAsync(LocalStorageKeys.UserSessionId))
            {
                await localStorage.RemoveItemAsync(
                    LocalStorageKeys.UserSessionId);
            }

            var keysArr = (await localStorage.KeysAsync(
                )).Where(IsTrmrkKey).ToArray();

            await localStorage.RemoveItemsAsync(keysArr);
            await sessionStorage.ClearAsync();
        }

        private TMtbl SetAppUserDataProps<TMtbl>(
            TMtbl mtbl,
            string usernameHash,
            List<byte> usernameHashBytesList)
            where TMtbl : AppUserDataCoreMtbl
        {
            mtbl.UsernameHash = usernameHash;

            mtbl.UsernameHashBytes = new NestedObjNmrbl<byte>(
                null,
                usernameHashBytesList);

            return mtbl;
        }

        private Tuple<Guid, string, byte[]> GetSessionProps(
            IHttpContextAccessor httpContextAccessor)
        {
            var httpContext = httpContextAccessor.HttpContext;
            var cookies = httpContext.Request.Cookies;

            var userSessionGuid = cookies.GetNullableValue<Guid>(
                SessionKeys.UserSessionGuid,
                Guid.TryParse);

            var usernameHash = cookies.GetStr(SessionKeys.UserName);
            byte[] usernameHashBytes = usernameHash.TryDecodeFromBase64();

            Tuple<Guid, string, byte[]> retTuple = null;

            if (userSessionGuid.HasValue && usernameHash != null && usernameHashBytes != null)
            {
                retTuple = new Tuple<Guid, string, byte[]>(
                    userSessionGuid.Value, usernameHash, usernameHashBytes);
            }

            return retTuple;
        }
    }
}
