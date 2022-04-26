using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Turmerik.AspNetCore.Settings;
using Turmerik.AspNetCore.UserSession;
using Turmerik.Core.Infrastucture;

namespace Turmerik.AspNetCore.Services
{
    public interface IAppCoreServiceCollection : ITrmrkCoreServiceCollection
    {
        TrmrkAppSettingsMtbl TrmrkAppSettings { get; }
    }

    public class AppCoreServiceCollectionImmtbl : TrmrkCoreServiceCollectionImmtbl, IAppCoreServiceCollection
    {
        public AppCoreServiceCollectionImmtbl(IAppCoreServiceCollection src) : base(src)
        {
            TrmrkAppSettings = src.TrmrkAppSettings;
        }

        public TrmrkAppSettingsMtbl TrmrkAppSettings { get; protected set; }
    }

    public class AppCoreServiceCollectionMtbl : TrmrkCoreServiceCollectionMtbl, IAppCoreServiceCollection
    {
        public AppCoreServiceCollectionMtbl()
        {
        }

        public AppCoreServiceCollectionMtbl(ITrmrkCoreServiceCollection src) : base(src)
        {
        }

        public AppCoreServiceCollectionMtbl(IAppCoreServiceCollection src) : base(src)
        {
            TrmrkAppSettings = src.TrmrkAppSettings;
        }

        public TrmrkAppSettingsMtbl TrmrkAppSettings { get; set; }
    }
}
