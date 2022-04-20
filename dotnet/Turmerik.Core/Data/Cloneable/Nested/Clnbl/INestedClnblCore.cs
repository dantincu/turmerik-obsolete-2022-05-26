using System;
using System.Collections.Generic;
using System.Text;

namespace Turmerik.Core.Data.Cloneable.Nested.Clnbl
{
    public interface INestedClnblCore : INestedObjCore
    {
    }

    public interface INestedClnblCore<TClnbl> : INestedObjCore<TClnbl>, INestedClnblCore
    {
    }

    public interface INestedImmtblClnblCore<TClnbl, TImmtbl> : INestedImmtblObjCore<TClnbl, TImmtbl>, INestedClnblCore<TClnbl>
    {
    }

    public interface INestedMtblClnblCore<TClnbl, TMtbl> : INestedMtblObjCore<TClnbl, TMtbl>, INestedClnblCore<TClnbl>
    {
    }

    public abstract class NestedClnblCoreBase<TClnbl> : INestedClnblCore<TClnbl>
    {
        protected TClnbl ObjCore { get; set; }
        public TClnbl GetObj() => ObjCore;
    }

    public abstract class NestedImmtblClnblCoreBase<TClnbl, TImmtbl> : NestedClnblCoreBase<TClnbl>, INestedImmtblClnblCore<TClnbl, TImmtbl>
    {
        public TImmtbl Immtbl => ImmtblCore;
        protected TImmtbl ImmtblCore { get; set; }
    }

    public abstract class NestedMtblClnblCoreBase<TClnbl, TMtbl> : NestedClnblCoreBase<TClnbl>, INestedMtblClnblCore<TClnbl, TMtbl>
    {
        public TMtbl Mtbl => MtblCore;
        protected TMtbl MtblCore { get; set; }
        public virtual void SetMtbl(TMtbl mtbl) => MtblCore = mtbl;
    }
}
