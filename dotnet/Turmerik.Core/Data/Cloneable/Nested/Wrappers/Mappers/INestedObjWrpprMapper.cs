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
    public interface INestedObjWrpprMapper<TOpts, TWrppr>
        where TOpts : INestedObjMapOpts<TWrppr>
        where TWrppr : INestedObjWrpprCore
    {
        TWrppr GetTrgPropValue(TOpts opts);
    }

    public interface INestedObjWrpprMapper<TOpts> : INestedObjWrpprMapper<TOpts, INestedObjWrppr>
        where TOpts : INestedObjMapOpts<INestedObjWrppr>
    {
    }

    public interface INestedObjWrpprMapper : INestedObjWrpprMapper<INestedObjMapOpts>
    {
    }

    public interface INestedObjWrpprMapper<TOpts, TWrppr, TObj, TImmtbl, TMtbl> : INestedObjWrpprMapper<TOpts, TWrppr>
        where TOpts : INestedObjMapOpts<TWrppr, TObj, TImmtbl, TMtbl>
        where TWrppr : INestedObjWrppr<TObj, TImmtbl, TMtbl>
        where TImmtbl : TObj
        where TMtbl : TObj
    {
    }

    public interface INestedObjWrpprMapper<TOpts, TObj, TImmtbl, TMtbl> : INestedObjWrpprMapper<TOpts, INestedObjWrppr<TObj, TImmtbl, TMtbl>, TObj, TImmtbl, TMtbl>
        where TOpts : INestedObjMapOpts<INestedObjWrppr<TObj, TImmtbl, TMtbl>, TObj, TImmtbl, TMtbl>
        where TImmtbl : TObj
        where TMtbl : TObj
    {
    }

    public interface INestedObjWrpprMapper<TObj, TImmtbl, TMtbl> : INestedObjWrpprMapper<INestedObjMapOpts<TObj, TImmtbl, TMtbl>, TObj, TImmtbl, TMtbl>
        where TImmtbl : TObj
        where TMtbl : TObj
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

    public interface INestedObjMapOpts<TWrppr, TObj, TImmtbl, TMtbl> : INestedObjMapOpts<TWrppr>
        where TWrppr : INestedObjWrpprCore<TObj, TImmtbl, TMtbl>
        where TImmtbl : TObj
        where TMtbl : TObj
    {
    }

    public interface INestedObjMapOpts<TObj, TImmtbl, TMtbl> : INestedObjMapOpts<INestedObjWrppr<TObj, TImmtbl, TMtbl>>, INestedObjMapOpts<INestedObjWrppr<TObj, TImmtbl, TMtbl>, TObj, TImmtbl, TMtbl>
        where TImmtbl : TObj
        where TMtbl : TObj
    {
    }

    public interface INestedObjMapOpts : INestedObjMapOpts<INestedObjWrppr>
    {
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

    public class NestedObjMapOptsImmtbl : NestedObjMapOptsCoreImmtblBase, INestedObjMapOpts
    {
        public NestedObjMapOptsImmtbl(INestedObjMapOpts src) : base(src)
        {
            SrcPropValue = src.SrcPropValue;
        }

        public INestedObjWrppr SrcPropValue { get; }
    }

    public class NestedObjMapOptsMtbl : NestedObjMapOptsCoreMtblBase, INestedObjMapOpts
    {
        public NestedObjMapOptsMtbl()
        {
        }

        public NestedObjMapOptsMtbl(INestedObjMapOpts src) : base(src)
        {
            SrcPropValue = src.SrcPropValue;
        }

        public INestedObjWrppr SrcPropValue { get; set; }
    }

    public class NestedObjMapOptsImmtbl<TObj, TImmtbl, TMtbl> : NestedObjMapOptsImmtbl<INestedObjWrppr<TObj, TImmtbl, TMtbl>>, INestedObjMapOpts<TObj, TImmtbl, TMtbl>
        where TImmtbl : TObj
        where TMtbl : TObj
    {
        public NestedObjMapOptsImmtbl(INestedObjMapOpts<INestedObjWrppr<TObj, TImmtbl, TMtbl>, TObj, TImmtbl, TMtbl> src) : base(src)
        {
        }
    }

    public class NestedObjMapOptsMtbl<TObj, TImmtbl, TMtbl> : NestedObjMapOptsMtbl<INestedObjWrppr<TObj, TImmtbl, TMtbl>>, INestedObjMapOpts<TObj, TImmtbl, TMtbl>
        where TImmtbl : TObj
        where TMtbl : TObj
    {
        public NestedObjMapOptsMtbl(INestedObjMapOpts<INestedObjWrppr<TObj, TImmtbl, TMtbl>, TObj, TImmtbl, TMtbl> src) : base(src)
        {
        }
    }

    public abstract class NestedObjWrpprMapperBase<TObj, TImmtbl, TMtbl> : INestedObjWrpprMapper<TObj, TImmtbl, TMtbl>
        where TImmtbl : TObj
        where TMtbl : TObj
    {
        public INestedObjWrppr<TObj, TImmtbl, TMtbl> GetTrgPropValue(INestedObjMapOpts<TObj, TImmtbl, TMtbl> opts)
        {
            throw new NotImplementedException();
        }
    }
}
