using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace Turmerik.Core.Cloneable.Nested.Clnbl
{
    public interface INestedClnblNmrbl : INestedClnbl
    {
    }

    public interface INestedClnblNmrbl<TClnbl, TImmtbl, TMtbl> : INestedObj<ReadOnlyCollection<TImmtbl>, List<TMtbl>>, INestedClnblNmrbl
        where TClnbl : ICloneableObject
        where TImmtbl : TClnbl
        where TMtbl : TClnbl
    {
    }

    public class NestedClnblNmrbl<TClnbl, TImmtbl, TMtbl> : NestedObjBase<ReadOnlyCollection<TImmtbl>, List<TMtbl>>, INestedClnblNmrbl<TClnbl, TImmtbl, TMtbl>
        where TClnbl : ICloneableObject
        where TImmtbl : TClnbl
        where TMtbl : TClnbl
    {
        public NestedClnblNmrbl()
        {
        }

        public NestedClnblNmrbl(ReadOnlyCollection<TImmtbl> immtbl) : base(immtbl)
        {
        }

        public NestedClnblNmrbl(ReadOnlyCollection<TImmtbl> immtbl, List<TMtbl> mtbl) : base(immtbl, mtbl)
        {
        }
    }
}
