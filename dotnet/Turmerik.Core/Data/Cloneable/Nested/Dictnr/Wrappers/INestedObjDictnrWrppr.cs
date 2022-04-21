using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Turmerik.Core.Data.Cloneable.Nested.Wrappers;

namespace Turmerik.Core.Data.Cloneable.Nested.Dictnr.Wrappers
{
    public interface INestedObjDictnrWrppr : INestedObjWrpprCore
    {
    }

    public interface INestedObjDictnrWrppr<TKey, TObj> : INestedObjWrpprCore<INestedObjDictnr<TKey, TObj>, INestedObjRdnlDictnr<TKey, TObj>, INestedObjEdtblDictnr<TKey, TObj>>, INestedObjDictnrWrppr
    {
    }

    public class NestedObjDictnrWrppr<TKey, TObj> : INestedObjDictnrWrppr<TKey, TObj>
    {
        public NestedObjDictnrWrppr(
            INestedObjRdnlDictnr<TKey, TObj> immtblWrppr,
            INestedObjEdtblDictnr<TKey, TObj> mtblWrppr)
        {
            Immtbl = immtblWrppr;
            Mtbl = mtblWrppr;

            if (mtblWrppr?.Mtbl != null)
            {
                ObjWrpprCore = new NestedObjEdtblDictnr<TKey, TObj>(mtblWrppr.Mtbl);
            }
            else if (immtblWrppr?.Immtbl != null)
            {
                ObjWrpprCore = new NestedObjRdnlDictnr<TKey, TObj>(immtblWrppr.Immtbl);
            }
        }

        public INestedObjRdnlDictnr<TKey, TObj> Immtbl { get; }
        public INestedObjEdtblDictnr<TKey, TObj> Mtbl { get; }
        protected INestedObjDictnr<TKey, TObj> ObjWrpprCore { get; }

        public object GetObj() => ObjWrpprCore;
        public object GetImmtbl() => Immtbl;
        public object GetMtbl() => Mtbl;
    }
}
