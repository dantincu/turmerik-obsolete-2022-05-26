using System;
using System.Collections.Generic;
using System.Text;
using Turmerik.Core.Data.Cloneable.Nested.Wrappers;

namespace Turmerik.Core.Data.Cloneable.Nested.Clnbl.Wrappers
{
    public interface INestedClnblWrppr : INestedObjWrppr, INestedClnblWrpprCore
    {
    }

    public interface INestedClnblWrppr<TClnbl, TImmtbl, TMtbl> : INestedClnblWrppr, INestedClnblWrpprCore<TClnbl, TImmtbl, TMtbl>
        where TClnbl : ICloneableObject
        where TImmtbl : TClnbl
        where TMtbl : TClnbl
    {
    }

    public class NestedClnblWrppr<TClnbl, TImmtbl, TMtbl> : INestedClnblWrppr<TClnbl, TImmtbl, TMtbl>
        where TClnbl : ICloneableObject
        where TImmtbl : TClnbl
        where TMtbl : TClnbl
    {
        public NestedClnblWrppr(TImmtbl immtblWrppr, TMtbl mtblWrppr)
        {
            ImmtblWrppr = immtblWrppr;
            MtblWrppr = mtblWrppr;

            ObjWrpprCore = mtblWrppr != null ? (TClnbl)mtblWrppr : immtblWrppr;
        }

        public TImmtbl ImmtblWrppr { get; }
        public TMtbl MtblWrppr { get; }
        protected TClnbl ObjWrpprCore { get; }

        public object GetObjWrppr() => ObjWrpprCore;
        public object GetImmtblWrppr() => ImmtblWrppr;
        public object GetMtblWrppr() => MtblWrppr;
    }
}
