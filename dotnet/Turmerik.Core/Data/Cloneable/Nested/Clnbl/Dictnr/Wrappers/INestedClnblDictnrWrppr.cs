using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Turmerik.Core.Data.Cloneable.Nested.Clnbl.Wrappers;
using Turmerik.Core.Data.Cloneable.Nested.Dictnr.Wrappers;

namespace Turmerik.Core.Data.Cloneable.Nested.Clnbl.Dictnr.Wrappers
{
    public interface INestedClnblDictnrWrppr : INestedObjDictnrWrppr
    {
    }

    public interface INestedClnblDictnrWrppr<TKey, TClnbl, TImmtbl, TMtbl> : INestedClnblWrpprCore<INestedClnblDictnr<TKey, TClnbl>, INestedClnblRdnlDictnr<TKey, TClnbl, TImmtbl>, INestedClnblEdtblDictnr<TKey, TClnbl, TMtbl>>, INestedClnblDictnrWrppr
        where TClnbl : ICloneableObject
        where TImmtbl : TClnbl
        where TMtbl : TClnbl
    {
    }

    public class NestedClnblDictnrWrppr<TKey, TClnbl, TImmtbl, TMtbl> : INestedClnblDictnrWrppr<TKey, TClnbl, TImmtbl, TMtbl>
        where TClnbl : ICloneableObject
        where TImmtbl : TClnbl
        where TMtbl : TClnbl
    {
        public NestedClnblDictnrWrppr(
            INestedClnblRdnlDictnr<TKey, TClnbl, TImmtbl> immtblWrppr,
            INestedClnblEdtblDictnr<TKey, TClnbl, TMtbl> mtblWrppr)
        {
            ImmtblWrppr = immtblWrppr;
            MtblWrppr = mtblWrppr;

            ObjWrpprCore = new NestedClnblDictnr<TKey, TClnbl>(mtblWrppr?.Mtbl.ToDictionary(
                kvp => kvp.Key, kvp => kvp.Value as INestedObj<TClnbl>) ?? immtblWrppr?.Immtbl.ToDictionary(
                    kvp => kvp.Key, kvp => kvp.Value as INestedObj<TClnbl>));
        }

        public INestedClnblRdnlDictnr<TKey, TClnbl, TImmtbl> ImmtblWrppr { get; }
        public INestedClnblEdtblDictnr<TKey, TClnbl, TMtbl> MtblWrppr { get; }
        protected INestedClnblDictnr<TKey, TClnbl> ObjWrpprCore { get; }

        public object GetObjWrppr() => ObjWrpprCore;
        public object GetImmtblWrppr() => ImmtblWrppr;
        public object GetMtblWrppr() => MtblWrppr;
    }
}
