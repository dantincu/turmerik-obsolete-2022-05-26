using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;
using Turmerik.Core.Data.Cloneable.Nested.Wrappers;
using Turmerik.Core.Data.Cloneable.Nested.Wrappers.Mappers;
using Turmerik.Core.Infrastucture;

namespace Turmerik.Core.Data.Cloneable.Nested.Dictnr.Wrappers.Mappers
{
    public interface INestedObjDictnrWrpprMapper : INestedObjWrpprMapperCore
    {
    }

    public interface INestedObjDictnrWrpprMapper<TOpts, TWrppr, TKey, TObj> : INestedObjWrpprMapper<TWrppr>, INestedObjDictnrWrpprMapper
        where TOpts : INestedObjMapOpts<TWrppr>
        where TWrppr : INestedObjDictnrWrppr<TKey, TObj>
    {
    }

    public interface INestedObjDictnrWrpprMapper<TKey, TObj> : INestedObjDictnrWrpprMapper<INestedObjMapOpts<INestedObjDictnrWrppr<TKey, TObj>>, INestedObjDictnrWrppr<TKey, TObj>, TKey, TObj>
    {
    }

    public abstract class NestedObjDictnrWrpprMapperBase<TKey, TObj> : INestedObjDictnrWrpprMapper<TKey, TObj>
    {
        public INestedObjWrpprCore GetTrgPropValue(INestedObjMapOptsCore opts)
        {
            var options = (INestedObjMapOpts<INestedObjWrppr<INestedObjDictnrWrppr<TKey, TObj>>>)opts;
            var wrppr = GetTrgPropValue(options);

            return wrppr;
        }

        public abstract INestedObjWrppr<INestedObjDictnrWrppr<TKey, TObj>> GetTrgPropValue(INestedObjMapOpts<INestedObjWrppr<INestedObjDictnrWrppr<TKey, TObj>>> opts);
    }
}
