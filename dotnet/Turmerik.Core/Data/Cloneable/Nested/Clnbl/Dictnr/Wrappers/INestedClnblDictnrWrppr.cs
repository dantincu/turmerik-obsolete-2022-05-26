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
            Immtbl = immtblWrppr;
            Mtbl = mtblWrppr;

            if (mtblWrppr != null)
            {
                ObjWrpprCore = new NestedClnblDictnr<TKey, TClnbl>(
                    mtblWrppr.Mtbl.ToDictionary(
                        kvp => kvp.Key, kvp => (INestedClnbl<TClnbl>)new NestedClnbl<TClnbl>(
                            kvp.Value.Mtbl)));
            }
            else if (immtblWrppr != null)
            {
                ObjWrpprCore = new NestedClnblDictnr<TKey, TClnbl>(
                    immtblWrppr.Immtbl.ToDictionary(
                        kvp => kvp.Key, kvp => (INestedClnbl<TClnbl>)new NestedClnbl<TClnbl>(
                            kvp.Value.Immtbl)));
            }
        }

        public INestedClnblRdnlDictnr<TKey, TClnbl, TImmtbl> Immtbl { get; }
        public INestedClnblEdtblDictnr<TKey, TClnbl, TMtbl> Mtbl { get; }
        protected INestedClnblDictnr<TKey, TClnbl> ObjWrpprCore { get; }

        public object GetObj() => ObjWrpprCore;
        public object GetImmtbl() => Immtbl;
        public object GetMtbl() => Mtbl;
    }
}
