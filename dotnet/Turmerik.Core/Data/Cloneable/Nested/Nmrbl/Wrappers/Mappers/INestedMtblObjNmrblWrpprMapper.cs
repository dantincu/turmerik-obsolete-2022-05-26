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

    public class NestedMtblObjNmrblWrpprMapper<TObj, TImmtbl, TMtbl> : NestedObjNmrblWrpprMapperBase<TObj, TImmtbl, TMtbl>, INestedMtblObjNmrblWrpprMapper<TObj, TImmtbl, TMtbl>
        where TImmtbl : TObj
        where TMtbl : TObj
    {
    }
}
