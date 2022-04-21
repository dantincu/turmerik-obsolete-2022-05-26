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

    public interface INestedObjWrppr<TObj> : INestedObjWrppr<TObj, TObj, TObj>
    {
    }

    public class NestedObjWrppr<TObj, TImmtbl, TMtbl> : INestedObjWrppr<TObj, TImmtbl, TMtbl>
        where TImmtbl : TObj
        where TMtbl : TObj
    {
        public NestedObjWrppr(TImmtbl immtblWrppr, TMtbl mtblWrppr)
        {
            Immtbl = immtblWrppr;
            Mtbl = mtblWrppr;

            ObjWrpprCore = mtblWrppr != null ? (TObj)mtblWrppr : immtblWrppr;
        }

        public TImmtbl Immtbl { get; }
        public TMtbl Mtbl { get; }
        protected TObj ObjWrpprCore { get; }

        public object GetObj() => ObjWrpprCore;
        public object GetImmtbl() => Immtbl;
        public object GetMtbl() => Mtbl;
    }

    public class NestedObjWrppr<TObj> : NestedObjWrppr<TObj, TObj, TObj>, INestedObjWrppr<TObj>
    {
        public NestedObjWrppr(TObj immtblWrppr, TObj mtblWrppr) : base(immtblWrppr, mtblWrppr)
        {
        }
    }
}
