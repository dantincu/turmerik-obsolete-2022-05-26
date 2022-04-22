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
            IReadOnlyCollection<PropertyWrapper> trgProps;
            IReadOnlyCollection<PropertyWrapper> srcProps;

            var trgType = typesCache.Get(opts.TrgType ?? opts.TrgObj.GetType());
            var srcType = typesCache.Get(opts.SrcType ?? opts.SrcObj.GetType());

            if (opts.TrgIsMtbl)
            {
                trgProps = trgType.InstPubGetPubSetProps.Value;
                srcProps = srcType.InstPubGetPubSetProps.Value;
            }
            else
            {
                trgProps = trgType.InstPubGetPubOrFamSetProps.Value;
                srcProps = srcType.InstPubGetPubOrFamSetProps.Value;
            }

            foreach (var trgProp in trgProps)
            {
                var srcProp = srcProps.SingleOrDefault(
                    prop => prop.Name == trgProp.Name);

                if (srcProp != null)
                {
                    Type srcPropType = srcProp.PropType.Value.Data;
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
            }
        }

        private INestedObj GetNestedClonableWrapperTrgPropValue(
            INestedObj srcPropValue,
            Type trgPropType,
            bool isMtbl)
        {
            var mapper = mainFactory.GetMapper(
                this,
                srcPropValue,
                trgPropType,
                isMtbl);

            var wrppr = mapper.GetObj(srcPropValue);
            return wrppr;
        }
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
