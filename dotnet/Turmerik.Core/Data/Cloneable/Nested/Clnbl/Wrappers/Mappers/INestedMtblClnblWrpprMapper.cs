using System;
using System.Collections.Generic;
using System.Text;
using Turmerik.Core.Data.Cloneable.Nested.Wrappers.Mappers;
using Turmerik.Core.Helpers;

namespace Turmerik.Core.Data.Cloneable.Nested.Clnbl.Wrappers.Mappers
{
    public interface INestedMtblClnblWrpprMapper : INestedClnblWrpprMapper
    {
    }

    public interface INestedMtblClnblWrpprMapper<TClnbl, TImmtbl, TMtbl> : INestedClnblWrpprMapper<TClnbl, TImmtbl, TMtbl>, INestedMtblClnblWrpprMapper
        where TClnbl : class, ICloneableObject
        where TImmtbl : class, TClnbl
        where TMtbl : class, TClnbl
    {
    }

    public interface INestedMtblClnblWrpprMapperFactory<TClnbl, TImmtbl, TMtbl> : INestedObjWrpprMapperFactory<INestedMtblClnblWrpprMapper<TClnbl, TImmtbl, TMtbl>, INestedClnblWrppr<TClnbl, TImmtbl, TMtbl>>
        where TClnbl : class, ICloneableObject
        where TImmtbl : class, TClnbl
        where TMtbl : class, TClnbl
    {
    }

    public class NestedMtblClnblWrpprMapperFactory<TClnbl, TImmtbl, TMtbl> : INestedMtblClnblWrpprMapperFactory<TClnbl, TImmtbl, TMtbl>
        where TClnbl : class, ICloneableObject
        where TImmtbl : class, TClnbl
        where TMtbl : class, TClnbl
    {
        public INestedMtblClnblWrpprMapper<TClnbl, TImmtbl, TMtbl> GetMapper(
            ICloneableMapper mapper = null,
            IClonnerFactory clonnerFactory = null)
        {
            var retMapper = new NestedMtblClnblWrpprMapper<TClnbl, TImmtbl, TMtbl>(
                mapper, clonnerFactory);

            return retMapper;
        }

        public INestedObjWrpprMapperCore GetMapperInstn(ICloneableMapper mapper = null, IClonnerFactory clonnerFactory = null) => GetMapper(mapper, clonnerFactory);
    }

    public static class NestedMtblClnblWrpprMapperFactory
    {
        public static INestedObjWrpprMapperCore GetMapper(
            ITypesStaticDataCache typesCache,
            ICloneableMapper mapper,
            IClonnerFactory clonnerFactory,
            Type clnblType,
            Type immtblType,
            Type mtblType)
        {
            var retMapper = typesCache.GetNestedObjWrpprMapperFactory(
                mapper,
                clonnerFactory,
                typeof(NestedMtblClnblWrpprMapperFactory<ICloneableObject, ICloneableObject, ICloneableObject>),
                clnblType,
                immtblType,
                mtblType);

            return retMapper;
        }
    }

    public class NestedMtblClnblWrpprMapper<TClnbl, TImmtbl, TMtbl> : NestedClnblWrpprMapperBase<TClnbl, TImmtbl, TMtbl>, INestedMtblClnblWrpprMapper<TClnbl, TImmtbl, TMtbl>
        where TClnbl : class, ICloneableObject
        where TImmtbl : class, TClnbl
        where TMtbl : class, TClnbl
    {
        public NestedMtblClnblWrpprMapper(ICloneableMapper mapper, IClonnerFactory clonnerFactory) : base(mapper, clonnerFactory)
        {
        }

        public override INestedClnblWrppr<TClnbl, TImmtbl, TMtbl> GetTrgPropValue(INestedObjMapOpts<INestedClnblWrppr<TClnbl, TImmtbl, TMtbl>> opts)
        {
            var wrppr = opts.SrcPropValue;
            TMtbl mtbl = null;

            if (wrppr != null)
            {
                mtbl = wrppr.Mtbl;

                if (mtbl == null)
                {
                    var immtbl = wrppr.Immtbl;

                    if (immtbl != null)
                    {
                        mtbl = Clonner.ToMtbl(immtbl, typeof(TMtbl));
                    }
                }
            }

            var retWrppr = new NestedClnblWrppr<TClnbl, TImmtbl, TMtbl>(
                null, mtbl);

            return retWrppr;
        }
    }
}
