using System;
using System.Collections.Generic;
using System.Text;

namespace Turmerik.Core.Cloneable.Nested.Clnbl
{
    public interface INestedClnbl : INestedObj
    {
    }

    public interface INestedClnbl<TClnbl, TImmtbl, TMtbl> : INestedObj<TImmtbl, TMtbl>, INestedClnbl
        where TClnbl : ICloneableObject
        where TImmtbl : TClnbl
        where TMtbl : TClnbl
    {
    }

    public class NestedClnbl<TClnbl, TImmtbl, TMtbl> : NestedObjBase<TImmtbl, TMtbl>, INestedClnbl<TClnbl, TImmtbl, TMtbl>
        where TClnbl : ICloneableObject
        where TImmtbl : TClnbl
        where TMtbl : TClnbl
    {
        public NestedClnbl()
        {
        }

        public NestedClnbl(TImmtbl immtbl) : base(immtbl)
        {
        }

        public NestedClnbl(TImmtbl immtbl, TMtbl mtbl) : base(immtbl, mtbl)
        {
        }
    }
}
