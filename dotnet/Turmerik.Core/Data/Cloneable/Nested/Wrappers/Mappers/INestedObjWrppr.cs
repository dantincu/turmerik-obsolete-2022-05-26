using System;
using System.Collections.Generic;
using System.Text;
using Turmerik.Core.Helpers;

namespace Turmerik.Core.Data.Cloneable.Nested.Wrappers.Mappers
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
        protected NestedObjWrppr(TImmtbl immtblWrppr, TMtbl mtblWrppr)
        {
            ImmtblWrppr = immtblWrppr;
            MtblWrppr = mtblWrppr;

            ObjWrppr = mtblWrppr != null ? (TObj)mtblWrppr : immtblWrppr;
        }

        public TImmtbl ImmtblWrppr { get; }
        public TMtbl MtblWrppr { get; }
        public TObj ObjWrppr { get; }

        public object GetObjWrppr() => ObjWrppr;
        public object GetImmtblWrppr() => ImmtblWrppr;
        public object GetMtblWrppr() => MtblWrppr;
    }
}
