using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;
using Turmerik.Core.Data.Cloneable.Nested.Wrappers;
using Turmerik.Core.Data.Cloneable.Nested.Wrappers.Mappers;
using Turmerik.Core.Infrastucture;

namespace Turmerik.Core.Data.Cloneable.Nested.Nmrbl.Wrappers.Mappers
{
    public interface INestedObjNmrblWrpprMapper<TOpts, TWrppr, TObj, TImmtbl, TMtbl> : INestedObjWrpprMapper<TOpts, TWrppr>
        where TOpts : INestedObjNmrblMapOpts<TWrppr, TObj, TImmtbl, TMtbl>
        where TWrppr : INestedObjNmrblWrppr<TObj, TImmtbl, TMtbl>
        where TImmtbl : TObj
        where TMtbl : TObj
    {
    }

    public interface INestedObjNmrblWrpprMapper<TOpts, TObj, TImmtbl, TMtbl> : INestedObjNmrblWrpprMapper<TOpts, INestedObjNmrblWrppr<TObj, TImmtbl, TMtbl>, TObj, TImmtbl, TMtbl>
        where TOpts : INestedObjNmrblMapOpts<INestedObjNmrblWrppr<TObj, TImmtbl, TMtbl>, TObj, TImmtbl, TMtbl>
        where TImmtbl : TObj
        where TMtbl : TObj
    {
    }

    public interface INestedObjNmrblWrpprMapper<TObj, TImmtbl, TMtbl> : INestedObjNmrblWrpprMapper<INestedObjNmrblMapOpts<TObj, TImmtbl, TMtbl>, TObj, TImmtbl, TMtbl>
        where TImmtbl : TObj
        where TMtbl : TObj
    {
    }

    public interface INestedObjNmrblMapOpts<TWrppr, TObj, TImmtbl, TMtbl> : INestedObjMapOpts<TWrppr>
        where TWrppr : INestedObjNmrblWrppr<TObj, TImmtbl, TMtbl>
        where TImmtbl : TObj
        where TMtbl : TObj
    {
    }

    public interface INestedObjNmrblMapOpts<TObj, TImmtbl, TMtbl> : INestedObjNmrblMapOpts<INestedObjNmrblWrppr<TObj, TImmtbl, TMtbl>, TObj, TImmtbl, TMtbl>
        where TImmtbl : TObj
        where TMtbl : TObj
    {
    }

    public class NestedObjNmrblMapOptsImmtbl<TObj, TImmtbl, TMtbl> : NestedObjMapOptsImmtbl<INestedObjNmrblWrppr<TObj, TImmtbl, TMtbl>>, INestedObjNmrblMapOpts<TObj, TImmtbl, TMtbl>
        where TImmtbl : TObj
        where TMtbl : TObj
    {
        public NestedObjNmrblMapOptsImmtbl(INestedObjNmrblMapOpts<TObj, TImmtbl, TMtbl> src) : base(src)
        {
        }
    }

    public class NestedObjNmrblMapOptsMtbl<TObj, TImmtbl, TMtbl> : NestedObjMapOptsMtbl<INestedObjNmrblWrppr<TObj, TImmtbl, TMtbl>>, INestedObjNmrblMapOpts<TObj, TImmtbl, TMtbl>
        where TImmtbl : TObj
        where TMtbl : TObj
    {
        public NestedObjNmrblMapOptsMtbl()
        {
        }

        public NestedObjNmrblMapOptsMtbl(INestedObjNmrblMapOpts<TObj, TImmtbl, TMtbl> src) : base(src)
        {
        }
    }

    public abstract class NestedObjNmrblWrpprMapperBase<TObj, TImmtbl, TMtbl> : INestedObjNmrblWrpprMapper<TObj, TImmtbl, TMtbl>
        where TImmtbl : TObj
        where TMtbl : TObj
    {
        public INestedObjNmrblWrppr<TObj, TImmtbl, TMtbl> GetTrgPropValue(INestedObjNmrblMapOpts<TObj, TImmtbl, TMtbl> opts)
        {
            throw new NotImplementedException();
        }
    }
}
