using System;
using System.Collections.Generic;
using System.Text;
using Turmerik.Core.Data.Cloneable.Nested.Clnbl.Wrappers;
using Turmerik.Core.Data.Cloneable.Nested.Wrappers.Mappers;
using Turmerik.Core.Infrastucture;

namespace Turmerik.Core.Data.Cloneable.Nested.Clnbl.Nmrbl.Wrappers.Mappers
{
    public abstract class NestedClnblWrpprMapperBase<TClnbl, TImmtbl, TMtbl> : ComponentBase, INestedClnblWrpprMapper<TClnbl, TImmtbl, TMtbl>
        where TClnbl : ICloneableObject
        where TImmtbl : TClnbl
        where TMtbl : TClnbl
    {
        public NestedClnblWrpprMapperBase(IServiceProvider services) : base(services)
        {
        }

        public INestedClnblWrppr<TClnbl, TImmtbl, TMtbl> GetTrgPropValue(INestedClnblMapOpts<TClnbl, TImmtbl, TMtbl> opts)
        {
            throw new NotImplementedException();
        }
    }
}
