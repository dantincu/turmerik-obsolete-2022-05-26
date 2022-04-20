using System;
using System.Collections.Generic;
using System.Text;
using Turmerik.Core.Infrastucture;

namespace Turmerik.Core.Data.Cloneable.Nested.Wrappers.Mappers
{
    public interface INestedMtblObjWrpprMapper<TObj, TImmtbl, TMtbl> : INestedObjWrpprMapper<TObj, TImmtbl, TMtbl>
        where TImmtbl : TObj
        where TMtbl : TObj
    {
    }

    public class NestedMtblObjWrpprMapper<TObj, TImmtbl, TMtbl> : NestedObjWrpprMapperBase<TObj, TImmtbl, TMtbl>, INestedMtblObjWrpprMapper<TObj, TImmtbl, TMtbl>
        where TImmtbl : TObj
        where TMtbl : TObj
    {
    }
}
