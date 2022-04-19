using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;
using Turmerik.Core.Infrastucture;

namespace Turmerik.Core.Data.Cloneable.Nested.Wrappers.Mappers
{
    public interface INestedObjWrpprMapperCore
    {
        INestedObjWrpprCore GetTrgPropValue(INestedObjMapOpts opts);
    }

    public interface INestedObjMapOpts
    {
        INestedObjWrpprCore SrcPropValue { get; }
        Type SrcPropType { get; }
        Type TrgPropType { get; }
    }

    public class NestedObjMapOptsImmtbl : INestedObjMapOpts
    {
        public NestedObjMapOptsImmtbl(INestedObjMapOpts src)
        {
            SrcPropValue = src.SrcPropValue;
            SrcPropType = src.SrcPropType;
            TrgPropType = src.TrgPropType;
        }

        public INestedObjWrpprCore SrcPropValue { get; }
        public Type SrcPropType { get; }
        public Type TrgPropType { get; }
    }

    public class NestedObjMapOptsMtbl : INestedObjMapOpts
    {
        public NestedObjMapOptsMtbl()
        {
        }

        public NestedObjMapOptsMtbl(INestedObjMapOpts src)
        {
            SrcPropValue = src.SrcPropValue;
            SrcPropType = src.SrcPropType;
            TrgPropType = src.TrgPropType;
        }

        public INestedObjWrpprCore SrcPropValue { get; set; }
        public Type SrcPropType { get; set; }
        public Type TrgPropType { get; set; }
    }
}
