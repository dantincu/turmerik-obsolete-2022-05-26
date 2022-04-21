using System;
using System.Collections.Generic;
using System.Text;
using Turmerik.Core.Data.Cloneable.Nested.Wrappers;
using Turmerik.Core.Data.Cloneable.Nested.Wrappers.Mappers;
using Turmerik.Core.Helpers;
using Turmerik.Core.Infrastucture;

namespace Turmerik.Core.Data.Cloneable.Nested.Dictnr.Wrappers.Mappers
{
    public interface INestedImmtblObjDictnrWrpprMapper : INestedObjDictnrWrpprMapper
    {
    }

    public interface INestedImmtblObjDictnrWrpprMapper<TKey, TObj> : INestedObjDictnrWrpprMapper<TKey, TObj>, INestedImmtblObjDictnrWrpprMapper
    {
    }

    public interface INestedImmtblObjDictnrWrpprMapperFactory<TKey, TObj> : INestedObjWrpprMapperFactory<INestedImmtblObjDictnrWrpprMapper<TKey, TObj>, INestedObjDictnrWrppr<TKey, TObj>>
    {
    }

    public class NestedImmtblObjDictnrWrpprMapperFactory<TKey, TObj> : INestedImmtblObjDictnrWrpprMapperFactory<TKey, TObj>
    {
        public INestedImmtblObjDictnrWrpprMapper<TKey, TObj> GetMapper(
            ICloneableMapper mapper = null,
            IClonnerFactory clonnerFactory = null)
        {
            var retMapper = new NestedImmtblObjDictnrWrpprMapper<TKey, TObj>();
            return retMapper;
        }

        public INestedObjWrpprMapperCore GetMapperInstn(ICloneableMapper mapper = null, IClonnerFactory clonnerFactory = null) => GetMapper(mapper, clonnerFactory);
    }

    public static class NestedImmtblObjDictnrWrpprMapperFactory
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
                typeof(NestedImmtblObjDictnrWrpprMapperFactory<object, object>),
                keyType,
                objlType);

            return retMapper;
        }
    }

    public class NestedImmtblObjDictnrWrpprMapper<TKey, TObj> : NestedObjDictnrWrpprMapperBase<TKey, TObj>, INestedImmtblObjDictnrWrpprMapper<TKey, TObj>
    {
        public override INestedObjWrppr<INestedObjDictnrWrppr<TKey, TObj>> GetTrgPropValue(INestedObjMapOpts<INestedObjWrppr<INestedObjDictnrWrppr<TKey, TObj>>> opts)
        {
            var wrppr = opts.SrcPropValue;

            var immtbl = wrppr?.Immtbl ?? new NestedObjDictnrWrppr<TKey, TObj>(
                    new NestedObjRdnlDictnr<TKey, TObj>(
                        wrppr?.Mtbl?.Mtbl?.Mtbl),
                    null);

            var retWrppr = new NestedObjWrppr<INestedObjDictnrWrppr<TKey, TObj>>(
                immtbl, null);

            return retWrppr;
        }
    }
}
