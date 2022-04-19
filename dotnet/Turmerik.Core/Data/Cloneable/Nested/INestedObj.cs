using System;
using System.Collections.Generic;
using System.Text;

namespace Turmerik.Core.Data.Cloneable.Nested
{
    public interface INestedObj<TObj> : INestedObjCore<TObj>
    {
    }

    public interface INestedImmtblObj<TObj, TImmtbl> : INestedObj<TObj>, INestedImmtblObjCore<TObj, TImmtbl>
        where TImmtbl : TObj
    {
    }

    public interface INestedMtblObj<TObj, TMtbl> : INestedObj<TObj>, INestedMtblObjCore<TObj, TMtbl>
        where TMtbl : TObj
    {
    }

    public class NestedImmtblObj<TObj, TImmtbl> : NestedImmtblObjCoreBase<TObj, TImmtbl>, INestedImmtblObj<TObj, TImmtbl>
        where TImmtbl : TObj
    {
        public NestedImmtblObj(TImmtbl immtbl)
        {
            ObjCore = immtbl;
        }
    }

    public class NestedMtblObj<TObj, TMtbl> : NestedMtblObjCoreBase<TObj, TMtbl>, INestedMtblObj<TObj, TMtbl>
        where TMtbl : TObj
    {
        public NestedMtblObj(TMtbl mtbl)
        {
            SetMtbl(mtbl);
        }

        public NestedMtblObj()
        {
        }

        public override void SetMtbl(TMtbl mtbl)
        {
            base.SetMtbl(mtbl);
            ObjCore = mtbl;
        }
    }
}
