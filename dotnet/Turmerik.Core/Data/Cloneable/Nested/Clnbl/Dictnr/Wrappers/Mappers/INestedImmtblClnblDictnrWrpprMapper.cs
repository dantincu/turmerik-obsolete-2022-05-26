using System;
using System.Collections.Generic;
using System.Text;
using Turmerik.Core.Data.Cloneable.Nested.Wrappers.Mappers;

namespace Turmerik.Core.Data.Cloneable.Nested.Clnbl.Dictnr.Wrappers.Mappers
{
    public interface INestedImmtblClnblDictnrWrpprMapper<TKey, TClnbl, TImmtbl, TMtbl> : INestedClnblDictnrWrpprMapper<TKey, TClnbl, TImmtbl, TMtbl>
        where TClnbl : ICloneableObject
        where TImmtbl : TClnbl
        where TMtbl : TClnbl
    {
    }

    public class NestedImmtblClnblDictnrWrpprMapper<TKey, TClnbl, TImmtbl, TMtbl> : NestedClnblDictnrWrpprMapperBase<TKey, TClnbl, TImmtbl, TMtbl>, INestedImmtblClnblDictnrWrpprMapper<TKey, TClnbl, TImmtbl, TMtbl>
        where TClnbl : ICloneableObject
        where TImmtbl : TClnbl
        where TMtbl : TClnbl
    {
    }
}
