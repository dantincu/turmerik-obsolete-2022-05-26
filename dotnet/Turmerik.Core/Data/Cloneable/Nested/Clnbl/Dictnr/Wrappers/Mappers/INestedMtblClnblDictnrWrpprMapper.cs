using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Turmerik.Core.Data.Cloneable.Nested.Wrappers.Mappers;
using Turmerik.Core.Helpers;

namespace Turmerik.Core.Data.Cloneable.Nested.Clnbl.Dictnr.Wrappers.Mappers
{
    public interface INestedMtblClnblDictnrWrpprMapper : INestedClnblDictnrWrpprMapper
    {
    }

    public interface INestedMtblClnblDictnrWrpprMapper<TKey, TClnbl, TImmtbl, TMtbl> : INestedClnblDictnrWrpprMapper<TKey, TClnbl, TImmtbl, TMtbl>, INestedMtblClnblDictnrWrpprMapper
        where TClnbl : ICloneableObject
        where TImmtbl : class, TClnbl
        where TMtbl : class, TClnbl
    {
    }

    public interface INestedMtblClnblDictnrWrpprMapperFactory<TKey, TClnbl, TImmtbl, TMtbl> : INestedObjWrpprMapperFactory<INestedMtblClnblDictnrWrpprMapper<TKey, TClnbl, TImmtbl, TMtbl>, INestedClnblDictnrWrppr<TKey, TClnbl, TImmtbl, TMtbl>>
            where TClnbl : ICloneableObject
            where TImmtbl : class, TClnbl
            where TMtbl : class, TClnbl
    {
    }

    public class NestedMtblClnblDictnrWrpprMapperFactory<TKey, TClnbl, TImmtbl, TMtbl> : INestedMtblClnblDictnrWrpprMapperFactory<TKey, TClnbl, TImmtbl, TMtbl>
            where TClnbl : ICloneableObject
            where TImmtbl : class, TClnbl
            where TMtbl : class, TClnbl
    {
        public INestedMtblClnblDictnrWrpprMapper<TKey, TClnbl, TImmtbl, TMtbl> GetMapper(ICloneableMapper mapper = null, IClonnerFactory clonnerFactory = null)
        {
            var retMapper = new NestedMtblClnblDictnrWrpprMapper<TKey, TClnbl, TImmtbl, TMtbl>(mapper, clonnerFactory);
            return retMapper;
        }

        public INestedObjWrpprMapperCore GetMapperInstn(ICloneableMapper mapper = null, IClonnerFactory clonnerFactory = null) => GetMapper(mapper, clonnerFactory);
    }

    public static class NestedMtblClnblDictnrWrpprMapperFactory
    {
        public static INestedObjWrpprMapperCore GetMapper(
            ITypesStaticDataCache typesCache,
            ICloneableMapper mapper,
            IClonnerFactory clonnerFactory,
            Type keyType,
            Type clnblType,
            Type immtblType,
            Type mtblType)
        {
            var retMapper = typesCache.GetNestedObjWrpprMapperFactory(
                mapper,
                clonnerFactory,
                typeof(NestedMtblClnblDictnrWrpprMapperFactory<object, ICloneableObject, ICloneableObject, ICloneableObject>),
                keyType,
                clnblType,
                immtblType,
                mtblType);

            return retMapper;
        }
    }

    public class NestedMtblClnblDictnrWrpprMapper<TKey, TClnbl, TImmtbl, TMtbl> : NestedClnblDictnrWrpprMapperBase<TKey, TClnbl, TImmtbl, TMtbl>, INestedMtblClnblDictnrWrpprMapper<TKey, TClnbl, TImmtbl, TMtbl>
        where TClnbl : ICloneableObject
        where TImmtbl : class, TClnbl
        where TMtbl : class, TClnbl
    {
        public NestedMtblClnblDictnrWrpprMapper(ICloneableMapper mapper, IClonnerFactory clonnerFactory) : base(mapper, clonnerFactory)
        {
        }

        public override INestedClnblDictnrWrppr<TKey, TClnbl, TImmtbl, TMtbl> GetTrgPropValue(
            INestedObjMapOpts<INestedClnblDictnrWrppr<TKey, TClnbl, TImmtbl, TMtbl>> opts)
        {
            var wrppr = opts.SrcPropValue;
            INestedClnblEdtblDictnr<TKey, TClnbl, TMtbl> mtbl = null;

            if (wrppr != null)
            {
                mtbl = wrppr.Mtbl;

                if (mtbl == null && wrppr.Immtbl?.Immtbl != null)
                {
                    var dictnr = wrppr.Immtbl.Immtbl.ToDictionary(
                        kvp => kvp.Key, kvp => (INestedMtblClnbl<TClnbl, TMtbl>)new NestedMtblClnbl<TClnbl, TMtbl>(
                            Clonner.ToMtbl(kvp.Value.Immtbl, typeof(TMtbl))));

                    mtbl = new NestedClnblEdtblDictnr<TKey, TClnbl, TMtbl>(dictnr);
                }
            }

            var retWrppr = new NestedClnblDictnrWrppr<TKey, TClnbl, TImmtbl, TMtbl>(null, mtbl);
            return retWrppr;
        }
    }
}
