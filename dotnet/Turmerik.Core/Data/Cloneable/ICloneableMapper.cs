using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using Turmerik.Core.Data.Cloneable.Nested.Wrappers;
using Turmerik.Core.Data.Cloneable.Nested.Wrappers.Mappers;
using Turmerik.Core.Helpers;
using Turmerik.Core.Infrastucture;
using Turmerik.Core.Reflection.Wrappers;

namespace Turmerik.Core.Data.Cloneable
{
    public interface ICloneableMapper
    {
        void MapTarget(IObjMapOpts opts);
    }

    public class CloneableMapper : ICloneableMapper
    {
        private readonly ITypesStaticDataCache typesCache;

        public CloneableMapper(ITypesStaticDataCache typesCache)
        {
            this.typesCache = typesCache ?? throw new ArgumentNullException(nameof(typesCache));
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
                    
                    if (typeof(INestedObjWrpprCore).IsAssignableFrom(trgPropType))
                    {
                        trgPropValue = GetNestedClonableWrapperTrgPropValue(
                            srcPropValue,
                            srcPropType,
                            trgPropType);
                    }
                    else
                    {
                        trgPropValue = srcPropValue;
                    }

                    opts.PropValSetter(trgProp.Data, trgPropValue);
                }
            }
        }

        private object GetNestedClonableWrapperTrgPropValue(
            object srcPropValue,
            Type srcPropType,
            Type trgPropType)
        {
            throw new NotImplementedException();
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
