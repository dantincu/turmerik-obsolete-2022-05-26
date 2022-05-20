using Microsoft.AspNetCore.Http;
using System.Collections.Concurrent;
using Turmerik.AspNetCore.AppStartup;
using Turmerik.AspNetCore.Infrastructure;
using Turmerik.AspNetCore.Services.LocalSessionStorage;
using Turmerik.Core.Cloneable;
using Turmerik.Core.Cloneable.Nested;
using Turmerik.Core.Components;
using Turmerik.Core.Data;
using Turmerik.Core.Helpers;

namespace Turmerik.AspNetCore.OpenId.UserSession
{
    public interface IUserSessionsDictnr
    {
        Task<IAppUserSessionData> TryAddOrUpdateUserSessionAsync(
            IHttpContextAccessor httpContextAccessor,
            ILocalStorageWrapper localStorage,
            ISessionStorageWrapper sessionStorage,
            Guid localSessionGuid);

        Task<IAppUserSessionData> TryRemoveUserSessionAsync(
            IHttpContextAccessor httpContextAccessor,
            ILocalStorageWrapper localStorage,
            ISessionStorageWrapper sessionStorage);
    }

    public class UserSessionsDictnr : IUserSessionsDictnr
    {
        private readonly ICloneableMapper mapper;

        private readonly ConcurrentDictionary<IReadOnlyCollection<byte>, IAppUserData> usersDataDictnr;
        private readonly ConcurrentDictionary<IReadOnlyCollection<byte>, ConcurrentDictionary<Guid, IAppUserSessionData>> userSessionsDictnr;

        public UserSessionsDictnr(
            ICloneableMapper mapper)
        {
            this.mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            usersDataDictnr = new ConcurrentDictionary<IReadOnlyCollection<byte>, IAppUserData>();

            userSessionsDictnr = new ConcurrentDictionary<IReadOnlyCollection<byte>, ConcurrentDictionary<Guid, IAppUserSessionData>>(
                new BytesRdnlClctnEqCompr());
        }

        public async Task<IAppUserSessionData> TryAddOrUpdateUserSessionAsync(
            IHttpContextAccessor httpContextAccessor,
            ILocalStorageWrapper localStorage,
            ISessionStorageWrapper sessionStorage,
            Guid localSessionGuid)
        {
            var sessionProps = GetSessionProps(httpContextAccessor);
            IAppUserSessionData immtbl = null;

            if (sessionProps != null)
            {
                var userSessionGuid = sessionProps.Item1;
                var usernameHashBytes = sessionProps.Item2;

                var usernameHashBytesList = usernameHashBytes.ToList();
                var utcNow = DateTime.UtcNow;

                usersDataDictnr.GetOrAdd(
                    sessionProps.Item2,
                    key => SetAppUserDataProps(
                        new AppUserDataMtbl(),
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
                        usernameHashBytesList),
                        (k, isUpdate, data) =>
                        {
                            data.LastActiveDateTimeUtc = utcNow;

                            if (!data.LocalSessionGuids.Mtbl.Contains(localSessionGuid))
                            {
                                data.LocalSessionGuids.Mtbl.Add(localSessionGuid);
                            }
                            
                            return data;
                        });

                        return dictnr;
                    });

                var mtbl = new AppUserSessionDataMtbl(mapper, immtbl);

                await localStorage.Service.SetItemAsync(
                    LocalStorageKeys.UserSessionId,
                    mtbl);
            }

            return immtbl;
        }

        public async Task<IAppUserSessionData> TryRemoveUserSessionAsync(
            IHttpContextAccessor httpContextAccessor,
            ILocalStorageWrapper localStorage,
            ISessionStorageWrapper sessionStorage)
        {
            var sessionProps = GetSessionProps(
                httpContextAccessor);

            IAppUserSessionData immtbl = null;

            var userSessionGuid = sessionProps.Item1;
            var usernameHashBytes = sessionProps.Item2.RdnlC();

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
            ILocalStorageWrapper localStorage,
            ISessionStorageWrapper sessionStorage)
        {
            if (await localStorage.Service.ContainKeyAsync(LocalStorageKeys.UserSessionId))
            {
                await localStorage.Service.RemoveItemAsync(
                    LocalStorageKeys.UserSessionId);
            }

            var keysArr = (await localStorage.Service.KeysAsync(
                )).Where(IsTrmrkKey).ToArray();

            await localStorage.Service.RemoveItemsAsync(keysArr);
            await sessionStorage.Service.ClearAsync();
        }

        private TMtbl SetAppUserDataProps<TMtbl>(
            TMtbl mtbl,
            List<byte> usernameHashBytesList)
            where TMtbl : AppUserDataCoreMtbl
        {
            mtbl.UsernameHashBytes = new NestedObjNmrbl<byte>(
                null,
                usernameHashBytesList);

            return mtbl;
        }

        private Tuple<Guid, byte[]> GetSessionProps(
            IHttpContextAccessor httpContextAccessor)
        {
            var httpContext = httpContextAccessor.HttpContext;
            var username = httpContext.User.Identity.Name;

            var userSessionGuid = GetUserSessionGuid(httpContext);
            byte[] usernameHashBytes = EncodeH.EncodeSha1(username);

            Tuple<Guid, byte[]> retTuple = null;

            if (userSessionGuid.HasValue)
            {
                retTuple = new Tuple<Guid, byte[]>(
                    userSessionGuid.Value, usernameHashBytes);
            }

            return retTuple;
        }

        private Guid? GetUserSessionGuid(
            HttpContext httpContext)
        {
            var cookies = httpContext.Request.Cookies;

            var userSessionGuid = cookies.GetNullableValue<Guid>(
                SessionKeys.UserSessionGuid,
                Guid.TryParse);

            return userSessionGuid;
        }
    }
}
