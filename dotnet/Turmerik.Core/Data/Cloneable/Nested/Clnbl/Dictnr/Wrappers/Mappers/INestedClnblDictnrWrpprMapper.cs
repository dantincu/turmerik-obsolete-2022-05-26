using System;
using System.Collections.Generic;
using System.Text;
using Turmerik.Core.Data.Cloneable.Nested.Wrappers.Mappers;
using Turmerik.Core.Infrastucture;

namespace Turmerik.Core.Data.Cloneable.Nested.Clnbl.Dictnr.Wrappers.Mappers
{
    public abstract class NestedClnblDictnrWrpprMapperBase<TKey, TClnbl, TImmtbl, TMtbl> : ComponentBase, INestedClnblDictnrWrpprMapper<TKey, TClnbl, TImmtbl, TMtbl>
        where TClnbl : ICloneableObject
        where TImmtbl : TClnbl
        where TMtbl : TClnbl
    {
        public NestedClnblDictnrWrpprMapperBase(IServiceProvider services) : base(services)
        {
        }

        public INestedClnblDictnrWrppr<TKey, TClnbl, TImmtbl, TMtbl> GetTrgPropValue(INestedClnblDictnrMapOpts<TKey, TClnbl, TImmtbl, TMtbl> opts)
        {
            throw new NotImplementedException();
        }
    }
}
