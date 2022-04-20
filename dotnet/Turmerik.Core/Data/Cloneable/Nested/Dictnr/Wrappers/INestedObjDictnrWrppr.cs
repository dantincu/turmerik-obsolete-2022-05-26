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

    public interface INestedObjDictnrWrppr<TKey, TObj, TImmtbl, TMtbl> : INestedObjWrpprCore<INestedObjDictnr<TKey, TObj>, INestedObjRdnlDictnr<TKey, TObj, TImmtbl>, INestedObjEdtblDictnr<TKey, TObj, TMtbl>>, INestedObjDictnrWrppr
        where TImmtbl : TObj
        where TMtbl : TObj
    {
    }

    public class NestedObjDictnrWrppr<TKey, TObj, TImmtbl, TMtbl> : INestedObjDictnrWrppr<TKey, TObj, TImmtbl, TMtbl>
        where TImmtbl : TObj
        where TMtbl : TObj
    {
        public NestedObjDictnrWrppr(
            INestedObjRdnlDictnr<TKey, TObj, TImmtbl> immtblWrppr,
            INestedObjEdtblDictnr<TKey, TObj, TMtbl> mtblWrppr)
        {
            ImmtblWrppr = immtblWrppr;
            MtblWrppr = mtblWrppr;

            ObjWrpprCore = new NestedObjDictnr<TKey, TObj>(mtblWrppr?.Mtbl.ToDictionary(
                kvp => kvp.Key, kvp => kvp.Value as INestedObj<TObj>) ?? immtblWrppr?.Immtbl.ToDictionary(
                    kvp => kvp.Key, kvp => kvp.Value as INestedObj<TObj>));
        }

        public INestedObjRdnlDictnr<TKey, TObj, TImmtbl> ImmtblWrppr { get; }
        public INestedObjEdtblDictnr<TKey, TObj, TMtbl> MtblWrppr { get; }
        protected INestedObjDictnr<TKey, TObj> ObjWrpprCore { get; }

        public object GetObjWrppr() => ObjWrpprCore;
        public object GetImmtblWrppr() => ImmtblWrppr;
        public object GetMtblWrppr() => MtblWrppr;
    }
}
