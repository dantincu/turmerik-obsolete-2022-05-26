using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;
using Turmerik.Core.Data.Cloneable.Nested.Wrappers;
using Turmerik.Core.Data.Cloneable.Nested.Wrappers.Mappers;
using Turmerik.Core.Infrastucture;

namespace Turmerik.Core.Data.Cloneable.Nested.Nmrbl.Wrappers.Mappers
{
    public interface INestedObjNmrblWrpprMapper : INestedObjWrpprMapperCore
    {
    }

    public interface INestedObjNmrblWrpprMapper<TOpts, TWrppr, TObj> : INestedObjWrpprMapper<TOpts, TWrppr>, INestedObjNmrblWrpprMapper
        where TOpts : INestedObjMapOpts<TWrppr>
        where TWrppr : INestedObjNmrblWrppr<TObj>
    {
    }

    public interface INestedObjNmrblWrpprMapper<TObj> : INestedObjNmrblWrpprMapper<INestedObjMapOpts<INestedObjNmrblWrppr<TObj>>, INestedObjNmrblWrppr<TObj>, TObj>
    {
    }

    public abstract class NestedImmtblObjNmrblWrpprMapperBase<TObj> : INestedObjNmrblWrpprMapper<TObj>
    {
        public INestedObjWrpprCore GetTrgPropValue(INestedObjMapOptsCore opts)
        {
            var options = (INestedObjMapOpts<INestedObjNmrblWrppr<TObj>>)opts;
            var wrppr = GetTrgPropValue(options);

            return wrppr;
        }

        public abstract INestedObjNmrblWrppr<TObj> GetTrgPropValue(INestedObjMapOpts<INestedObjNmrblWrppr<TObj>> opts);
    }
}
