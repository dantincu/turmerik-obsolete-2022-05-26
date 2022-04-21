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
    public interface INestedObjWrpprMapperFactory
    {
        INestedObjWrpprMapperCore GetINestedObjWrpprMapperCore(
            Type srcPropType,
            Type trgPropType);
    }

    public class NestedObjWrpprMapperFactory : INestedObjWrpprMapperFactory
    {
        private readonly ITypesStaticDataCache typesCache;

        public NestedObjWrpprMapperFactory(ITypesStaticDataCache typesCache)
        {
            this.typesCache = typesCache ?? throw new ArgumentNullException(nameof(typesCache));
        }

        public INestedObjWrpprMapperCore GetINestedObjWrpprMapperCore(
            Type srcPropType,
            Type trgPropType)
        {
            INestedObjWrpprMapperCore mapper;

            if (srcPropType.IsGenericType)
            {
                var srcData = GetMapperTypeData(srcPropType);
                var trgData = GetMapperTypeData(trgPropType);

                mapper = GetINestedObjWrpprMapperCore(
                    srcData,
                    trgData);
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

        private INestedObjWrpprMapperCore GetINestedObjWrpprMapperCore(
            MapperTypeData srcData,
            MapperTypeData trgData)
        {
            throw new NotImplementedException();
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
