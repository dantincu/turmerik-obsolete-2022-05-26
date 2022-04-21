using System;
using System.Collections.Generic;
using System.Text;
using Turmerik.Core.Helpers;

namespace Turmerik.Core.Data.Cloneable.Nested.Wrappers
{
    public interface INestedObjWrpprCore
    {
        object GetObj();
        object GetImmtbl();
        object GetMtbl();
    }

    public interface INestedObjWrpprCore<TNestedObj> : INestedObjWrpprCore
    {
    }

    public interface INestedObjWrpprCore<TNestedObj, TNestedImmtbl, TNestedMtbl> : INestedObjWrpprCore<TNestedObj>
    {
        TNestedImmtbl Immtbl { get; }
        TNestedMtbl Mtbl { get; }
    }
}
