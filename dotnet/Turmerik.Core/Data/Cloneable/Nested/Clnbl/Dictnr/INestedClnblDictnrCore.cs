using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using Turmerik.Core.Data.Cloneable.Nested.Clnbl.Nmrbl;
using Turmerik.Core.Data.Cloneable.Nested.Dictnr;
using Turmerik.Core.Helpers;

namespace Turmerik.Core.Data.Cloneable.Nested.Clnbl.Dictnr
{
    public interface INestedClnblDictnrCore<TClnbl, TKvp, TDictnr> : INestedObjDictnrCore<TClnbl, TKvp, TDictnr>, INestedClnblNmrblCore<TClnbl, TKvp, TDictnr>
        where TClnbl : ICloneableObject
        where TDictnr : IEnumerable<TKvp>
    {
    }

    public interface INestedClnblDictnr<TKey, TClnbl, TNested, TDictnr> : INestedClnblDictnrCore<TClnbl, KeyValuePair<TKey, TNested>, TDictnr>
        where TClnbl : ICloneableObject
        where TNested : INestedClnblCore<TClnbl>
        where TDictnr : IEnumerable<KeyValuePair<TKey, TNested>>
    {
    }

    public interface INestedClnblDictnr<TKey, TClnbl> : INestedClnblDictnr<TKey, TClnbl, INestedClnbl<TClnbl>, ReadOnlyDictionary<TKey, INestedClnbl<TClnbl>>>
        where TClnbl : ICloneableObject
    {
    }

    public interface INestedClnblRdnlDictnr<TKey, TClnbl, TImmtbl> : INestedClnblDictnr<TKey, TClnbl, INestedImmtblClnbl<TClnbl, TImmtbl>, ReadOnlyDictionary<TKey, INestedImmtblClnbl<TClnbl, TImmtbl>>>, INestedImmtblClnblCore<ReadOnlyDictionary<TKey, INestedImmtblClnbl<TClnbl, TImmtbl>>, ReadOnlyDictionary<TKey, INestedImmtblClnbl<TClnbl, TImmtbl>>>
        where TClnbl : ICloneableObject
        where TImmtbl : class, TClnbl
    {
    }

    public interface INestedClnblEdtblDictnr<TKey, TClnbl, TMtbl> : INestedClnblDictnr<TKey, TClnbl, INestedMtblClnbl<TClnbl, TMtbl>, Dictionary<TKey, INestedMtblClnbl<TClnbl, TMtbl>>>, INestedMtblClnblCore<Dictionary<TKey, INestedMtblClnbl<TClnbl, TMtbl>>, Dictionary<TKey, INestedMtblClnbl<TClnbl, TMtbl>>>
        where TClnbl : ICloneableObject
        where TMtbl : class, TClnbl
    {
    }

    public class NestedClnblDictnr<TKey, TClnbl> : NestedClnblCoreBase<ReadOnlyDictionary<TKey, INestedClnbl<TClnbl>>>, INestedClnblDictnr<TKey, TClnbl>
        where TClnbl : ICloneableObject
    {
        public NestedClnblDictnr(IEnumerable<KeyValuePair<TKey, INestedClnbl<TClnbl>>> kvpNmrbl)
        {
        }

        public NestedClnblDictnr(ReadOnlyDictionary<TKey, INestedClnbl<TClnbl>> dictnr)
        {
            ObjCore = dictnr;
        }
    }

    public class NestedClnblRdnlDictnr<TKey, TClnbl, TImmtbl> : NestedImmtblClnblCoreBase<ReadOnlyDictionary<TKey, INestedImmtblClnbl<TClnbl, TImmtbl>>, ReadOnlyDictionary<TKey, INestedImmtblClnbl<TClnbl, TImmtbl>>>, INestedClnblRdnlDictnr<TKey, TClnbl, TImmtbl>
        where TClnbl : ICloneableObject
        where TImmtbl : class, TClnbl
    {
        public NestedClnblRdnlDictnr(IEnumerable<KeyValuePair<TKey, INestedImmtblClnbl<TClnbl, TImmtbl>>> kvpNmrbl) : this(kvpNmrbl?.RdnlD())
        {
        }

        public NestedClnblRdnlDictnr(ReadOnlyDictionary<TKey, INestedImmtblClnbl<TClnbl, TImmtbl>> dictnr)
        {
            ImmtblCore = dictnr;
            ObjCore = dictnr;
        }
    }

    public class NestedClnblEdtblDictnr<TKey, TClnbl, TMtbl> : NestedMtblClnblCoreBase<Dictionary<TKey, INestedMtblClnbl<TClnbl, TMtbl>>, Dictionary<TKey, INestedMtblClnbl<TClnbl, TMtbl>>>, INestedClnblEdtblDictnr<TKey, TClnbl, TMtbl>
        where TClnbl : ICloneableObject
        where TMtbl : class, TClnbl
    {
        public NestedClnblEdtblDictnr()
        {
        }

        public NestedClnblEdtblDictnr(IEnumerable<KeyValuePair<TKey, INestedMtblClnbl<TClnbl, TMtbl>>> kvpNmrbl) : this(kvpNmrbl?.Dictnr())
        {
        }

        public NestedClnblEdtblDictnr(Dictionary<TKey, INestedMtblClnbl<TClnbl, TMtbl>> dictnr)
        {
            SetMtbl(dictnr);
        }
    }
}
