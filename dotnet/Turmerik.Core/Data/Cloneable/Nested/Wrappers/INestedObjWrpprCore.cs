using System;
using System.Collections.Generic;
using System.Text;
using Turmerik.Core.Helpers;

namespace Turmerik.Core.Data.Cloneable.Nested.Wrappers
{
    public interface INestedObjWrpprCore
    {
        object GetObjWrppr();
        object GetImmtblWrppr();
        object GetMtblWrppr();
    }

    public interface INestedObjWrpprCore<TNestedObj, TNestedImmtbl, TNestedMtbl> : INestedObjWrpprCore
    {
        TNestedImmtbl ImmtblWrppr { get; }
        TNestedMtbl MtblWrppr { get; }
    }
}
