using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Turmerik.Core.Data.Cloneable.Nested.Wrappers;
using Turmerik.Core.Data.Cloneable.Nested.Wrappers.Mappers;
using Turmerik.Core.Helpers;

namespace Turmerik.Core.Data.Cloneable.Nested.Clnbl.Nmrbl.Wrappers.Mappers
{
    public interface INestedMtblClnblNmrblWrpprMapper : INestedClnblNmrblWrpprMapper
    {
    }

    public interface INestedMtblClnblNmrblWrpprMapper<TClnbl, TImmtbl, TMtbl> : INestedClnblNmrblWrpprMapper<TClnbl, TImmtbl, TMtbl>, INestedMtblClnblNmrblWrpprMapper
        where TClnbl : ICloneableObject
        where TImmtbl : class, TClnbl
        where TMtbl : class, TClnbl
    {
    }

    public interface INestedMtblClnblNmrblWrpprMapperFactory<TClnbl, TImmtbl, TMtbl> : INestedObjWrpprMapperFactory<INestedMtblClnblNmrblWrpprMapper<TClnbl, TImmtbl, TMtbl>, INestedClnblNmrblWrppr<TClnbl, TImmtbl, TMtbl>>
        where TClnbl : ICloneableObject
        where TImmtbl : class, TClnbl
        where TMtbl : class, TClnbl
    {
    }

    public class NestedMtblClnblNmrblWrpprMapperFactory<TClnbl, TImmtbl, TMtbl> : INestedMtblClnblNmrblWrpprMapperFactory<TClnbl, TImmtbl, TMtbl>
        where TClnbl : ICloneableObject
        where TImmtbl : class, TClnbl
        where TMtbl : class, TClnbl
    {
        public INestedMtblClnblNmrblWrpprMapper<TClnbl, TImmtbl, TMtbl> GetMapper(
            ICloneableMapper mapper = null,
            IClonnerFactory clonnerFactory = null)
        {
            var retMapper = new NestedMtblClnblNmrblWrpprMapper<TClnbl, TImmtbl, TMtbl>(
                mapper, clonnerFactory);

            return retMapper;
        }

        public INestedObjWrpprMapperCore GetMapperInstn(ICloneableMapper mapper = null, IClonnerFactory clonnerFactory = null) => GetMapper(mapper, clonnerFactory);
    }

    public static class NestedMtblClnblNmrblWrpprMapperFactory
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
                typeof(NestedMtblClnblNmrblWrpprMapperFactory<ICloneableObject, ICloneableObject, ICloneableObject>),
                clnblType,
                immtblType,
                mtblType);

            return retMapper;
        }
    }

    public class NestedMtblClnblNmrblWrpprMapper<TClnbl, TImmtbl, TMtbl> : NestedClnblNmrblWrpprMapperBase<TClnbl, TImmtbl, TMtbl>, INestedMtblClnblNmrblWrpprMapper<TClnbl, TImmtbl, TMtbl>
        where TClnbl : ICloneableObject
        where TImmtbl : class, TClnbl
        where TMtbl : class, TClnbl
    {
        public NestedMtblClnblNmrblWrpprMapper(ICloneableMapper mapper, IClonnerFactory clonnerFactory) : base(mapper, clonnerFactory)
        {
        }

        public override INestedClnblNmrblWrppr<TClnbl, TImmtbl, TMtbl> GetTrgPropValue(INestedObjMapOpts<INestedClnblNmrblWrppr<TClnbl, TImmtbl, TMtbl>> opts)
        {
            var wrppr = opts.SrcPropValue;
            INestedMtblClnblList<TClnbl, TMtbl> mtbl = null;

            if (wrppr != null)
            {
                mtbl = wrppr.Mtbl;

                if (mtbl == null && wrppr.Immtbl?.Immtbl != null)
                {
                    var clctn = wrppr.Immtbl.Immtbl.Select(
                        obj => (INestedMtblClnbl<TClnbl, TMtbl>)new NestedMtblClnbl<TClnbl, TMtbl>(
                            Clonner.ToMtbl(obj.GetObj()))).RdnlC();

                    mtbl = new NestedMtblClnblList<TClnbl, TMtbl>(clctn);
                }
            }

            var retWrppr = new NestedClnblNmrblWrppr<TClnbl, TImmtbl, TMtbl>(null, mtbl);
            return retWrppr;
        }
    }
}
