using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Turmerik.Core.Data.Cloneable.Nested.Wrappers;
using Turmerik.Core.Data.Cloneable.Nested.Wrappers.Mappers;
using Turmerik.Core.Helpers;
using Turmerik.Core.Infrastucture;

namespace Turmerik.Core.Data.Cloneable.Nested.Nmrbl.Wrappers.Mappers
{
    public interface INestedMtblObjNmrblWrpprMapper : INestedObjNmrblWrpprMapper
    {
    }

    public interface INestedMtblObjNmrblWrpprMapper<TObj> : INestedObjNmrblWrpprMapper<INestedObjMapOpts<INestedObjNmrblWrppr<TObj>>, INestedObjNmrblWrppr<TObj>, TObj>, INestedMtblObjNmrblWrpprMapper
    {
    }

    public interface INestedMtblObjNmrblWrpprMapperFactory<TObj> : INestedObjWrpprMapperFactory<INestedMtblObjNmrblWrpprMapper<TObj>, INestedObjNmrblWrppr<TObj>>
    {
    }

    public class NestedMtblObjNmrblWrpprMapperFactory<TObj> : INestedMtblObjNmrblWrpprMapperFactory<TObj>
    {
        public INestedMtblObjNmrblWrpprMapper<TObj> GetMapper(ICloneableMapper mapper = null, IClonnerFactory clonnerFactory = null)
        {
            var retMapper = new NestedMtblObjNmrblWrpprMapper<TObj>();
            return retMapper;
        }

        public INestedObjWrpprMapperCore GetMapperInstn(ICloneableMapper mapper = null, IClonnerFactory clonnerFactory = null) => GetMapper(mapper, clonnerFactory);
    }

    public static class NestedMtblObjNmrblWrpprMapperFactory
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
                typeof(NestedMtblObjNmrblWrpprMapperFactory<object>),
                objlType);

            return retMapper;
        }
    }

    public class NestedMtblObjNmrblWrpprMapper<TObj> : NestedImmtblObjNmrblWrpprMapperBase<TObj>, INestedMtblObjNmrblWrpprMapper<TObj>
    {
        public override INestedObjNmrblWrppr<TObj> GetTrgPropValue(INestedObjMapOpts<INestedObjNmrblWrppr<TObj>> opts)
        {
            var wrppr = opts.SrcPropValue;
            INestedMtblObjList<TObj> mtblWrppr = wrppr?.Mtbl;

            if (mtblWrppr == null && wrppr.Immtbl != null)
            {
                var clctn = wrppr.Immtbl.GetObj();
                mtblWrppr = new NestedMtblObjList<TObj>(clctn);
            }

            var retWrppr = new NestedObjNmrblWrppr<TObj>(
                null, mtblWrppr);

            return retWrppr;
        }
    }
}
