using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;
using Turmerik.Core.Data.Cloneable.Nested.Wrappers;
using Turmerik.Core.Data.Cloneable.Nested.Wrappers.Mappers;
using Turmerik.Core.Infrastucture;

namespace Turmerik.Core.Data.Cloneable.Nested.Dictnr.Wrappers.Mappers
{
    public interface INestedObjDictnrWrpprMapper<TOpts, TWrppr, TKey, TObj, TImmtbl, TMtbl> : INestedObjWrpprMapper<TOpts, TWrppr>
        where TOpts : INestedObjDictnrMapOpts<TWrppr, TKey, TObj, TImmtbl, TMtbl>
        where TWrppr : INestedObjDictnrWrppr<TKey, TObj, TImmtbl, TMtbl>
        where TImmtbl : TObj
        where TMtbl : TObj
    {
    }

    public interface INestedObjDictnrWrpprMapper<TOpts, TKey, TObj, TImmtbl, TMtbl> : INestedObjDictnrWrpprMapper<TOpts, INestedObjDictnrWrppr<TKey, TObj, TImmtbl, TMtbl>, TKey, TObj, TImmtbl, TMtbl>
        where TOpts : INestedObjDictnrMapOpts<TKey, TObj, TImmtbl, TMtbl>
        where TImmtbl : TObj
        where TMtbl : TObj
    {
    }

    public interface INestedObjDictnrWrpprMapper<TKey, TObj, TImmtbl, TMtbl> : INestedObjDictnrWrpprMapper<INestedObjDictnrMapOpts<TKey, TObj, TImmtbl, TMtbl>, TKey, TObj, TImmtbl, TMtbl>
        where TImmtbl : TObj
        where TMtbl : TObj
    {
    }

    public interface INestedObjDictnrMapOpts<TWrppr, TKey, TObj, TImmtbl, TMtbl> : INestedObjMapOpts<TWrppr>
        where TWrppr : INestedObjDictnrWrppr<TKey, TObj, TImmtbl, TMtbl>
        where TImmtbl : TObj
        where TMtbl : TObj
    {
    }

    public interface INestedObjDictnrMapOpts<TKey, TObj, TImmtbl, TMtbl> : INestedObjDictnrMapOpts<INestedObjDictnrWrppr<TKey, TObj, TImmtbl, TMtbl>, TKey, TObj, TImmtbl, TMtbl>
        where TImmtbl : TObj
        where TMtbl : TObj
    {
    }

    public class NestedObjDictnrMapOptsImmtbl<TKey, TObj, TImmtbl, TMtbl> : NestedObjMapOptsImmtbl<INestedObjDictnrWrppr<TKey, TObj, TImmtbl, TMtbl>>, INestedObjDictnrMapOpts<TKey, TObj, TImmtbl, TMtbl>
        where TImmtbl : TObj
        where TMtbl : TObj
    {
        public NestedObjDictnrMapOptsImmtbl(INestedObjDictnrMapOpts<TKey, TObj, TImmtbl, TMtbl> src) : base(src)
        {
        }
    }

    public class NestedObjDictnrMapOptsMtbl<TKey, TObj, TImmtbl, TMtbl> : NestedObjMapOptsMtbl<INestedObjDictnrWrppr<TKey, TObj, TImmtbl, TMtbl>>, INestedObjDictnrMapOpts<TKey, TObj, TImmtbl, TMtbl>
        where TImmtbl : TObj
        where TMtbl : TObj
    {
        public NestedObjDictnrMapOptsMtbl()
        {
        }

        public NestedObjDictnrMapOptsMtbl(INestedObjDictnrMapOpts<TKey, TObj, TImmtbl, TMtbl> src) : base(src)
        {
        }
    }

    public abstract class NestedObjDictnrWrpprMapperBase<TKey, TObj, TImmtbl, TMtbl> : INestedObjDictnrWrpprMapper<TKey, TObj, TImmtbl, TMtbl>
        where TImmtbl : TObj
        where TMtbl : TObj
    {
        public INestedObjDictnrWrppr<TKey, TObj, TImmtbl, TMtbl> GetTrgPropValue(INestedObjDictnrMapOpts<TKey, TObj, TImmtbl, TMtbl> opts)
        {
            throw new NotImplementedException();
        }
    }
}
