using System;
using System.Collections.Generic;
using System.Text;

namespace Turmerik.Core.Data.Cloneable.Nested.Clnbl
{
    public interface INestedClnbl<TClnbl> : INestedClnblCore<TClnbl>
        where TClnbl : ICloneableObject
    {
    }

    public interface INestedImmtblClnbl<TClnbl, TImmtbl> : INestedImmtblObj<TClnbl, TImmtbl>, INestedClnbl<TClnbl>, INestedImmtblClnblCore<TClnbl, TImmtbl>
        where TClnbl : ICloneableObject
        where TImmtbl : TClnbl
    {
    }

    public interface INestedMtblClnbl<TClnbl, TMtbl> : INestedMtblObj<TClnbl, TMtbl>, INestedClnbl<TClnbl>, INestedMtblClnblCore<TClnbl, TMtbl>
        where TClnbl : ICloneableObject
        where TMtbl : TClnbl
    {
    }

    public class NestedClnbl<TClnbl> : NestedObj<TClnbl>, INestedClnbl<TClnbl>
        where TClnbl : ICloneableObject
    {
        public NestedClnbl(TClnbl obj) : base(obj)
        {
        }
    }

    public class NestedImmtblClnbl<TClnbl, TImmtbl> : NestedImmtblObj<TClnbl, TImmtbl>, INestedImmtblClnbl<TClnbl, TImmtbl>
        where TClnbl : ICloneableObject
        where TImmtbl : TClnbl
    {
        public NestedImmtblClnbl(TImmtbl immtbl) : base(immtbl)
        {
        }
    }

    public class NestedMtblClnbl<TClnbl, TMtbl> : NestedMtblObj<TClnbl, TMtbl>, INestedMtblClnbl<TClnbl, TMtbl>
        where TClnbl : ICloneableObject
        where TMtbl : TClnbl
    {
        public NestedMtblClnbl(TMtbl immtbl) : base(immtbl)
        {
        }
    }
}
