using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using Turmerik.Core.Data.Cloneable.Nested.Wrappers;
using Turmerik.Core.Data.Cloneable.Nested.Wrappers.Mappers;
using Turmerik.Core.Helpers;
using Turmerik.Core.Infrastucture;

namespace Turmerik.Core.Data.Cloneable.Nested.Nmrbl.Wrappers.Mappers
{
    public interface INestedImmtblObjNmrblWrpprMapper : INestedObjNmrblWrpprMapper
    {
    }

    public interface INestedImmtblObjNmrblWrpprMapper<TObj> : INestedObjNmrblWrpprMapper<INestedObjMapOpts<INestedObjNmrblWrppr<TObj>>, INestedObjNmrblWrppr<TObj>, TObj>, INestedImmtblObjNmrblWrpprMapper
    {
    }

    public interface INestedImmtblObjNmrblWrpprMapperFactory<TObj> : INestedObjWrpprMapperFactory<INestedImmtblObjNmrblWrpprMapper<TObj>, INestedObjNmrblWrppr<TObj>>
    {
    }

    public class NestedImmtblObjNmrblWrpprMapperFactory<TObj> : INestedImmtblObjNmrblWrpprMapperFactory<TObj>
    {
        public INestedImmtblObjNmrblWrpprMapper<TObj> GetMapper(ICloneableMapper mapper = null, IClonnerFactory clonnerFactory = null)
        {
            var retMapper = new NestedImmtblObjNmrblWrpprMapper<TObj>();
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
                typeof(NestedImmtblObjNmrblWrpprMapperFactory<object>),
                objlType);

            return retMapper;
        }
    }

    public class NestedImmtblObjNmrblWrpprMapper<TObj> : NestedImmtblObjNmrblWrpprMapperBase<TObj>, INestedImmtblObjNmrblWrpprMapper<TObj>
    {
        public override INestedObjNmrblWrppr<TObj> GetTrgPropValue(INestedObjMapOpts<INestedObjNmrblWrppr<TObj>> opts)
        {
            var wrppr = opts.SrcPropValue;
            INestedImmtblObjClctn<TObj> mtblWrppr = wrppr?.Immtbl;

            if (mtblWrppr == null && wrppr.Mtbl != null)
            {
                var clctn = wrppr.Mtbl.GetObj();
                mtblWrppr = new NestedImmtblObjClctn<TObj>(clctn);
            }

            var retWrppr = new NestedObjNmrblWrppr<TObj>(
                mtblWrppr, null);

            return retWrppr;
        }
    }
}
