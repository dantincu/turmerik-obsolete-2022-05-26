using System;
using System.Collections.Generic;
using System.Text;
using Turmerik.Core.Data.Cloneable.Nested.Wrappers;
using Turmerik.Core.Data.Cloneable.Nested.Wrappers.Mappers;
using Turmerik.Core.Infrastucture;

namespace Turmerik.Core.Data.Cloneable.Nested.Nmrbl.Wrappers.Mappers
{
    public interface INestedMtblObjNmrblWrpprMapper<TObj, TImmtbl, TMtbl> : INestedObjNmrblWrpprMapper<TObj, TImmtbl, TMtbl>
        where TImmtbl : TObj
        where TMtbl : TObj
    {
    }

    public class NestedMtblObjNmrblWrpprMapper<TObj, TImmtbl, TMtbl> : ComponentBase, INestedMtblObjNmrblWrpprMapper<TObj, TImmtbl, TMtbl>
        where TImmtbl : TObj
        where TMtbl : TObj
    {
        public NestedMtblObjNmrblWrpprMapper(IServiceProvider services) : base(services)
        {
        }

        public INestedObjNmrblWrppr<TObj, TImmtbl, TMtbl> GetTrgPropValue(INestedObjNmrblMapOpts<TObj, TImmtbl, TMtbl> opts)
        {
            throw new NotImplementedException();
        }
    }
}
