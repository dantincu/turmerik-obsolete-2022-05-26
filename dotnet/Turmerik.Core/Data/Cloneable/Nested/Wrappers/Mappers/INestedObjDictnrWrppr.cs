using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Turmerik.Core.Data.Cloneable.Nested.Wrappers.Mappers
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

            ObjWrppr = new NestedObjDictnr<TKey, TObj>(mtblWrppr?.Mtbl.ToDictionary(
                kvp => kvp.Key, kvp => kvp.Value as INestedObj<TObj>) ?? immtblWrppr?.Immtbl.ToDictionary(
                    kvp => kvp.Key, kvp => kvp.Value as INestedObj<TObj>));
        }

        public INestedObjDictnr<TKey, TObj> ObjWrppr { get; }
        public INestedObjRdnlDictnr<TKey, TObj, TImmtbl> ImmtblWrppr { get; }
        public INestedObjEdtblDictnr<TKey, TObj, TMtbl> MtblWrppr { get; }

        public object GetObjWrppr() => ObjWrppr;
        public object GetImmtblWrppr() => ImmtblWrppr;
        public object GetMtblWrppr() => MtblWrppr;
    }
}
