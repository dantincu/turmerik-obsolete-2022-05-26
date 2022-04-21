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
}
