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
        INestedObjWrpprMapperCore GetINestedObjWrpprMapperCore(Type mapperType);
    }

    public class NestedObjWrpprMapperFactory : INestedObjWrpprMapperFactory
    {
        private readonly ITypesStaticDataCache typesCache;

        public NestedObjWrpprMapperFactory(ITypesStaticDataCache typesCache)
        {
            this.typesCache = typesCache ?? throw new ArgumentNullException(nameof(typesCache));
        }

        public INestedObjWrpprMapperCore GetINestedObjWrpprMapperCore(Type mapperType)
        {
            INestedObjWrpprMapperCore mapper;

            if (mapperType.IsGenericType)
            {
                var type = typesCache.Get(mapperType);

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

                mapper = GetINestedObjWrpprMapperCore(mapperData);
            }
            else
            {
                throw new InvalidOperationException($"The provided type {mapperType.GetTypeFullDisplayName()} is not a generic type");
            }

            return mapper;
        }

        private INestedObjWrpprMapperCore GetINestedObjWrpprMapperCore(MapperTypeData mapperData)
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
