using System;
using System.Collections.Generic;
using System.Text;
using Turmerik.Core.Data.Cloneable.Nested.Wrappers;
using Turmerik.Core.Data.Cloneable.Nested.Wrappers.Mappers;
using Turmerik.Core.Helpers;
using Turmerik.Core.Infrastucture;

namespace Turmerik.Core.Data.Cloneable.Nested.Dictnr.Wrappers.Mappers
{
    public interface INestedMtblObjDictnrWrpprMapper : INestedObjDictnrWrpprMapper
    {
    }

    public interface INestedMtblObjDictnrWrpprMapper<TKey, TObj> : INestedObjDictnrWrpprMapper<TKey, TObj>, INestedMtblObjDictnrWrpprMapper
    {
    }

    public interface INestedMtblObjDictnrWrpprMapperFactory<TKey, TObj> : INestedObjWrpprMapperFactory<INestedMtblObjDictnrWrpprMapper<TKey, TObj>, INestedObjDictnrWrppr<TKey, TObj>>
    {
    }

    public class NestedMtblObjDictnrWrpprMapperFactory<TKey, TObj> : INestedMtblObjDictnrWrpprMapperFactory<TKey, TObj>
    {
        public INestedMtblObjDictnrWrpprMapper<TKey, TObj> GetMapper(
            ICloneableMapper mapper = null,
            IClonnerFactory clonnerFactory = null)
        {
            var retMapper = new NestedMtblObjDictnrWrpprMapper<TKey, TObj>();
            return retMapper;
        }

        public INestedObjWrpprMapperCore GetMapperInstn(ICloneableMapper mapper = null, IClonnerFactory clonnerFactory = null) => GetMapper(mapper, clonnerFactory);
    }

    public static class NestedMtblObjDictnrWrpprMapperFactory
    {
        public static INestedObjWrpprMapperCore GetMapper(
            ITypesStaticDataCache typesCache,
            ICloneableMapper mapper,
            IClonnerFactory clonnerFactory,
            Type keyType,
            Type objlType)
        {
            var retMapper = typesCache.GetNestedObjWrpprMapperFactory(
                mapper,
                clonnerFactory,
                typeof(NestedMtblObjDictnrWrpprMapperFactory<object, object>),
                keyType,
                objlType);

            return retMapper;
        }
    }

    public class NestedMtblObjDictnrWrpprMapper<TKey, TObj> : NestedObjDictnrWrpprMapperBase<TKey, TObj>, INestedMtblObjDictnrWrpprMapper<TKey, TObj>
    {
        public override INestedObjWrppr<INestedObjDictnrWrppr<TKey, TObj>> GetTrgPropValue(INestedObjMapOpts<INestedObjWrppr<INestedObjDictnrWrppr<TKey, TObj>>> opts)
        {
            var wrppr = opts.SrcPropValue;

            var mtbl = wrppr?.Mtbl ?? new NestedObjDictnrWrppr<TKey, TObj>(
                null,
                new NestedObjEdtblDictnr<TKey, TObj>(
                    wrppr?.Immtbl?.Immtbl?.Immtbl));

            var retWrppr = new NestedObjWrppr<INestedObjDictnrWrppr<TKey, TObj>>(
                mtbl, null);

            return retWrppr;
        }
    }
}
