using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using Turmerik.Core.Cloneable.Nested;
using Turmerik.Core.Cloneable.Nested.Clnbl.Mappers;
using Turmerik.Core.Cloneable.Nested.Mappers;
using Turmerik.Core.Helpers;
using Turmerik.Core.Infrastucture;
using Turmerik.Core.Reflection.Wrappers;

namespace Turmerik.Core.Cloneable
{
    public interface ICloneableMapper
    {
        void MapTarget(IObjMapOpts opts);
    }

    public class CloneableMapper : ICloneableMapper
    {
        private readonly ITypesStaticDataCache typesCache;
        private readonly INestedObjMapperMainFactory mainFactory;

        public CloneableMapper(
            ITypesStaticDataCache typesCache,
            INestedObjMapperMainFactory mainFactory)
        {
            this.typesCache = typesCache ?? throw new ArgumentNullException(nameof(typesCache));
            this.mainFactory = mainFactory ?? throw new ArgumentNullException(nameof(mainFactory));
        }

        public void MapTarget(IObjMapOpts opts)
        {
            (var trgProps, var srcProps) = GetPropWrpprs(opts);

            foreach (var trgProp in trgProps)
            {
                var srcProp = srcProps.SingleOrDefault(
                    prop => prop.Name == trgProp.Name);

                if (srcProp != null)
                {
                    MapTargetCore(opts, trgProp, srcProp);
                }
            }
        }

        private void MapTargetCore(
            IObjMapOpts opts,
            PropertyWrapper trgProp,
            PropertyWrapper srcProp)
        {
            Type trgPropType = trgProp.PropType.Value.Data;
            object srcPropValue = srcProp.Data.GetValue(opts.SrcObj);

            object trgPropValue;

            if (typeof(INestedObj).IsAssignableFrom(trgPropType))
            {
                trgPropValue = GetNestedClonableWrapperTrgPropValue(
                    (INestedObj)srcPropValue,
                    trgPropType,
                    opts.TrgIsMtbl);
            }
            else
            {
                trgPropValue = srcPropValue;
            }

            opts.PropValSetter(trgProp.Data, trgPropValue);
        }

        private Tuple<IReadOnlyCollection<PropertyWrapper>, IReadOnlyCollection<PropertyWrapper>> GetPropWrpprs(IObjMapOpts opts)
        {
            var trgProps = GetPropWrpprs(
                opts.TrgType, opts.TrgObj, opts.TrgIsMtbl);

            var srcProps = GetPropWrpprs(
                opts.SrcType, opts.SrcObj, null);

            return new Tuple<IReadOnlyCollection<PropertyWrapper>, IReadOnlyCollection<PropertyWrapper>>(
                trgProps, srcProps);
        }

        private IReadOnlyCollection<PropertyWrapper> GetPropWrpprs(
            Type type,
            object obj,
            bool? trgIsMtbl)
        {
            var typeWrppr = typesCache.Get(type ?? obj.GetType());
            IReadOnlyCollection<PropertyWrapper> props;

            if (trgIsMtbl.HasValue)
            {
                if (trgIsMtbl.Value)
                {
                    props = typeWrppr.InstPubGetPubSetProps.Value;
                }
                else
                {
                    props = typeWrppr.InstPubGetPubOrFamSetProps.Value;
                }
            }
            else
            {
                props = typeWrppr.InstPubGetProps.Value;
            }

            return props;
        }

        private INestedObj GetNestedClonableWrapperTrgPropValue(
            INestedObj srcPropValue,
            Type trgPropType,
            bool isMtbl)
        {
            var mapper = mainFactory.GetMapper(
                this,
                trgPropType,
                isMtbl);

            // var trgType = GetTrgType(trgPropType, isMtbl);
            var wrppr = mapper.GetObj(srcPropValue, trgPropType);

            return wrppr;
        }

        /* private Type GetTrgType(
            Type trgPropType,
            bool isMtbl)
        {
            var typeAttrs = typesCache.Get(trgPropType).Attrs.Value;
            CloneableBaseAttribute attr;

            if (isMtbl)
            {
                attr = (CloneableMtblAttribute)typeAttrs.Get(
                    typeof(CloneableMtblAttribute)).Single().Data;
            }
            else
            {
                attr = (CloneableImmtblAttribute)typeAttrs.Get(
                    typeof(CloneableImmtblAttribute)).Single().Data;
            }

            Type retType = attr.Type;

            if (retType == null)
            {
                if (attr.ClnblTypeFactoryType != null)
                {
                    var factory = (CloneableTypeFactoryBase)Activator.CreateInstance(
                        attr.ClnblTypeFactoryType);

                    retType = factory.GetType();
                }
                else
                {
                    throw new InvalidOperationException(
                        $"Either cloneable type or cloneable type factory type must be provided");
                }
            }

            return retType;
        } */
    }

    public interface IObjMapOpts
    {
        Type TrgType { get; }
        Type SrcType { get; }
        object TrgObj { get; }
        object SrcObj { get; }
        Action<PropertyInfo, object> PropValSetter { get; }
        bool TrgIsMtbl { get; }
    }

    public class ObjMapOptsImmtbl : IObjMapOpts
    {
        public Type TrgType { get; }
        public Type SrcType { get; }
        public object TrgObj { get; }
        public object SrcObj { get; }
        public Action<PropertyInfo, object> PropValSetter { get; }
        public bool TrgIsMtbl { get; }

        public ObjMapOptsImmtbl(IObjMapOpts src)
        {
            TrgType = src.TrgType;
            SrcType = src.SrcType;
            TrgObj = src.TrgObj;
            SrcObj = src.SrcObj;
            PropValSetter = src.PropValSetter;
            TrgIsMtbl = src.TrgIsMtbl;
        }
    }

    public class ObjMapOptsMtbl : IObjMapOpts
    {
        public ObjMapOptsMtbl()
        {
        }

        public ObjMapOptsMtbl(IObjMapOpts src)
        {
            TrgType = src.TrgType;
            SrcType = src.SrcType;
            TrgObj = src.TrgObj;
            SrcObj = src.SrcObj;
            PropValSetter = src.PropValSetter;
            TrgIsMtbl = src.TrgIsMtbl;
        }

        public Type TrgType { get; set; }
        public Type SrcType { get; set; }
        public object TrgObj { get; set; }
        public object SrcObj { get; set; }
        public Action<PropertyInfo, object> PropValSetter { get; set; }
        public bool TrgIsMtbl { get; set; }
    }
}
