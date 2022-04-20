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
            ImmtblWrppr = immtblWrppr;
            MtblWrppr = mtblWrppr;

            ObjWrpprCore = new NestedClnblNmrbl<TClnbl>(
                mtblWrppr?.Mtbl.Cast<INestedClnbl<TClnbl>>(
                    ) ?? immtblWrppr?.Immtbl.Cast<INestedClnbl<TClnbl>>());
        }

        public INestedImmtblClnblClctn<TClnbl, TImmtbl> ImmtblWrppr { get; }
        public INestedMtblClnblList<TClnbl, TMtbl> MtblWrppr { get; }
        protected INestedClnblNmrbl<TClnbl> ObjWrpprCore { get; }

        public object GetObjWrppr() => ObjWrpprCore;
        public object GetImmtblWrppr() => ImmtblWrppr;
        public object GetMtblWrppr() => MtblWrppr;
    }
}
