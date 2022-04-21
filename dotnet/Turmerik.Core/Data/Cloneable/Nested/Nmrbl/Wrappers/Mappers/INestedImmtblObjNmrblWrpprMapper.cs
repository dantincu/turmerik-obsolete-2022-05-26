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
