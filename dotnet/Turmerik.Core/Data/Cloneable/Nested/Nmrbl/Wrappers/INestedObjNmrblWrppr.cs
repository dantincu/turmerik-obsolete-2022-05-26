using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Turmerik.Core.Data.Cloneable.Nested.Wrappers;

namespace Turmerik.Core.Data.Cloneable.Nested.Nmrbl.Wrappers
{
    public interface INestedObjNmrblWrppr : INestedObjWrpprCore
    {
    }

    public interface INestedObjNmrblWrppr<TObj, TImmtbl, TMtbl> : INestedObjWrpprCore<INestedObjNmrbl<TObj>, INestedImmtblObjClctn<TObj, TImmtbl>, INestedMtblObjList<TObj, TMtbl>>, INestedObjNmrblWrppr
        where TImmtbl : TObj
        where TMtbl : TObj
    {
    }

    public class NestedObjNmrblWrppr<TObj, TImmtbl, TMtbl> : INestedObjNmrblWrppr<TObj, TImmtbl, TMtbl>
        where TImmtbl : TObj
        where TMtbl : TObj
    {
        public NestedObjNmrblWrppr(INestedImmtblObjClctn<TObj, TImmtbl> immtblWrppr, INestedMtblObjList<TObj, TMtbl> mtblWrppr)
        {
            ImmtblWrppr = immtblWrppr;
            MtblWrppr = mtblWrppr;

            ObjWrpprCore = new NestedObjNmrbl<TObj>(
                mtblWrppr?.Mtbl.Cast<INestedObj<TObj>>(
                    ) ?? immtblWrppr?.Immtbl.Cast<INestedObj<TObj>>());
        }

        public INestedImmtblObjClctn<TObj, TImmtbl> ImmtblWrppr { get; }
        public INestedMtblObjList<TObj, TMtbl> MtblWrppr { get; }
        protected INestedObjNmrbl<TObj> ObjWrpprCore { get; }

        public object GetObjWrppr() => ObjWrpprCore;
        public object GetImmtblWrppr() => ImmtblWrppr;
        public object GetMtblWrppr() => MtblWrppr;
    }
}
