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
using Turmerik.Core.Collections;
using Turmerik.Core.Helpers;
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
        private readonly ITypesStaticDataCache typesCache;
        private readonly IClonnerFactory clonnerFactory;
        private readonly ReadOnlyDictionary<Type, NestedObjMapperFactoryTypesTuple> mapperFactoryGenericTypesDictnr;

        public NestedObjMapperMainFactory(
            ITypesStaticDataCache typesCache,
            IClonnerFactory clonnerFactory)
        {
            this.typesCache = typesCache ?? throw new ArgumentNullException(nameof(typesCache));
            this.clonnerFactory = clonnerFactory ?? throw new ArgumentNullException(nameof(clonnerFactory));
            mapperFactoryGenericTypesDictnr = GetMapperFactoryGenericTypesDictnr(typesCache);
        }

        public INestedObjMapper GetMapper(
            ICloneableMapper mapper,
            INestedObj obj,
            Type propType,
            bool isMtbl)
        {
            var propTypeWrppr = typesCache.Get(propType);
            Type[] genericTypeArgs = propTypeWrppr.GenericTypeArgs.Value?.ToArray();

            if (genericTypeArgs == null)
            {
                throw new InvalidOperationException(
                    $"Provided property type {propType.FullName} is not generic");
            }

            var genericTypeDef = propTypeWrppr.GenericTypeDef.Value;
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

            var mapperFactoryObj = Activator.CreateInstance(
                mapperFactoryType,
                clonnerFactory,
                mapper);

            var mapperFactory = (INestedObjMapper)mapperFactoryObj;
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
                callback(factory(typeof(INestedObjNmrbl<object>),
                    typeof(NestedImmtblObjNmrblMapperFactory<object>),
                    typeof(NestedMtblObjNmrblMapperFactory<object>)));

                callback(factory(typeof(INestedObjDictnr<object, object>),
                    typeof(NestedImmtblObjDictnrMapperFactory<object, object>),
                    typeof(NestedMtblObjDictnrMapperFactory<object, object>)));

                callback(factory(typeof(INestedClnbl<ICloneableObject, ICloneableObject, ICloneableObject>),
                    typeof(NestedImmtblClnblMapperFactory<ICloneableObject, ICloneableObject, ICloneableObject>),
                    typeof(NestedMtblClnblMapperFactory<ICloneableObject, ICloneableObject, ICloneableObject>)));

                callback(factory(typeof(INestedClnblNmrbl<ICloneableObject, ICloneableObject, ICloneableObject>),
                    typeof(NestedImmtblClnblNmrblMapperFactory<ICloneableObject, ICloneableObject, ICloneableObject>),
                    typeof(NestedMtblClnblNmrblMapperFactory<ICloneableObject, ICloneableObject, ICloneableObject>)));

                callback(factory(typeof(INestedClnblDictnr<object, ICloneableObject, ICloneableObject, ICloneableObject>),
                    typeof(NestedImmtblClnblDictnrMapperFactory<object, ICloneableObject, ICloneableObject, ICloneableObject>),
                    typeof(NestedMtblClnblDictnrMapperFactory<object, ICloneableObject, ICloneableObject, ICloneableObject>)));
            }, (
                Type nestedType,
                Type immtblMapperFactoryType,
                Type mtblMapperFactoryType) =>
            {
                var nestedTypeGenericTypeDef = typesCache.GetGenericTypeDef(nestedType, true);

                var retKvp = new KeyValuePair<Type, NestedObjMapperFactoryTypesTuple>(
                    nestedTypeGenericTypeDef.Data,
                    new NestedObjMapperFactoryTypesTuple(
                        nestedTypeGenericTypeDef,
                        typesCache.GetGenericTypeDef(immtblMapperFactoryType, true),
                        typesCache.GetGenericTypeDef(mtblMapperFactoryType, true)));

                return retKvp;
            }).RdnlD();

            return mapperFactoryGenericTypesDictnr;
        }
    }
}
