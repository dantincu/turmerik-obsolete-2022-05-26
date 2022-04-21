using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;
using Turmerik.Core.Data.Cloneable.Nested.Clnbl.Dictnr.Wrappers;
using Turmerik.Core.Data.Cloneable.Nested.Clnbl.Dictnr.Wrappers.Mappers;
using Turmerik.Core.Data.Cloneable.Nested.Clnbl.Nmrbl.Wrappers;
using Turmerik.Core.Data.Cloneable.Nested.Clnbl.Nmrbl.Wrappers.Mappers;
using Turmerik.Core.Data.Cloneable.Nested.Clnbl.Wrappers;
using Turmerik.Core.Data.Cloneable.Nested.Clnbl.Wrappers.Mappers;
using Turmerik.Core.Data.Cloneable.Nested.Dictnr.Wrappers;
using Turmerik.Core.Data.Cloneable.Nested.Dictnr.Wrappers.Mappers;
using Turmerik.Core.Data.Cloneable.Nested.Nmrbl.Wrappers;
using Turmerik.Core.Data.Cloneable.Nested.Nmrbl.Wrappers.Mappers;
using Turmerik.Core.Infrastucture;

namespace Turmerik.Core.Data.Cloneable.Nested.Wrappers.Mappers
{
    public interface INestedObjWrpprMapperCore
    {
        INestedObjWrpprCore GetTrgPropValue(INestedObjMapOptsCore opts);
    }

    public interface INestedObjWrpprMapper<TOpts, TWrppr> : INestedObjWrpprMapperCore
        where TOpts : INestedObjMapOpts<TWrppr>
        where TWrppr : INestedObjWrpprCore
    {
        TWrppr GetTrgPropValue(TOpts opts);
    }

    public interface INestedObjWrpprMapper<TOpts, TWrppr, TObj> : INestedObjWrpprMapper<TOpts, TWrppr>
        where TOpts : INestedObjMapOpts<TWrppr>
        where TWrppr : INestedObjWrpprCore<TObj>
    {
    }

    public interface INestedObjWrpprMapper<TObj> : INestedObjWrpprMapper<INestedObjMapOpts<INestedObjWrppr<TObj>>, INestedObjWrppr<TObj>>
    {
    }

    public interface INestedObjMapOptsCore
    {
        Type SrcPropType { get; }
        Type TrgPropType { get; }
    }

    public interface INestedObjMapOpts<TWrppr> : INestedObjMapOptsCore
        where TWrppr : INestedObjWrpprCore
    {
        TWrppr SrcPropValue { get; }
    }

    public abstract class NestedObjMapOptsCoreImmtblBase : INestedObjMapOptsCore
    {
        protected NestedObjMapOptsCoreImmtblBase(INestedObjMapOptsCore src)
        {
            SrcPropType = src.SrcPropType;
            TrgPropType = src.TrgPropType;
        }

        public Type SrcPropType { get; }
        public Type TrgPropType { get; }
    }

    public abstract class NestedObjMapOptsCoreMtblBase : INestedObjMapOptsCore
    {
        protected NestedObjMapOptsCoreMtblBase()
        {
        }

        protected NestedObjMapOptsCoreMtblBase(INestedObjMapOptsCore src)
        {
            SrcPropType = src.SrcPropType;
            TrgPropType = src.TrgPropType;
        }

        public Type SrcPropType { get; set; }
        public Type TrgPropType { get; set; }
    }

    public class NestedObjMapOptsImmtbl<TWrppr> : NestedObjMapOptsCoreImmtblBase, INestedObjMapOpts<TWrppr>
        where TWrppr : INestedObjWrpprCore
    {
        public NestedObjMapOptsImmtbl(INestedObjMapOpts<TWrppr> src) : base(src)
        {
            SrcPropValue = src.SrcPropValue;
        }

        public TWrppr SrcPropValue { get; }
    }

    public class NestedObjMapOptsMtbl<TWrppr> : NestedObjMapOptsCoreMtblBase, INestedObjMapOpts<TWrppr>
        where TWrppr : INestedObjWrpprCore
    {
        public NestedObjMapOptsMtbl()
        {
        }

        public NestedObjMapOptsMtbl(INestedObjMapOpts<TWrppr> src) : base(src)
        {
            SrcPropValue = src.SrcPropValue;
        }

        public TWrppr SrcPropValue { get; set; }
    }

    public abstract class NestedObjWrpprMapperBase<TObj> : INestedObjWrpprMapper<TObj>
    {
        public abstract INestedObjWrppr<TObj> GetTrgPropValue(INestedObjMapOpts<INestedObjWrppr<TObj>> opts);

        public INestedObjWrpprCore GetTrgPropValue(INestedObjMapOptsCore opts)
        {
            var options = (INestedObjMapOpts<INestedObjWrppr<TObj>>)opts;
            var retVal = GetTrgPropValue(options);

            return retVal;
        }
    }
}
