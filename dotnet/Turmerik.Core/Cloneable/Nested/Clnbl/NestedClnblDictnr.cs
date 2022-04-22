using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace Turmerik.Core.Cloneable.Nested.Clnbl
{
    public interface INestedClnblDictnr : INestedClnbl
    {
    }

    public interface INestedClnblDictnr<TKey, TClnbl, TImmtbl, TMtbl> : INestedObj<ReadOnlyDictionary<TKey, TImmtbl>, Dictionary<TKey, TMtbl>>, INestedClnblDictnr
        where TClnbl : ICloneableObject
        where TImmtbl : TClnbl
        where TMtbl : TClnbl
    {
    }

    public class NestedClnblDictnr<TKey, TClnbl, TImmtbl, TMtbl> : NestedObjBase<ReadOnlyDictionary<TKey, TImmtbl>, Dictionary<TKey, TMtbl>>, INestedClnblDictnr<TKey, TClnbl, TImmtbl, TMtbl>
        where TClnbl : ICloneableObject
        where TImmtbl : TClnbl
        where TMtbl : TClnbl
    {
        public NestedClnblDictnr()
        {
        }

        public NestedClnblDictnr(ReadOnlyDictionary<TKey, TImmtbl> immtbl) : base(immtbl)
        {
        }

        public NestedClnblDictnr(ReadOnlyDictionary<TKey, TImmtbl> immtbl, Dictionary<TKey, TMtbl> mtbl) : base(immtbl, mtbl)
        {
        }
    }
}
