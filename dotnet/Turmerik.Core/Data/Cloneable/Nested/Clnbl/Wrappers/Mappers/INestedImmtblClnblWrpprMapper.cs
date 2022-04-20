using System;
using System.Collections.Generic;
using System.Text;
using Turmerik.Core.Data.Cloneable.Nested.Wrappers.Mappers;

namespace Turmerik.Core.Data.Cloneable.Nested.Clnbl.Wrappers.Mappers
{
    public interface INestedImmtblClnblWrpprMapper<TClnbl, TImmtbl, TMtbl> : INestedClnblWrpprMapper<TClnbl, TImmtbl, TMtbl>
        where TClnbl : ICloneableObject
        where TImmtbl : TClnbl
        where TMtbl : TClnbl
    {
    }

    public class NestedImmtblClnblWrpprMapper<TClnbl, TImmtbl, TMtbl> : NestedClnblWrpprMapperBase<TClnbl, TImmtbl, TMtbl>, INestedImmtblClnblWrpprMapper<TClnbl, TImmtbl, TMtbl>
        where TClnbl : ICloneableObject
        where TImmtbl : TClnbl
        where TMtbl : TClnbl
    {
    }
}
