using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Turmerik.Core.Cloneable;
using Turmerik.Core.Cloneable.Nested;

namespace Turmerik.AspNetCore.OpenId.UserSession
{
    public interface IAppUserDataCore : ICloneableObject
    {
        string UsernameHash { get; }
        NestedObjNmrbl<byte> UsernameHashBytes { get; }
    }

    public abstract class AppUserDataCoreImmtbl : CloneableObjectImmtblBase, IAppUserDataCore
    {
        public AppUserDataCoreImmtbl(ClnblArgs args) : base(args)
        {
        }

        public AppUserDataCoreImmtbl(ICloneableMapper mapper, IAppUserDataCore src) : base(mapper, src)
        {
        }

        public string UsernameHash { get; protected set; }
        public NestedObjNmrbl<byte> UsernameHashBytes { get; protected set; }
    }

    public abstract class AppUserDataCoreMtbl : CloneableObjectMtblBase, IAppUserDataCore
    {
        public AppUserDataCoreMtbl()
        {
        }

        public AppUserDataCoreMtbl(ClnblArgs args) : base(args)
        {
        }

        public AppUserDataCoreMtbl(ICloneableMapper mapper, IAppUserDataCore src) : base(mapper, src)
        {
        }

        public string UsernameHash { get; set; }
        public NestedObjNmrbl<byte> UsernameHashBytes { get; set; }
    }
}
