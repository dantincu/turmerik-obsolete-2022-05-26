using System.Collections.ObjectModel;
using Turmerik.Core.Cloneable;
using Turmerik.Core.Cloneable.Nested;
using Turmerik.Core.Helpers;

namespace Turmerik.AspNetCore.UserSession
{
    public interface IAppUserSession : ICloneableObject
    {
        Guid UserSessionGuid { get; }
        string UsernameHash { get; }
        NestedObjNmrbl<byte> UsernameHashBytes { get; }
    }

    public class AppUserSessionImmtbl : CloneableObjectImmtblBase, IAppUserSession
    {
        public AppUserSessionImmtbl(ClnblArgs args) : base(args)
        {
        }

        public AppUserSessionImmtbl(ICloneableMapper mapper, ICloneableObject src) : base(mapper, src)
        {
        }

        public Guid UserSessionGuid { get; protected set; }
        public string UsernameHash { get; protected set; }
        public NestedObjNmrbl<byte> UsernameHashBytes { get; protected set; }
    }

    public class AppUserSessionMtbl : CloneableObjectMtblBase, IAppUserSession
    {
        public AppUserSessionMtbl()
        {
        }

        public AppUserSessionMtbl(ClnblArgs args) : base(args)
        {
        }

        public AppUserSessionMtbl(ICloneableMapper mapper, ICloneableObject src) : base(mapper, src)
        {
        }

        public Guid UserSessionGuid { get; set; }
        public string UsernameHash { get; set; }
        public NestedObjNmrbl<byte> UsernameHashBytes { get; set; }
    }
}
