using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Turmerik.Core.Data.Cloneable.Nested.Wrappers;
using Turmerik.Core.Data.Cloneable.Nested.Wrappers.Mappers;
using Turmerik.Core.Infrastucture;

namespace Turmerik.Core.Data.Cloneable.Nested.Nmrbl.Wrappers.Mappers
{
    public interface INestedMtblObjNmrblWrpprMapper : INestedObjNmrblWrpprMapper
    {
    }

    public interface INestedMtblObjNmrblWrpprMapper<TObj> : INestedObjNmrblWrpprMapper<INestedObjMapOpts<INestedObjNmrblWrppr<TObj>>, INestedObjNmrblWrppr<TObj>, TObj>, INestedMtblObjNmrblWrpprMapper
    {
    }

    public class NestedMtblObjNmrblWrpprMapper<TObj> : INestedMtblObjNmrblWrpprMapper<TObj>
    {
        public INestedObjNmrblWrppr<TObj> GetTrgPropValue(INestedObjMapOpts<INestedObjNmrblWrppr<TObj>> opts)
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

        public INestedObjWrpprCore GetTrgPropValue(INestedObjMapOptsCore opts)
        {
            var options = (INestedObjMapOpts<INestedObjNmrblWrppr<TObj>>)opts;
            var wrppr = GetTrgPropValue(options);

            return wrppr;
        }
    }
}
