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
        public TObj GetObj() => ObjCore;
        protected TObj ObjCore { get; set; }
    }

    public abstract class NestedImmtblObjCoreBase<TObj, TImmtbl> : NestedObjCoreBase<TObj>, INestedImmtblObjCore<TObj, TImmtbl>
    {
        public TImmtbl Immtbl => ImmtblCore;
        protected TImmtbl ImmtblCore { get; set; }
    }

    public abstract class NestedMtblObjCoreBase<TObj, TMtbl> : NestedObjCoreBase<TObj>, INestedMtblObjCore<TObj, TMtbl>
    {
        public TMtbl Mtbl => MtblCore;
        protected TMtbl MtblCore { get; set; }

        public abstract void SetMtbl(TMtbl mtbl);
    }
}
