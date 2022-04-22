using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;
using Turmerik.Core.Data;
using Turmerik.Core.Data.Cloneable;
using Turmerik.Core.Data.Cloneable.Nested.Wrappers.Mappers;
using Turmerik.Core.Helpers;
using Turmerik.Core.Threading;

namespace Turmerik.Core.Infrastucture
{
    public interface ITrmrkCoreServiceCollection : ICloneableObject
    {
        ILambdaExprHelperFactory LambdaExprHelperFactory { get; }
        ITypesStaticDataCache TypesStaticDataCache { get; }
        IEnumValuesStaticDataCache EnumValuesStaticDataCache { get; }
        ISynchronizerFactory SynchronizerFactory { get; }
    }

    public class TrmrkCoreServiceCollectionImmtbl : ITrmrkCoreServiceCollection
    {
        public TrmrkCoreServiceCollectionImmtbl(ITrmrkCoreServiceCollection src)
        {
            LambdaExprHelperFactory = src.LambdaExprHelperFactory;
            TypesStaticDataCache = src.TypesStaticDataCache;
            EnumValuesStaticDataCache = src.EnumValuesStaticDataCache;
            SynchronizerFactory = src.SynchronizerFactory;
        }

        public ILambdaExprHelperFactory LambdaExprHelperFactory { get; }
        public ITypesStaticDataCache TypesStaticDataCache { get; }
        public IEnumValuesStaticDataCache EnumValuesStaticDataCache { get; }
        public ISynchronizerFactory SynchronizerFactory { get; }
    }

    public class TrmrkCoreServiceCollectionMtbl : ITrmrkCoreServiceCollection
    {
        public TrmrkCoreServiceCollectionMtbl()
        {
        }

        public TrmrkCoreServiceCollectionMtbl(ITrmrkCoreServiceCollection src)
        {
            LambdaExprHelperFactory = src.LambdaExprHelperFactory;
            TypesStaticDataCache = src.TypesStaticDataCache;
            EnumValuesStaticDataCache = src.EnumValuesStaticDataCache;
            SynchronizerFactory = src.SynchronizerFactory;
        }

        public ILambdaExprHelperFactory LambdaExprHelperFactory { get; set; }
        public ITypesStaticDataCache TypesStaticDataCache { get; set; }
        public IEnumValuesStaticDataCache EnumValuesStaticDataCache { get; set; }
        public ISynchronizerFactory SynchronizerFactory { get; set; }
    }

    public class TrmrkCoreServiceCollectionBuilder
    {
        public static ITrmrkCoreServiceCollection RegisterAll(IServiceCollection services)
        {
            var mtbl = new TrmrkCoreServiceCollectionMtbl
            {
                LambdaExprHelperFactory = new LambdaExprHelperFactory(),
                TypesStaticDataCache = new TypesStaticDataCache(),
                EnumValuesStaticDataCache = new EnumValuesStaticDataCache(),
                SynchronizerFactory = new SynchronizerFactory()
            };

            var immtbl = new TrmrkCoreServiceCollectionImmtbl(mtbl);

            services.AddSingleton(immtbl.LambdaExprHelperFactory);
            services.AddSingleton(immtbl.TypesStaticDataCache);
            services.AddSingleton(immtbl.EnumValuesStaticDataCache);
            services.AddSingleton(immtbl.SynchronizerFactory);

            services.AddSingleton<IConsoleComponent, ConsoleComponent>();

            services.AddSingleton<IStringToStringConverterCore, StringToStringConverterCore>();
            services.AddSingleton<IStringToIntConverter, StringToIntConverter>();
            services.AddSingleton<IStringToBoolConverter, StringToBoolConverter>();
            services.AddSingleton<IStringToEnumConverter, StringToEnumConverter>();
            services.AddSingleton<IStringToDateTimeConverter, StringToDateTimeConverter>();

            services.AddSingleton<IClonnerFactory, ClonnerFactory>();
            services.AddSingleton<INestedObjWrpprMapperMainFactory, NestedObjWrpprMapperFactory>();
            services.AddSingleton<ICloneableMapper, CloneableMapper>();

            return immtbl;
        }
    }
}
