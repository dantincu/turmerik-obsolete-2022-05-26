using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace Turmerik.Core.Cloneable.Nested
{
    public interface INestedObjNmrbl : INestedObj
    {
    }

    public interface INestedObjNmrbl<TObj> : INestedObj<ReadOnlyCollection<TObj>, List<TObj>>, INestedObjNmrbl
    {
    }

    public class NestedObjNmrbl<TObj> : NestedObjBase<ReadOnlyCollection<TObj>, List<TObj>>, INestedObjNmrbl<TObj>
    {
        public NestedObjNmrbl()
        {
        }

        public NestedObjNmrbl(ReadOnlyCollection<TObj> immtbl) : base(immtbl)
        {
        }

        public NestedObjNmrbl(ReadOnlyCollection<TObj> immtbl, List<TObj> mtbl) : base(immtbl, mtbl)
        {
        }
    }
}
