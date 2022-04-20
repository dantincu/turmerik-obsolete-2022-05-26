using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;
using Turmerik.Core.Data;
using Turmerik.Core.Data.Cloneable;
using Turmerik.Core.Data.Cloneable.Mappers;
using Turmerik.Core.Helpers;
using Turmerik.Core.Threading;

namespace Turmerik.Core.Infrastucture
{
    public interface ITrmrkCoreServiceCollection : ICloneableObject
    {
        ITypesStaticDataCache TypesStaticDataCache { get; }
    }

    public class TrmrkCoreServiceCollectionImmtbl : CloneableObjectImmtblBase, ITrmrkCoreServiceCollection
    {
        public TrmrkCoreServiceCollectionImmtbl(ITrmrkCoreServiceCollection src)
        {
            TypesStaticDataCache = src.TypesStaticDataCache;
        }

        public ITypesStaticDataCache TypesStaticDataCache { get; protected set; }
    }

    public class TrmrkCoreServiceCollectionMtbl : CloneableObjectMtblBase, ITrmrkCoreServiceCollection
    {
        public TrmrkCoreServiceCollectionMtbl()
        {
        }

        public TrmrkCoreServiceCollectionMtbl(ITrmrkCoreServiceCollection src)
        {
            TypesStaticDataCache = src.TypesStaticDataCache;
        }

        public ITypesStaticDataCache TypesStaticDataCache { get; set; }
    }

    public class TrmrkCoreServiceCollectionBuilder
    {
        public static ITrmrkCoreServiceCollection RegisterAll(IServiceCollection services)
        {
            var mtbl = new TrmrkCoreServiceCollectionMtbl
            {
                TypesStaticDataCache = new TypesStaticDataCache()
            };

            var immtbl = new TrmrkCoreServiceCollectionImmtbl(mtbl);

            services.AddSingleton(immtbl.TypesStaticDataCache);
            services.AddSingleton<IEnumValuesStaticDataCache, EnumValuesStaticDataCache>();

            services.AddSingleton<IConsoleComponent, ConsoleComponent>();
            services.AddSingleton<ISynchronizerComponent, SynchronizerComponent>();

            services.AddSingleton<IStringToStringConverterCore, StringToStringConverterCore>();
            services.AddSingleton<IStringToIntConverter, StringToIntConverter>();
            services.AddSingleton<IStringToBoolConverter, StringToBoolConverter>();
            services.AddSingleton<IStringToEnumConverter, StringToEnumConverter>();
            services.AddSingleton<IStringToDateTimeConverter, StringToDateTimeConverter>();

            services.AddSingleton<ICloneableImmtblMapper, CloneableImmtblMapper>();
            services.AddSingleton<ICloneableMtblMapper, CloneableMtblMapper>();

            return immtbl;
        }
    }
}
