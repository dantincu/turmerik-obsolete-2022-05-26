using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

namespace Turmerik.Core.Data.Cloneable.Nested
{
    public interface INestedObjCore
    {
    }

    public interface INestedObjCore<TObj> : INestedObjCore
    {
        TObj GetObj();
    }

    public interface INestedImmtblObjCore<TObj, TImmtbl> : INestedObjCore<TObj>
    {
        TImmtbl Immtbl { get; }
    }

    public interface INestedMtblObjCore<TObj, TMtbl> : INestedObjCore<TObj>
    {
        TMtbl Mtbl { get; }
        void SetMtbl(TMtbl mtbl);
    }

    public abstract class NestedObjCoreBase<TObj> : INestedObjCore<TObj>
    {
        protected TObj ObjCore { get; set; }
        public TObj GetObj() => ObjCore;
    }

    public abstract class NestedImmtblObjCoreBase<TObj, TImmtbl> : NestedObjCoreBase<TObj>, INestedImmtblObjCore<TObj, TImmtbl>
    {
        public TImmtbl Immtbl => ImmtblCore;
        protected TImmtbl ImmtblCore { get; set; }
    }

    public abstract class NestedMtblObjCoreBase<TClnbl, TMtbl> : NestedObjCoreBase<TClnbl>, INestedMtblObjCore<TClnbl, TMtbl>
    {
        public TMtbl Mtbl => MtblCore;
        protected TMtbl MtblCore { get; set; }
        public virtual void SetMtbl(TMtbl mtbl) => MtblCore = mtbl;
    }
}
