using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Turmerik.Core.Data.Cloneable.Nested.Clnbl.Wrappers;
using Turmerik.Core.Data.Cloneable.Nested.Nmrbl.Wrappers;

namespace Turmerik.Core.Data.Cloneable.Nested.Clnbl.Nmrbl.Wrappers
{
    public interface INestedClnblNmrblWrppr : INestedObjNmrblWrppr
    {
    }

    public interface INestedClnblNmrblWrppr<TClnbl, TImmtbl, TMtbl> : INestedClnblWrpprCore<INestedClnblNmrbl<TClnbl>, INestedImmtblClnblClctn<TClnbl, TImmtbl>, INestedMtblClnblList<TClnbl, TMtbl>>, INestedClnblNmrblWrppr
        where TClnbl : ICloneableObject
        where TImmtbl : TClnbl
        where TMtbl : TClnbl
    {
    }

    public class NestedClnblNmrblWrppr<TClnbl, TImmtbl, TMtbl> : INestedClnblNmrblWrppr<TClnbl, TImmtbl, TMtbl>
        where TClnbl : ICloneableObject
        where TImmtbl : TClnbl
        where TMtbl : TClnbl
    {
        public NestedClnblNmrblWrppr(INestedImmtblClnblClctn<TClnbl, TImmtbl> immtblWrppr, INestedMtblClnblList<TClnbl, TMtbl> mtblWrppr)
        {
            Immtbl = immtblWrppr;
            Mtbl = mtblWrppr;

            if (mtblWrppr != null)
            {
                ObjWrpprCore = new NestedClnblNmrbl<TClnbl>(
                    mtblWrppr?.Mtbl?.Cast<INestedClnbl<TClnbl>>());
            }
            else if (immtblWrppr != null)
            {
                ObjWrpprCore = new NestedClnblNmrbl<TClnbl>(
                    immtblWrppr?.Immtbl?.Cast<INestedClnbl<TClnbl>>());
            }
        }

        public INestedImmtblClnblClctn<TClnbl, TImmtbl> Immtbl { get; }
        public INestedMtblClnblList<TClnbl, TMtbl> Mtbl { get; }
        protected INestedClnblNmrbl<TClnbl> ObjWrpprCore { get; }

        public object GetObj() => ObjWrpprCore;
        public object GetImmtbl() => Immtbl;
        public object GetMtbl() => Mtbl;
    }

    public class NestedClnblNmrblWrppr<TClnbl> : NestedClnblNmrblWrppr<TClnbl, TClnbl, TClnbl>
        where TClnbl : ICloneableObject
    {
        public NestedClnblNmrblWrppr(INestedImmtblClnblClctn<TClnbl, TClnbl> immtblWrppr, INestedMtblClnblList<TClnbl, TClnbl> mtblWrppr) : base(immtblWrppr, mtblWrppr)
        {
        }
    }
}
