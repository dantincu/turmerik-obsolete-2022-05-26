using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using Turmerik.Core.Cloneable.Nested;
using Turmerik.Core.Cloneable.Nested.Clnbl;
using Turmerik.Core.Cloneable.Nested.Clnbl.Mappers;
using Turmerik.Core.Cloneable.Nested.Clnbl.Mappers.Factories;
using Turmerik.Core.Cloneable.Nested.Mappers;
using Turmerik.Core.Cloneable.Nested.Mappers.Factories;
using Turmerik.Core.Collections.Builders;
using Turmerik.Core.Collections.Cache;
using Turmerik.Core.Helpers;
using Turmerik.Core.Reflection;
using Turmerik.Core.Reflection.Wrappers;

namespace Turmerik.Core.Cloneable
{
    public interface INestedObjMapperMainFactory
    {
        INestedObjMapper GetMapper(
            ICloneableMapper mainMapper,
            Type propType,
            bool isMtbl);
    }

    public class NestedObjMapperFactoryTypesTuple
    {
        public NestedObjMapperFactoryTypesTuple(
            TypeWrapper nestedType,
            TypeWrapper immtblMapperFactoryType,
            TypeWrapper mtblMapperFactoryType)
        {
            NestedType = nestedType ?? throw new ArgumentNullException(nameof(nestedType));
            ImmtblMapperFactoryType = immtblMapperFactoryType ?? throw new ArgumentNullException(nameof(immtblMapperFactoryType));
            MtblMapperFactoryType = mtblMapperFactoryType ?? throw new ArgumentNullException(nameof(mtblMapperFactoryType));
        }

        public TypeWrapper NestedType { get; }
        public TypeWrapper ImmtblMapperFactoryType { get; }
        public TypeWrapper MtblMapperFactoryType { get; }
    }

    public class NestedObjMapperMainFactory : INestedObjMapperMainFactory
    {
        private readonly ITypesStaticDataCache typesCache;
        private readonly IClonnerFactory clonnerFactory;
        private readonly ReadOnlyDictionary<Type, NestedObjMapperFactoryTypesTuple> mapperFactoryGenericTypesDictnr;
        private readonly StaticDataCache<Type, NestedObjMapperFactoryTypesTuple> nestedObjTypesMappings;

        public NestedObjMapperMainFactory(
            ITypesStaticDataCache typesCache,
            IClonnerFactory clonnerFactory)
        {
            this.typesCache = typesCache ?? throw new ArgumentNullException(nameof(typesCache));
            this.clonnerFactory = clonnerFactory ?? throw new ArgumentNullException(nameof(clonnerFactory));

            mapperFactoryGenericTypesDictnr = GetMapperFactoryGenericTypesDictnr(typesCache);
            nestedObjTypesMappings = GetNestedObjTypesMappings(typesCache);
        }

        public INestedObjMapper GetMapper(
            ICloneableMapper mainMapper,
            Type propType,
            bool isMtbl)
        {
            var nestedTypeTuple = nestedObjTypesMappings.Get(propType);
            TypeWrapper mapperFactoryType;

            if (isMtbl)
            {
                mapperFactoryType = nestedTypeTuple.MtblMapperFactoryType;
            }
            else
            {
                mapperFactoryType = nestedTypeTuple.ImmtblMapperFactoryType;
            }

            var mapperFactory = mapperFactoryType.Data.Create<INestedObjMapperFactory>(
                clonnerFactory,
                mainMapper);

            var mapper = mapperFactory.GetMapper();
            return mapper;
        }

        private TypeWrapper GetNestedObjGenericBaseType(
            ITypesStaticDataCache typesCache,
            TypeWrapper propTypeWrppr,
            KeyValuePair<Type, NestedObjMapperFactoryTypesTuple> nestedTypeKvp)
        {
            var genericTypeDef = propTypeWrppr.GenericTypeDef.Value;
            var nestedTypeGenericTypeDef = nestedTypeKvp.Value.NestedType.Data;

            while (genericTypeDef == null || genericTypeDef != nestedTypeGenericTypeDef)
            {
                propTypeWrppr = typesCache.Get(propTypeWrppr.Data.BaseType);
                genericTypeDef = propTypeWrppr.GenericTypeDef.Value;
            }

            return propTypeWrppr;
        }

        private NestedObjMapperFactoryTypesTuple GetNestedObjMapperFactoryTypesTuple(
            ITypesStaticDataCache typesCache, Type propType)
        {
            var propTypeWrppr = typesCache.Get(propType);

            var nestedTypeKvp = mapperFactoryGenericTypesDictnr.First(
                kvp => kvp.Key.IsAssignableFrom(propType));

            var nestedTypeGenericTypeDef = nestedTypeKvp.Value.NestedType.Data;

            var nestedBaseTypeWrppr = GetNestedObjGenericBaseType(typesCache, propTypeWrppr, nestedTypeKvp);
            var genericTypeArgs = nestedBaseTypeWrppr.GenericTypeArgs.Value.ToArray();

            Type immtblMapperFactoryType = nestedTypeKvp.Value.ImmtblMapperFactoryType.Data.MakeGenericType(genericTypeArgs);
            Type mtblMapperFactoryType = nestedTypeKvp.Value.MtblMapperFactoryType.Data.MakeGenericType(genericTypeArgs);

            var retTuple = new NestedObjMapperFactoryTypesTuple(
                typesCache.Get(nestedTypeKvp.Key),
                typesCache.Get(immtblMapperFactoryType),
                typesCache.Get(mtblMapperFactoryType));

            return retTuple;
        }

        private ReadOnlyDictionary<Type, NestedObjMapperFactoryTypesTuple> GetMapperFactoryGenericTypesDictnr(
            ITypesStaticDataCache typesCache)
        {
            var mapperFactoryGenericTypesDictnr = new Dictionary<Type, NestedObjMapperFactoryTypesTuple>
            {
            }.BuildDictnr(
            (dictnr, factory, callback) =>
            {
                callback(factory(typeof(INestedObjNmrbl),
                    typeof(NestedObjNmrbl<object>),
                    typeof(NestedImmtblObjNmrblMapperFactory<object>),
                    typeof(NestedMtblObjNmrblMapperFactory<object>)));

                callback(factory(typeof(INestedObjDictnr),
                    typeof(NestedObjDictnr<object, object>),
                    typeof(NestedImmtblObjDictnrMapperFactory<object, object>),
                    typeof(NestedMtblObjDictnrMapperFactory<object, object>)));

                callback(factory(typeof(INestedClnbl),
                    typeof(NestedClnbl<ICloneableObject, ICloneableObject, ICloneableObject>),
                    typeof(NestedImmtblClnblMapperFactory<ICloneableObject, ICloneableObject, ICloneableObject>),
                    typeof(NestedMtblClnblMapperFactory<ICloneableObject, ICloneableObject, ICloneableObject>)));

                callback(factory(typeof(INestedClnblNmrbl),
                    typeof(NestedClnblNmrbl<ICloneableObject, ICloneableObject, ICloneableObject>),
                    typeof(NestedImmtblClnblNmrblMapperFactory<ICloneableObject, ICloneableObject, ICloneableObject>),
                    typeof(NestedMtblClnblNmrblMapperFactory<ICloneableObject, ICloneableObject, ICloneableObject>)));

                callback(factory(typeof(INestedClnblDictnr),
                    typeof(NestedClnblDictnr<object, ICloneableObject, ICloneableObject, ICloneableObject>),
                    typeof(NestedImmtblClnblDictnrMapperFactory<object, ICloneableObject, ICloneableObject, ICloneableObject>),
                    typeof(NestedMtblClnblDictnrMapperFactory<object, ICloneableObject, ICloneableObject, ICloneableObject>)));
            }, (
                Type nestedType,
                Type nestedGenericType,
                Type immtblMapperFactoryType,
                Type mtblMapperFactoryType) => new KeyValuePair<Type, NestedObjMapperFactoryTypesTuple>(
                    nestedType,
                    new NestedObjMapperFactoryTypesTuple(
                        typesCache.GetGenericTypeDef(nestedGenericType, true),
                        typesCache.GetGenericTypeDef(immtblMapperFactoryType, true),
                        typesCache.GetGenericTypeDef(mtblMapperFactoryType, true)))).RdnlD();

            return mapperFactoryGenericTypesDictnr;
        }

        private StaticDataCache<Type, NestedObjMapperFactoryTypesTuple> GetNestedObjTypesMappings(
            ITypesStaticDataCache typesCache)
        {
            var nestedObjTypesMappings = new StaticDataCache<Type, NestedObjMapperFactoryTypesTuple>(
                propType => GetNestedObjMapperFactoryTypesTuple(typesCache, propType));

            return nestedObjTypesMappings;
        }
    }
}
