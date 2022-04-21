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

    public interface INestedClnblWrppr<TClnbl> : INestedClnblWrppr<TClnbl, TClnbl, TClnbl>
        where TClnbl : ICloneableObject
    {
    }

    public class NestedClnblWrppr<TClnbl, TImmtbl, TMtbl> : INestedClnblWrppr<TClnbl, TImmtbl, TMtbl>
        where TClnbl : ICloneableObject
        where TImmtbl : TClnbl
        where TMtbl : TClnbl
    {
        public NestedClnblWrppr(TImmtbl immtblWrppr, TMtbl mtblWrppr)
        {
            Immtbl = immtblWrppr;
            Mtbl = mtblWrppr;

            ObjWrpprCore = mtblWrppr != null ? (TClnbl)mtblWrppr : immtblWrppr;
        }

        public TImmtbl Immtbl { get; }
        public TMtbl Mtbl { get; }
        protected TClnbl ObjWrpprCore { get; }

        public object GetObj() => ObjWrpprCore;
        public object GetImmtbl() => Immtbl;
        public object GetMtbl() => Mtbl;
    }

    public class NestedClnblWrppr<TClnbl> : NestedClnblWrppr<TClnbl, TClnbl, TClnbl>, INestedClnblWrppr<TClnbl>
        where TClnbl : ICloneableObject
    {
        public NestedClnblWrppr(TClnbl immtblWrppr, TClnbl mtblWrppr) : base(immtblWrppr, mtblWrppr)
        {
        }
    }
}
