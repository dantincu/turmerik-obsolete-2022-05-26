using System;
using System.Collections.Generic;
using System.Text;
using Turmerik.Core.Helpers;
using Turmerik.Core.Infrastucture;

namespace Turmerik.Core.Data.Cloneable.Nested.Wrappers.Mappers
{
    public interface INestedImmtblObjWrpprMapper : INestedObjWrpprMapperCore
    {
    }

    public interface INestedImmtblObjWrpprMapper<TObj> : INestedObjWrpprMapper<TObj>, INestedImmtblObjWrpprMapper
    {
    }

    public interface INestedImmtblObjWrpprMapperFactory<TObj> : INestedObjWrpprMapperFactory<NestedImmtblObjWrpprMapper<TObj>, INestedObjWrppr<TObj>>
    {
    }

    public class NestedImmtblObjWrpprMapperFactory<TObj> : INestedImmtblObjWrpprMapperFactory<TObj>
    {
        public NestedImmtblObjWrpprMapper<TObj> GetMapper(ICloneableMapper mapper = null, IClonnerFactory clonnerFactory = null)
        {
            var retMapper = new NestedImmtblObjWrpprMapper<TObj>();
            return retMapper;
        }

        public INestedObjWrpprMapperCore GetMapperInstn(ICloneableMapper mapper = null, IClonnerFactory clonnerFactory = null) => GetMapper(mapper, clonnerFactory);
    }

    public static class NestedImmtblObjNmrblWrpprMapperFactory
    {
        public static INestedObjWrpprMapperCore GetMapper(
            ITypesStaticDataCache typesCache,
            ICloneableMapper mapper,
            IClonnerFactory clonnerFactory,
            Type objlType)
        {
            var retMapper = typesCache.GetNestedObjWrpprMapperFactory(
                mapper,
                clonnerFactory,
                typeof(NestedImmtblObjWrpprMapperFactory<object>),
                objlType);

            return retMapper;
        }
    }

    public class NestedImmtblObjWrpprMapper<TObj> : NestedObjWrpprMapperBase<TObj>, INestedImmtblObjWrpprMapper<TObj>
    {
        public override INestedObjWrppr<TObj> GetTrgPropValue(INestedObjMapOpts<INestedObjWrppr<TObj>> opts)
        {
            var wrppr = new NestedObjWrppr<TObj>(
                opts.SrcPropValue.Immtbl, default);

            return wrppr;
        }
    }
}
