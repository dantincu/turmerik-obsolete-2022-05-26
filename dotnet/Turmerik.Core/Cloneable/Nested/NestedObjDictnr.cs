using System;
using System.Collections.Generic;
using System.Text;
using System.Collections.ObjectModel;

namespace Turmerik.Core.Cloneable.Nested
{
    public interface INestedObjDictnr : INestedObj
    {
    }

    public interface INestedObjDictnr<TKey, TObj> : INestedObj<ReadOnlyDictionary<TKey, TObj>, Dictionary<TKey, TObj>>, INestedObjDictnr
    {
    }

    public class NestedObjDictnr<TKey, TObj> : NestedObjBase<ReadOnlyDictionary<TKey, TObj>, Dictionary<TKey, TObj>>, INestedObjDictnr<TKey, TObj>
    {
        public NestedObjDictnr()
        {
        }

        public NestedObjDictnr(ReadOnlyDictionary<TKey, TObj> immtbl) : base(immtbl)
        {
        }

        public NestedObjDictnr(ReadOnlyDictionary<TKey, TObj> immtbl, Dictionary<TKey, TObj> mtbl) : base(immtbl, mtbl)
        {
        }
    }
}
