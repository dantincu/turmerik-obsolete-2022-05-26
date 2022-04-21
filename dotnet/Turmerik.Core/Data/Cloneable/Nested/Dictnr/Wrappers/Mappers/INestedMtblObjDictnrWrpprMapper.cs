using System;
using System.Collections.Generic;
using System.Text;
using Turmerik.Core.Data.Cloneable.Nested.Wrappers;
using Turmerik.Core.Data.Cloneable.Nested.Wrappers.Mappers;
using Turmerik.Core.Infrastucture;

namespace Turmerik.Core.Data.Cloneable.Nested.Dictnr.Wrappers.Mappers
{
    public interface INestedMtblObjDictnrWrpprMapper : INestedObjDictnrWrpprMapper
    {
    }

    public interface INestedMtblObjDictnrWrpprMapper<TKey, TObj> : INestedObjDictnrWrpprMapper<TKey, TObj>, INestedMtblObjDictnrWrpprMapper
    {
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
