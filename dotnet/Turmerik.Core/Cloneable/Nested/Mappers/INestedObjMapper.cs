using System;
using System.Collections.Generic;
using System.Text;
using Turmerik.Core.Helpers;
using Turmerik.Core.Reflection;

namespace Turmerik.Core.Cloneable.Nested.Mappers
{
    public interface INestedObjMapper
    {
        INestedObj GetObj(INestedObj nested, Type trgType);
    }

    public interface INestedObjMapper<TNested> : INestedObjMapper
        where TNested : INestedObj
    {
        TNested GetObjInstn(TNested nested, Type trgType);
    }

    public abstract class NestedObjMapperBase<TNested, TImmtbl, TMtbl> : INestedObjMapper<TNested>
        where TNested : INestedObj<TImmtbl, TMtbl>
    {
        public INestedObj GetObj(INestedObj nested, Type trgType)
        {
            TNested nestedObj;

            if (nested != null)
            {
                nestedObj = (TNested)nested;
                nestedObj = GetObjCore(nestedObj, trgType);
            }
            else
            {
                nestedObj = default(TNested);
            }

            return nestedObj;
        }

        public TNested GetObjInstn(TNested nested, Type trgType)
        {
            TNested retNested;

            if (nested != null)
            {
                retNested = GetObjCore(nested, trgType);
            }
            else
            {
                retNested = default(TNested);
            }

            return retNested;
        }

        protected abstract TNested GetObjCore(TNested nested, Type trgType);
    }

    public abstract class NestedImmtblObjMapperBase<TNested, TImmtbl, TMtbl> : NestedObjMapperBase<TNested, TImmtbl, TMtbl>, INestedObjMapper<TNested>
        where TNested : INestedObj<TImmtbl, TMtbl>
    {
        protected abstract TImmtbl GetImmtbl(TMtbl mtbl);

        protected override TNested GetObjCore(TNested nested, Type trgType)
        {
            var immtbl = nested.Immtbl;

            if (immtbl == null && nested.Mtbl != null)
            {
                immtbl = GetImmtbl(nested.Mtbl);
            }

            var retNested = GetNested(immtbl, trgType);
            return retNested;
        }

        protected TNested GetNested(TImmtbl immtbl, Type trgType) => trgType.Create<TNested>(immtbl);
    }

    public abstract class NestedMtblObjMapperBase<TNested, TImmtbl, TMtbl> : NestedObjMapperBase<TNested, TImmtbl, TMtbl>, INestedObjMapper<TNested>
        where TNested : INestedObj<TImmtbl, TMtbl>
    {
        protected abstract TMtbl GetMtbl(TImmtbl immtbl);

        protected override TNested GetObjCore(TNested nested, Type trgType)
        {
            var mtbl = nested.Mtbl;

            if (mtbl == null && nested.Immtbl != null)
            {
                mtbl = GetMtbl(nested.Immtbl);
            }

            var retNested = GetNested(mtbl, trgType);
            return retNested;
        }

        protected TNested GetNested(TMtbl mtbl, Type trgType) => trgType.Create<TNested>(null, mtbl);
    }
}
