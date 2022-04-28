using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Turmerik.Core.Cloneable;

namespace Turmerik.AspNetCore.UserSession
{
    public interface  IAppUserData : IAppUserDataCore
    {
    }

    public class AppUserDataImmtbl : AppUserDataCoreImmtbl, IAppUserData
    {
        public AppUserDataImmtbl(ClnblArgs args) : base(args)
        {
        }

        public AppUserDataImmtbl(ICloneableMapper mapper, IAppUserData src) : base(mapper, src)
        {
        }
    }

    public class AppUserDataMtbl : AppUserDataCoreMtbl, IAppUserData
    {
        public AppUserDataMtbl()
        {
        }

        public AppUserDataMtbl(ClnblArgs args) : base(args)
        {
        }

        public AppUserDataMtbl(ICloneableMapper mapper, IAppUserData src) : base(mapper, src)
        {
        }
    }
}
