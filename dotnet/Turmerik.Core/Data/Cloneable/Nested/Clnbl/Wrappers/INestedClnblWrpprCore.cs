using System;
using System.Collections.Generic;
using System.Text;
using Turmerik.Core.Data.Cloneable.Nested.Wrappers;

namespace Turmerik.Core.Data.Cloneable.Nested.Clnbl.Wrappers
{
    public interface INestedClnblWrpprCore : INestedObjWrpprCore
    {
    }

    public interface INestedClnblWrpprCore<TNestedClnbl, TNestedImmtbl, TNestedMtbl> : INestedObjWrpprCore<TNestedClnbl, TNestedImmtbl, TNestedMtbl>
    {
    }
}
