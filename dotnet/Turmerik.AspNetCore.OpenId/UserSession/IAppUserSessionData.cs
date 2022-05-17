using System.Collections.ObjectModel;
using Turmerik.Core.Cloneable;
using Turmerik.Core.Cloneable.Nested;
using Turmerik.Core.Helpers;

namespace Turmerik.AspNetCore.OpenId.UserSession
{
    public interface IAppUserSessionData : IAppUserDataCore
    {
        Guid UserSessionGuid { get; }
        DateTime LoginDateTimeUtc { get; }
        DateTime LastActiveDateTimeUtc { get; }
        DateTime? LogoutDateTimeUtc { get; }
        NestedObjNmrbl<Guid> LocalSessionGuids { get; }
    }

    public class AppUserSessionDataImmtbl : AppUserDataCoreImmtbl, IAppUserSessionData
    {
        public AppUserSessionDataImmtbl(ClnblArgs args) : base(args)
        {
        }

        public AppUserSessionDataImmtbl(ICloneableMapper mapper, IAppUserSessionData src) : base(mapper, src)
        {
        }

        public Guid UserSessionGuid { get; protected set; }
        public DateTime LoginDateTimeUtc { get; protected set; }
        public DateTime LastActiveDateTimeUtc { get; protected set; }
        public DateTime? LogoutDateTimeUtc { get; protected set; }
        public NestedObjNmrbl<Guid> LocalSessionGuids { get; protected set; }
    }

    public class AppUserSessionDataMtbl : AppUserDataCoreMtbl, IAppUserSessionData
    {
        public AppUserSessionDataMtbl()
        {
        }

        public AppUserSessionDataMtbl(ClnblArgs args) : base(args)
        {
        }

        public AppUserSessionDataMtbl(ICloneableMapper mapper, IAppUserSessionData src) : base(mapper, src)
        {
        }

        public Guid UserSessionGuid { get; set; }
        public DateTime LoginDateTimeUtc { get; set; }
        public DateTime LastActiveDateTimeUtc { get; set; }
        public DateTime? LogoutDateTimeUtc { get; set; }
        public NestedObjNmrbl<Guid> LocalSessionGuids { get; set; }
    }
}
