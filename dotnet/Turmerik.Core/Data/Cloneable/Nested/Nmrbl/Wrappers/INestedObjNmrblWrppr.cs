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

    public interface INestedObjNmrblWrppr<TObj> : INestedObjWrpprCore<INestedObjNmrbl<TObj>, INestedImmtblObjClctn<TObj>, INestedMtblObjList<TObj>>, INestedObjNmrblWrppr
    {
    }

    public class NestedObjNmrblWrppr<TObj> : INestedObjNmrblWrppr<TObj>
    {
        public NestedObjNmrblWrppr(INestedImmtblObjClctn<TObj> immtblWrppr, INestedMtblObjList<TObj> mtblWrppr)
        {
            Immtbl = immtblWrppr;
            Mtbl = mtblWrppr;

            IEnumerable<INestedObj<TObj>> nmrbl = null;

            if (mtblWrppr != null)
            {
                nmrbl = mtblWrppr.GetObj();
            }
            else if (immtblWrppr != null)
            {
                nmrbl = immtblWrppr.GetObj();
            }

            if (nmrbl != null)
            {
                ObjWrpprCore = new NestedObjNmrbl<TObj>(nmrbl.Cast<INestedObj<TObj>>());
            }
        }

        public INestedImmtblObjClctn<TObj> Immtbl { get; }
        public INestedMtblObjList<TObj> Mtbl { get; }
        protected INestedObjNmrbl<TObj> ObjWrpprCore { get; }

        public object GetObj() => ObjWrpprCore;
        public object GetImmtbl() => Immtbl;
        public object GetMtbl() => Mtbl;
    }
}
