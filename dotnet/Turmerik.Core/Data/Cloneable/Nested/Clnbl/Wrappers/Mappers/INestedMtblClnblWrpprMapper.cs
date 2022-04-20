using System;
using System.Collections.Generic;
using System.Text;
using Turmerik.Core.Data.Cloneable.Nested.Wrappers.Mappers;

namespace Turmerik.Core.Data.Cloneable.Nested.Clnbl.Wrappers.Mappers
{
    public interface INestedMtblClnblWrpprMapper<TClnbl, TImmtbl, TMtbl> : INestedClnblWrpprMapper<TClnbl, TImmtbl, TMtbl>
        where TClnbl : ICloneableObject
        where TImmtbl : TClnbl
        where TMtbl : TClnbl
    {
    }

    public class NestedMtblClnblWrpprMapper<TClnbl, TImmtbl, TMtbl> : NestedClnblWrpprMapperBase<TClnbl, TImmtbl, TMtbl>, INestedMtblClnblWrpprMapper<TClnbl, TImmtbl, TMtbl>
        where TClnbl : ICloneableObject
        where TImmtbl : TClnbl
        where TMtbl : TClnbl
    {
    }
}
