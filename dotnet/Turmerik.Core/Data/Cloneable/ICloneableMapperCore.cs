using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Turmerik.Core.Data.Cloneable.Nested.Wrappers;
using Turmerik.Core.Data.Cloneable.Nested.Wrappers.Mappers;
using Turmerik.Core.Infrastucture;
using Turmerik.Core.Reflection.Wrappers;

namespace Turmerik.Core.Data.Cloneable
{
    public interface ICloneableMapperCore
    {
        void MapTarget(ObjMapOptsImmtbl opts);
    }

    public abstract class CloneableMapperCoreBase : ComponentBase, ICloneableMapperCore
    {
        public CloneableMapperCoreBase(IServiceProvider services) : base(services)
        {
        }

        public void MapTarget(ObjMapOptsImmtbl opts)
        {
            foreach (var trgProp in opts.TrgProps)
            {
                var srcProp = opts.SrcProps.SingleOrDefault(
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

                    trgProp.Data.SetValue(opts.TrgObj, trgPropValue);
                }
            }
        }

        protected abstract INestedObjWrpprMapper GetNestedObjWrpprMapper(INestedObjMapOpts opts);

        private object GetNestedClonableWrapperTrgPropValue(
            object srcPropValue,
            Type srcPropType,
            Type trgPropType)
        {
            var optsMtbl = new NestedObjMapOptsMtbl
            {
                SrcPropType = srcPropType,
                TrgPropType = trgPropType,
                SrcPropValue = (INestedObjWrppr)srcPropValue
            };

            var opts = new NestedObjMapOptsImmtbl(optsMtbl);
            var mapper = GetNestedObjWrpprMapper(opts);

            object trgPropValue = mapper.GetTrgPropValue(opts);
            return trgPropValue;
        }
    }

    public interface IObjMapOpts
    {
        IReadOnlyCollection<PropertyWrapper> TrgProps { get; }
        IReadOnlyCollection<PropertyWrapper> SrcProps { get; }
        object TrgObj { get; }
        object SrcObj { get; }
    }

    public class ObjMapOptsImmtbl : IObjMapOpts
    {
        public IReadOnlyCollection<PropertyWrapper> TrgProps { get; }
        public IReadOnlyCollection<PropertyWrapper> SrcProps { get; }
        public object TrgObj { get; }
        public object SrcObj { get; }

        public ObjMapOptsImmtbl(IObjMapOpts src)
        {
            TrgProps = src.TrgProps;
            SrcProps = src.SrcProps;
            TrgObj = src.TrgObj;
            SrcObj = src.SrcObj;
        }
    }

    public class ObjMapOptsMtbl : IObjMapOpts
    {
        public ObjMapOptsMtbl()
        {
        }

        public ObjMapOptsMtbl(IObjMapOpts src)
        {
            TrgProps = src.TrgProps;
            SrcProps = src.SrcProps;
            TrgObj = src.TrgObj;
            SrcObj = src.SrcObj;
        }

        public IReadOnlyCollection<PropertyWrapper> TrgProps { get; set; }
        public IReadOnlyCollection<PropertyWrapper> SrcProps { get; set; }
        public object TrgObj { get; set; }
        public object SrcObj { get; set; }
    }
}
