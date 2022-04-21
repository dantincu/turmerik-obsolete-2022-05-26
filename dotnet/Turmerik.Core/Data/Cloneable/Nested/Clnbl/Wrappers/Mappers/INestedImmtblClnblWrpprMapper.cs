using System;
using System.Collections.Generic;
using System.Text;
using Turmerik.Core.Data.Cloneable.Nested.Wrappers.Mappers;
using Turmerik.Core.Helpers;

namespace Turmerik.Core.Data.Cloneable.Nested.Clnbl.Wrappers.Mappers
{
    public interface INestedImmtblClnblWrpprMapper : INestedClnblWrpprMapper
    {
    }

    public interface INestedImmtblClnblWrpprMapper<TClnbl, TImmtbl, TMtbl> : INestedClnblWrpprMapper<TClnbl, TImmtbl, TMtbl>, INestedImmtblClnblWrpprMapper
        where TClnbl : class, ICloneableObject
        where TImmtbl : class, TClnbl
        where TMtbl : class, TClnbl
    {
    }

    public interface INestedImmtblClnblWrpprMapperFactory<TClnbl, TImmtbl, TMtbl> : INestedObjWrpprMapperFactory<INestedImmtblClnblWrpprMapper<TClnbl, TImmtbl, TMtbl>, INestedClnblWrppr<TClnbl, TImmtbl, TMtbl>>
        where TClnbl : class, ICloneableObject
        where TImmtbl : class, TClnbl
        where TMtbl : class, TClnbl
    {
    }

    public class NestedImmtblClnblWrpprMapperFactory<TClnbl, TImmtbl, TMtbl> : INestedImmtblClnblWrpprMapperFactory<TClnbl, TImmtbl, TMtbl>
        where TClnbl : class, ICloneableObject
        where TImmtbl : class, TClnbl
        where TMtbl : class, TClnbl
    {
        public INestedImmtblClnblWrpprMapper<TClnbl, TImmtbl, TMtbl> GetMapper(
            ICloneableMapper mapper = null,
            IClonnerFactory clonnerFactory = null)
        {
            var retMapper = new NestedImmtblClnblWrpprMapper<TClnbl, TImmtbl, TMtbl>(
                mapper, clonnerFactory);

            return retMapper;
        }

        public INestedObjWrpprMapperCore GetMapperInstn(ICloneableMapper mapper = null, IClonnerFactory clonnerFactory = null) => GetMapper(mapper, clonnerFactory);
    }

    public static class NestedImmtblClnblWrpprMapperFactory
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
                typeof(NestedImmtblClnblWrpprMapperFactory<ICloneableObject, ICloneableObject, ICloneableObject>),
                clnblType,
                immtblType,
                mtblType);

            return retMapper;
        }
    }

    public class NestedImmtblClnblWrpprMapper<TClnbl, TImmtbl, TMtbl> : NestedClnblWrpprMapperBase<TClnbl, TImmtbl, TMtbl>, INestedImmtblClnblWrpprMapper<TClnbl, TImmtbl, TMtbl>
        where TClnbl : class, ICloneableObject
        where TImmtbl : class, TClnbl
        where TMtbl : class, TClnbl
    {
        public NestedImmtblClnblWrpprMapper(ICloneableMapper mapper, IClonnerFactory clonnerFactory) : base(mapper, clonnerFactory)
        {
        }

        public override INestedClnblWrppr<TClnbl, TImmtbl, TMtbl> GetTrgPropValue(INestedObjMapOpts<INestedClnblWrppr<TClnbl, TImmtbl, TMtbl>> opts)
        {
            var wrppr = opts.SrcPropValue;
            TImmtbl immtbl = null;

            if (wrppr != null)
            {
                immtbl = wrppr.Immtbl;

                if (immtbl == null && wrppr.Mtbl != null)
                {
                    immtbl = Clonner.ToImmtbl(wrppr.Mtbl, typeof(TImmtbl));
                }
            }

            var retWrppr = new NestedClnblWrppr<TClnbl, TImmtbl, TMtbl>(
                immtbl, null);

            return retWrppr;
        }
    }
}
