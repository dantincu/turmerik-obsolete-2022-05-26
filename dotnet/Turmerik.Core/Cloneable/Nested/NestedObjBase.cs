using System;
using System.Collections.Generic;
using System.Text;

namespace Turmerik.Core.Cloneable.Nested
{
    public interface INestedObj
    {
    }

    public interface INestedObj<TImmtbl, TMtbl> : INestedObj
    {
        TImmtbl Immtbl { get; }
        TMtbl Mtbl { get; set; }
    }

    public class NestedObjBase<TImmtbl, TMtbl> : INestedObj<TImmtbl, TMtbl>
    {
        public NestedObjBase()
        {
        }

        public NestedObjBase(TImmtbl immtbl)
        {
            Immtbl = immtbl;
        }

        public NestedObjBase(TImmtbl immtbl, TMtbl mtbl) : this(immtbl)
        {
            Mtbl = mtbl;
        }

        public TImmtbl Immtbl { get; }
        public TMtbl Mtbl { get; set; }
    }
}
