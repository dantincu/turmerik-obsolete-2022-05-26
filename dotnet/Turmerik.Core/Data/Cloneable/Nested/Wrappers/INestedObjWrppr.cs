using System;
using System.Collections.Generic;
using System.Text;
using Turmerik.Core.Helpers;

namespace Turmerik.Core.Data.Cloneable.Nested.Wrappers
{
    public interface INestedObjWrppr : INestedObjWrpprCore
    {
    }

    public interface INestedObjWrppr<TObj, TImmtbl, TMtbl> : INestedObjWrpprCore<TObj, TImmtbl, TMtbl>, INestedObjWrppr
        where TImmtbl : TObj
        where TMtbl : TObj
    {
    }

    public class NestedObjWrppr<TObj, TImmtbl, TMtbl> : INestedObjWrppr<TObj, TImmtbl, TMtbl>
        where TImmtbl : TObj
        where TMtbl : TObj
    {
        public NestedObjWrppr(TImmtbl immtblWrppr, TMtbl mtblWrppr)
        {
            ImmtblWrppr = immtblWrppr;
            MtblWrppr = mtblWrppr;

            ObjWrpprCore = mtblWrppr != null ? (TObj)mtblWrppr : immtblWrppr;
        }

        public TImmtbl ImmtblWrppr { get; }
        public TMtbl MtblWrppr { get; }
        protected TObj ObjWrpprCore { get; }

        public object GetObjWrppr() => ObjWrpprCore;
        public object GetImmtblWrppr() => ImmtblWrppr;
        public object GetMtblWrppr() => MtblWrppr;
    }
}
