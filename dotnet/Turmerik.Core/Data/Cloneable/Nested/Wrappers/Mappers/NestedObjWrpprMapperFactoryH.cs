using System;
using System.Collections.Generic;
using System.Text;
using Turmerik.Core.Helpers;

namespace Turmerik.Core.Data.Cloneable.Nested.Wrappers.Mappers
{
    public static class NestedObjWrpprMapperFactoryH
    {
        public static INestedObjWrpprMapperCore GetNestedObjWrpprMapperFactory(
            this ITypesStaticDataCache typesCache,
            ICloneableMapper mapper,
            IClonnerFactory clonnerFactory,
            Type genericType,
            params Type[] typeArgs)
        {
            Type genericTypeDef = typesCache.Get(genericType).GenericTypeDef.Value;
            Type factoryType = genericTypeDef.MakeGenericType(typeArgs);

            var factory = Activator.CreateInstance(factoryType) as INestedObjWrpprMapperFactoryCore;
            var retMapper = factory.GetMapperInstn(mapper, clonnerFactory);

            return retMapper;
        }
    }
}
