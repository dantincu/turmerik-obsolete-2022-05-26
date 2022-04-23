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
            ICloneableMapper mapper,
            INestedObj obj,
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
        private readonly IClonnerFactory clonnerFactory;
        private readonly ReadOnlyDictionary<Type, NestedObjMapperFactoryTypesTuple> mapperFactoryGenericTypesDictnr;
        private readonly StaticDataCache<Type, TypeWrapper> nestedObjTypesMappings;

        public NestedObjMapperMainFactory(
            ITypesStaticDataCache typesCache,
            IClonnerFactory clonnerFactory)
        {
            this.clonnerFactory = clonnerFactory ?? throw new ArgumentNullException(nameof(clonnerFactory));

            mapperFactoryGenericTypesDictnr = GetMapperFactoryGenericTypesDictnr(typesCache);
            nestedObjTypesMappings = GetNestedObjTypesMappings(mapperFactoryGenericTypesDictnr);
        }

        public INestedObjMapper GetMapper(
            ICloneableMapper mapper,
            INestedObj obj,
            Type propType,
            bool isMtbl)
        {
            var nestedType = nestedObjTypesMappings.Get(propType);
            Type[] genericTypeArgs = nestedType.GenericTypeArgs.Value.ToArray();

            var genericTypeDef = nestedType.GenericTypeDef.Value;
            var mapperFactoryGenericTypeTuple = mapperFactoryGenericTypesDictnr[genericTypeDef];

            TypeWrapper mapperFactoryGenericType;

            if (isMtbl)
            {
                mapperFactoryGenericType = mapperFactoryGenericTypeTuple.MtblMapperFactoryType;
            }
            else
            {
                mapperFactoryGenericType = mapperFactoryGenericTypeTuple.ImmtblMapperFactoryType;
            }

            Type mapperFactoryType = mapperFactoryGenericType.Data.MakeGenericType(genericTypeArgs);

            var mapperFactory = mapperFactoryType.Create<INestedObjMapper>(
                clonnerFactory,
                mapper);

            return mapperFactory;
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
                    typeof(INestedObjNmrbl<object>),
                    typeof(NestedImmtblObjNmrblMapperFactory<object>),
                    typeof(NestedMtblObjNmrblMapperFactory<object>)));

                callback(factory(typeof(INestedObjDictnr),
                    typeof(INestedObjDictnr<object, object>),
                    typeof(NestedImmtblObjDictnrMapperFactory<object, object>),
                    typeof(NestedMtblObjDictnrMapperFactory<object, object>)));

                callback(factory(typeof(INestedClnbl),
                    typeof(INestedClnbl<ICloneableObject, ICloneableObject, ICloneableObject>),
                    typeof(NestedImmtblClnblMapperFactory<ICloneableObject, ICloneableObject, ICloneableObject>),
                    typeof(NestedMtblClnblMapperFactory<ICloneableObject, ICloneableObject, ICloneableObject>)));

                callback(factory(typeof(INestedClnblNmrbl),
                    typeof(INestedClnblNmrbl<ICloneableObject, ICloneableObject, ICloneableObject>),
                    typeof(NestedImmtblClnblNmrblMapperFactory<ICloneableObject, ICloneableObject, ICloneableObject>),
                    typeof(NestedMtblClnblNmrblMapperFactory<ICloneableObject, ICloneableObject, ICloneableObject>)));

                callback(factory(typeof(INestedClnblDictnr),
                    typeof(INestedClnblDictnr<object, ICloneableObject, ICloneableObject, ICloneableObject>),
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

        private StaticDataCache<Type, TypeWrapper> GetNestedObjTypesMappings(
            ReadOnlyDictionary<Type, NestedObjMapperFactoryTypesTuple> mapperFactoryGenericTypesDictnr)
        {
            var nestedObjTypesMappings = new StaticDataCache<Type, TypeWrapper>(
                propType => mapperFactoryGenericTypesDictnr.First(
                kvp => kvp.Key.IsAssignableFrom(propType)).Value.NestedType);

            return nestedObjTypesMappings;
        }
    }
}
