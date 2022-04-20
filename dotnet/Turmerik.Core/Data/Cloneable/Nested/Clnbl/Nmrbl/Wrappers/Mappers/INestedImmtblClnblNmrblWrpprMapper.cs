using System;
using System.Collections.Generic;
using System.Text;
using Turmerik.Core.Data.Cloneable.Nested.Wrappers.Mappers;

namespace Turmerik.Core.Data.Cloneable.Nested.Clnbl.Nmrbl.Wrappers.Mappers
{
    public interface INestedImmtblClnblNmrblWrpprMapper<TClnbl, TImmtbl, TMtbl> : INestedClnblNmrblWrpprMapper<TClnbl, TImmtbl, TMtbl>
        where TClnbl : ICloneableObject
        where TImmtbl : TClnbl
        where TMtbl : TClnbl
    {
    }

    public class NestedImmtblClnblNmrblWrpprMapper<TClnbl, TImmtbl, TMtbl> : NestedClnblNmrblWrpprMapperBase<TClnbl, TImmtbl, TMtbl>, INestedImmtblClnblNmrblWrpprMapper<TClnbl, TImmtbl, TMtbl>
        where TClnbl : ICloneableObject
        where TImmtbl : TClnbl
        where TMtbl : TClnbl
    {
    }
}
