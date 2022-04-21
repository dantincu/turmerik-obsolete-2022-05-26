using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using Turmerik.Core.Data.Cloneable.Nested.Clnbl.Dictnr.Wrappers.Mappers;
using Turmerik.Core.Data.Cloneable.Nested.Clnbl.Nmrbl.Wrappers.Mappers;
using Turmerik.Core.Data.Cloneable.Nested.Clnbl.Wrappers.Mappers;
using Turmerik.Core.Data.Cloneable.Nested.Dictnr.Wrappers.Mappers;
using Turmerik.Core.Data.Cloneable.Nested.Nmrbl.Wrappers.Mappers;
using Turmerik.Core.Helpers;
using Turmerik.Core.Reflection.Wrappers;

namespace Turmerik.Core.Data.Cloneable.Nested.Wrappers.Mappers
{
    public interface INestedObjWrpprMapperFactoryCore
    {
        INestedObjWrpprMapperCore GetMapperInstn(
            ICloneableMapper mapper = null,
            IClonnerFactory clonnerFactory = null);
    }

    public interface INestedObjWrpprMapperFactory<TMapper, TWrppr> : INestedObjWrpprMapperFactoryCore
        where TMapper : INestedObjWrpprMapperCore
        where TWrppr : INestedObjWrpprCore
    {
        TMapper GetMapper(
            ICloneableMapper mapper = null,
            IClonnerFactory clonnerFactory = null);
    }

    public interface INestedObjWrpprMapperMainFactory
    {
        INestedObjWrpprMapperCore GetINestedObjWrpprMapperCore(
            Type srcPropType,
            Type trgPropType);
    }

    public class NestedObjWrpprMapperFactory : INestedObjWrpprMapperMainFactory
    {
        private static readonly IReadOnlyDictionary<Type, Type> mapperFactoryDictnr;

        private readonly ITypesStaticDataCache typesCache;
        private readonly ICloneableMapper mapper;
        private readonly IClonnerFactory clonnerFactory;

        static NestedObjWrpprMapperFactory()
        {
            mapperFactoryDictnr = new Dictionary<Type, Type>
            {
                { typeof(NestedImmtblObjWrpprMapper<object>), typeof(NestedImmtblObjWrpprMapperFactory<object>) },
                { typeof(NestedMtblObjWrpprMapper<object>), typeof(NestedMtblObjWrpprMapperFactory<object>) },
                { typeof(NestedImmtblObjNmrblWrpprMapper<object>), typeof(NestedImmtblObjNmrblWrpprMapperFactory<object>) },
                { typeof(NestedMtblObjNmrblWrpprMapper<object>), typeof(NestedMtblObjNmrblWrpprMapperFactory<object>) },
                { typeof(NestedImmtblObjDictnrWrpprMapper<object, object>), typeof(NestedImmtblObjDictnrWrpprMapperFactory<object, object>) },
                { typeof(NestedMtblObjDictnrWrpprMapper<object, object>), typeof(NestedMtblObjDictnrWrpprMapperFactory<object, object>) },
                {
                    typeof(NestedImmtblClnblWrpprMapper<ICloneableObject, ICloneableObject, ICloneableObject>),
                    typeof(NestedImmtblClnblWrpprMapperFactory<ICloneableObject, ICloneableObject, ICloneableObject>)
                },
                {
                    typeof(NestedMtblClnblWrpprMapper<ICloneableObject, ICloneableObject, ICloneableObject>),
                    typeof(NestedMtblClnblWrpprMapperFactory<ICloneableObject, ICloneableObject, ICloneableObject>)
                },
                {
                    typeof(NestedImmtblClnblNmrblWrpprMapper<ICloneableObject, ICloneableObject, ICloneableObject>),
                    typeof(NestedImmtblClnblNmrblWrpprMapperFactory<ICloneableObject, ICloneableObject, ICloneableObject>)
                },
                {
                    typeof(NestedMtblClnblNmrblWrpprMapper<ICloneableObject, ICloneableObject, ICloneableObject>),
                    typeof(NestedMtblClnblNmrblWrpprMapperFactory<ICloneableObject, ICloneableObject, ICloneableObject>)
                },
                {
                    typeof(NestedImmtblClnblDictnrWrpprMapper<object, ICloneableObject, ICloneableObject, ICloneableObject>),
                    typeof(NestedImmtblClnblDictnrWrpprMapperFactory<object, ICloneableObject, ICloneableObject, ICloneableObject>)
                },
                {
                    typeof(NestedMtblClnblDictnrWrpprMapper<object, ICloneableObject, ICloneableObject, ICloneableObject>),
                    typeof(NestedMtblClnblDictnrWrpprMapperFactory<object, ICloneableObject, ICloneableObject, ICloneableObject>)
                }
            }.RdnlD();
        }

        public NestedObjWrpprMapperFactory(
            ITypesStaticDataCache typesCache,
            ICloneableMapper mapper,
            IClonnerFactory clonnerFactory)
        {
            this.typesCache = typesCache ?? throw new ArgumentNullException(nameof(typesCache));
            this.mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            this.clonnerFactory = clonnerFactory ?? throw new ArgumentNullException(nameof(clonnerFactory));
        }

        public INestedObjWrpprMapperCore GetINestedObjWrpprMapperCore(
            Type srcPropType,
            Type trgPropType)
        {
            INestedObjWrpprMapperCore mapper;

            if (srcPropType.IsGenericType)
            {
                var trgData = GetMapperTypeData(trgPropType);
                mapper = GetINestedObjWrpprMapperCore(trgData);
            }
            else
            {
                throw new InvalidOperationException($"The provided type {srcPropType.GetTypeFullDisplayName()} is not a generic type");
            }

            return mapper;
        }

        private MapperTypeData GetMapperTypeData(Type propValueType)
        {
            if (!propValueType.IsGenericType)
            {
                throw new InvalidOperationException($"The provided type {propValueType.GetTypeFullDisplayName()} is not a generic type");
            }

            var type = typesCache.Get(propValueType);

            var genericTypeDef = typesCache.Get(
                type.GenericTypeDef.Value);

            var mapperData = new MapperTypeData
            {
                Type = type,
                GenericTypeDef = genericTypeDef,
                GenericTypeParams = genericTypeDef.GenericTypeParams.Value.Select(
                    t => typesCache.Get(t)).ToArray(),
                GenericTypeArgs = genericTypeDef.GenericTypeArgs.Value.Select(
                    t => typesCache.Get(t)).ToArray()
            };

            return mapperData;
        }

        private INestedObjWrpprMapperCore GetINestedObjWrpprMapperCore(MapperTypeData trgData)
        {
            var factoryGenericType = mapperFactoryDictnr[trgData.GenericTypeDef.Data];

            var genericTypeParams = trgData.GenericTypeParams.Select(
                tw => tw.Data).ToArray();

            var mapper = GetNestedObjWrpprMapper(
                factoryGenericType,
                genericTypeParams);

            return mapper;
        }

        private INestedObjWrpprMapperCore GetNestedObjWrpprMapper(
            Type genericTypeDef,
            Type[] typeArgs)
        {
            Type factoryType = genericTypeDef.MakeGenericType(typeArgs);

            var factory = Activator.CreateInstance(factoryType) as INestedObjWrpprMapperFactoryCore;
            var retMapper = factory.GetMapperInstn(mapper, clonnerFactory);

            return retMapper;
        }

        private class MapperTypeData
        {
            public TypeWrapper Type { get; set; }
            public TypeWrapper GenericTypeDef { get; set; }
            public TypeWrapper[] GenericTypeParams { get; set; }
            public TypeWrapper[] GenericTypeArgs { get; set; }
        }
    }
}
