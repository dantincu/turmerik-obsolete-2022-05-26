using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Turmerik.Core.Data.Cloneable.Nested.Wrappers.Mappers
{
    public interface INestedObjNmrblWrppr : INestedObjWrpprCore
    {
    }

    public interface INestedObjNmrblWrppr<TObj, TImmtbl, TMtbl> : INestedObjWrpprCore<INestedObjNmrbl<TObj>, INestedImmtblObjColcnt<TObj, TImmtbl>, INestedMtblObjList<TObj, TMtbl>>, INestedObjNmrblWrppr
        where TImmtbl : TObj
        where TMtbl : TObj
    {
    }

    public class NestedObjNmrblWrppr<TObj, TImmtbl, TMtbl> : INestedObjNmrblWrppr<TObj, TImmtbl, TMtbl>
        where TImmtbl : TObj
        where TMtbl : TObj
    {
        public NestedObjNmrblWrppr(INestedImmtblObjColcnt<TObj, TImmtbl> immtblWrppr, INestedMtblObjList<TObj, TMtbl> mtblWrppr)
        {
            ImmtblWrppr = immtblWrppr;
            MtblWrppr = mtblWrppr;

            ObjWrppr = new NestedObjNmrbl<TObj>(
                mtblWrppr?.Mtbl.Cast<INestedObj<TObj>>(
                    ) ?? immtblWrppr?.Immtbl.Cast<INestedObj<TObj>>());
        }

        public INestedObjNmrbl<TObj> ObjWrppr { get; }
        public INestedImmtblObjColcnt<TObj, TImmtbl> ImmtblWrppr { get; }
        public INestedMtblObjList<TObj, TMtbl> MtblWrppr { get; }

        public object GetObjWrppr() => ObjWrppr;
        public object GetImmtblWrppr() => ImmtblWrppr;
        public object GetMtblWrppr() => MtblWrppr;
    }
}
