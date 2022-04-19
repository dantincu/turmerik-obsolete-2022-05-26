using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;
using Turmerik.Core.Data.Cloneable.Mappers;
using Turmerik.Core.Helpers;
using Turmerik.Core.Threading;

namespace Turmerik.Core.Infrastucture
{
    public class ServiceCollectionBuilder
    {
        public static void RegisterAllServices(IServiceCollection services)
        {
            services.AddSingleton<ITypesStaticDataCache, TypesStaticDataCache>();
            services.AddSingleton<IEnumValuesStaticDataCache, EnumValuesStaticDataCache>();
            services.AddSingleton<IConsoleComponent, ConsoleComponent>();
            services.AddSingleton<ISynchronizerComponent, SynchronizerComponent>();

            services.AddSingleton<ICloneableImmtblMapper, CloneableImmtblMapper>();
            services.AddSingleton<ICloneableMtblMapper, CloneableMtblMapper>();
        }
    }
}
