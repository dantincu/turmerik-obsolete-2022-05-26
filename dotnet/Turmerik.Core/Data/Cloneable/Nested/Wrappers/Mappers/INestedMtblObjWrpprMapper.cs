using System;
using System.Collections.Generic;
using System.Text;
using Turmerik.Core.Helpers;
using Turmerik.Core.Infrastucture;

namespace Turmerik.Core.Data.Cloneable.Nested.Wrappers.Mappers
{
    public interface INestedMtblObjWrpprMapper : INestedObjWrpprMapperCore
    {
    }

    public interface INestedMtblObjWrpprMapper<TObj> : INestedObjWrpprMapper<TObj>, INestedMtblObjWrpprMapper
    {
    }

    public interface INestedMtblObjWrpprMapperFactory<TObj> : INestedObjWrpprMapperFactory<NestedMtblObjWrpprMapper<TObj>, INestedObjWrppr<TObj>>
    {
    }

    public class NestedMtblObjWrpprMapperFactory<TObj> : INestedMtblObjWrpprMapperFactory<TObj>
    {
        public NestedMtblObjWrpprMapper<TObj> GetMapper(ICloneableMapper mapper = null, IClonnerFactory clonnerFactory = null)
        {
            var retMapper = new NestedMtblObjWrpprMapper<TObj>();
            return retMapper;
        }

        public INestedObjWrpprMapperCore GetMapperInstn(ICloneableMapper mapper = null, IClonnerFactory clonnerFactory = null) => GetMapper(mapper, clonnerFactory);
    }

    public static class NestedMtblObjWrpprMapperFactory
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
                typeof(NestedMtblObjWrpprMapperFactory<object>),
                objlType);

            return retMapper;
        }
    }

    public class NestedMtblObjWrpprMapper<TObj> : NestedObjWrpprMapperBase<TObj>, INestedMtblObjWrpprMapper<TObj>
    {
        public override INestedObjWrppr<TObj> GetTrgPropValue(INestedObjMapOpts<INestedObjWrppr<TObj>> opts)
        {
            var wrppr = new NestedObjWrppr<TObj>(
                default, opts.SrcPropValue.Mtbl);

            return wrppr;
        }
    }
}
