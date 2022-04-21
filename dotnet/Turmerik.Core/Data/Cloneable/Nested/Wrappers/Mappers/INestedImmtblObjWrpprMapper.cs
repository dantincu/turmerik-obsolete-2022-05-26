using System;
using System.Collections.Generic;
using System.Text;
using Turmerik.Core.Infrastucture;

namespace Turmerik.Core.Data.Cloneable.Nested.Wrappers.Mappers
{
    public interface INestedImmtblObjWrpprMapper : INestedObjWrpprMapperCore
    {
    }

    public interface INestedImmtblObjWrpprMapper<TObj> : INestedObjWrpprMapper<TObj>, INestedImmtblObjWrpprMapper
    {
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
