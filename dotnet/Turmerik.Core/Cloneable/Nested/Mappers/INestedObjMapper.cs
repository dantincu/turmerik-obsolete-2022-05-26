using System;
using System.Collections.Generic;
using System.Text;
using Turmerik.Core.Helpers;

namespace Turmerik.Core.Cloneable.Nested.Mappers
{
    public interface INestedObjMapper
    {
        INestedObj GetObj(INestedObj nested);
    }

    public interface INestedObjMapper<TNested> : INestedObjMapper
        where TNested : INestedObj
    {
        TNested GetObjInstn(TNested nested);
    }

    public abstract class NestedObjMapperBase<TNested, TImmtbl, TMtbl> : INestedObjMapper<TNested>
        where TNested : INestedObj<TImmtbl, TMtbl>
    {
        public INestedObj GetObj(INestedObj nested)
        {
            TNested nestedObj;

            if (nested != null)
            {
                nestedObj = (TNested)nested;
                nestedObj = MapObjCore(nestedObj);
            }
            else
            {
                nestedObj = default(TNested);
            }

            return nestedObj;
        }

        public TNested GetObjInstn(TNested nested)
        {
            TNested retNested;

            if (nested != null)
            {
                retNested = MapObjCore(nested);
            }
            else
            {
                retNested = default(TNested);
            }

            return retNested;
        }

        protected abstract TNested MapObjCore(TNested nested);
    }

    public abstract class NestedImmtblObjMapperBase<TNested, TImmtbl, TMtbl> : NestedObjMapperBase<TNested, TImmtbl, TMtbl>, INestedObjMapper<TNested>
        where TNested : INestedObj<TImmtbl, TMtbl>
    {
        protected override TNested MapObjCore(TNested nested)
        {
            var immtbl = nested.Immtbl;

            if (immtbl == null && nested.Mtbl != null)
            {
                immtbl = GetImmtbl(nested.Mtbl);
            }

            var retNested = GetNested(immtbl);
            return retNested;
        }

        protected abstract TImmtbl GetImmtbl(TMtbl mtbl);
        protected abstract TNested GetNested(TImmtbl immtbl);
    }

    public abstract class NestedMtblObjMapperBase<TNested, TImmtbl, TMtbl> : NestedObjMapperBase<TNested, TImmtbl, TMtbl>, INestedObjMapper<TNested>
        where TNested : INestedObj<TImmtbl, TMtbl>
    {
        protected override TNested MapObjCore(TNested nested)
        {
            var mtbl = nested.Mtbl;

            if (mtbl == null && nested.Immtbl != null)
            {
                mtbl = GetMtbl(nested.Immtbl);
            }

            var retNested = GetNested(mtbl);
            return retNested;
        }

        protected abstract TMtbl GetMtbl(TImmtbl immtbl);
        protected abstract TNested GetNested(TMtbl mtbl);
    }
}
