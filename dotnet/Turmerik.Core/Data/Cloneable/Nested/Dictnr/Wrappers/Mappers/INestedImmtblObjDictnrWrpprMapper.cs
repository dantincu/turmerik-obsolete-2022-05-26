using System;
using System.Collections.Generic;
using System.Text;
using Turmerik.Core.Data.Cloneable.Nested.Wrappers;
using Turmerik.Core.Data.Cloneable.Nested.Wrappers.Mappers;
using Turmerik.Core.Infrastucture;

namespace Turmerik.Core.Data.Cloneable.Nested.Dictnr.Wrappers.Mappers
{
    public interface INestedImmtblObjDictnrWrpprMapper : INestedObjDictnrWrpprMapper
    {
    }

    public interface INestedImmtblObjDictnrWrpprMapper<TKey, TObj> : INestedObjDictnrWrpprMapper<TKey, TObj>, INestedImmtblObjDictnrWrpprMapper
    {
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
