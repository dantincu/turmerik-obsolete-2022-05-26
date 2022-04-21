using System;
using System.Linq;
using Turmerik.Core.Data.Cloneable.Nested.Wrappers.Mappers;
using Turmerik.Core.Helpers;

namespace Turmerik.Core.Data.Cloneable.Nested.Clnbl.Nmrbl.Wrappers.Mappers
{
    public interface INestedImmtblClnblNmrblWrpprMapper : INestedClnblNmrblWrpprMapper
    {
    }

    public interface INestedImmtblClnblNmrblWrpprMapper<TClnbl, TImmtbl, TMtbl> : INestedClnblNmrblWrpprMapper<TClnbl, TImmtbl, TMtbl>, INestedImmtblClnblNmrblWrpprMapper
        where TClnbl : ICloneableObject
        where TImmtbl : class, TClnbl
        where TMtbl : class, TClnbl
    {
    }

    public interface INestedImmtblClnblNmrblWrpprMapperFactory<TClnbl, TImmtbl, TMtbl> : INestedObjWrpprMapperFactory<INestedImmtblClnblNmrblWrpprMapper<TClnbl, TImmtbl, TMtbl>, INestedClnblNmrblWrppr<TClnbl, TImmtbl, TMtbl>>
        where TClnbl : ICloneableObject
        where TImmtbl : class, TClnbl
        where TMtbl : class, TClnbl
    {
    }

    public class NestedImmtblClnblNmrblWrpprMapperFactory<TClnbl, TImmtbl, TMtbl> : INestedImmtblClnblNmrblWrpprMapperFactory<TClnbl, TImmtbl, TMtbl>
        where TClnbl : ICloneableObject
        where TImmtbl : class, TClnbl
        where TMtbl : class, TClnbl
    {
        public INestedImmtblClnblNmrblWrpprMapper<TClnbl, TImmtbl, TMtbl> GetMapper(
            ICloneableMapper mapper = null,
            IClonnerFactory clonnerFactory = null)
        {
            var retMapper = new NestedImmtblClnblNmrblWrpprMapper<TClnbl, TImmtbl, TMtbl>(
                mapper, clonnerFactory);

            return retMapper;
        }

        public INestedObjWrpprMapperCore GetMapperInstn(ICloneableMapper mapper = null, IClonnerFactory clonnerFactory = null) => GetMapper(mapper, clonnerFactory);
    }

    public static class NestedImmtblClnblNmrblWrpprMapperFactory
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
                typeof(NestedImmtblClnblNmrblWrpprMapperFactory<ICloneableObject, ICloneableObject, ICloneableObject>),
                clnblType,
                immtblType,
                mtblType);

            return retMapper;
        }
    }

    public class NestedImmtblClnblNmrblWrpprMapper<TClnbl, TImmtbl, TMtbl> : NestedClnblNmrblWrpprMapperBase<TClnbl, TImmtbl, TMtbl>, INestedImmtblClnblNmrblWrpprMapper<TClnbl, TImmtbl, TMtbl>
        where TClnbl : ICloneableObject
        where TImmtbl : class, TClnbl
        where TMtbl : class, TClnbl
    {
        public NestedImmtblClnblNmrblWrpprMapper(ICloneableMapper mapper, IClonnerFactory clonnerFactory) : base(mapper, clonnerFactory)
        {
        }

        public override INestedClnblNmrblWrppr<TClnbl, TImmtbl, TMtbl> GetTrgPropValue(INestedObjMapOpts<INestedClnblNmrblWrppr<TClnbl, TImmtbl, TMtbl>> opts)
        {
            var wrppr = opts.SrcPropValue;
            INestedImmtblClnblClctn<TClnbl, TImmtbl> immtbl = null;

            if (wrppr != null)
            {
                immtbl = wrppr.Immtbl;

                if (immtbl == null && wrppr.Mtbl?.Mtbl != null)
                {
                    var clctn = wrppr.Mtbl.Mtbl.Select(
                        obj => (INestedImmtblClnbl<TClnbl, TImmtbl>)new NestedImmtblClnbl<TClnbl, TImmtbl>(
                            Clonner.ToImmtbl(obj.GetObj(), typeof(TImmtbl)))).RdnlC();

                    immtbl = new NestedImmtblClnblClctn<TClnbl, TImmtbl>(clctn);
                }
            }

            var retWrppr = new NestedClnblNmrblWrppr<TClnbl, TImmtbl, TMtbl>(immtbl, null);
            return retWrppr;
        }
    }
}
