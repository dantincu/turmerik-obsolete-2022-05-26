using System;
using System.Collections.Generic;
using System.Text;
using Turmerik.Core.Data.Cloneable.Nested.Wrappers.Mappers;
using Turmerik.Core.Helpers;

namespace Turmerik.Core.Data.Cloneable.Nested.Clnbl.Wrappers.Mappers
{
    public interface INestedImmtblClnblWrpprMapper : INestedClnblWrpprMapper
    {
    }

    public interface INestedImmtblClnblWrpprMapper<TClnbl, TImmtbl, TMtbl> : INestedClnblWrpprMapper<TClnbl, TImmtbl, TMtbl>, INestedImmtblClnblWrpprMapper
        where TClnbl : class, ICloneableObject
        where TImmtbl : class, TClnbl
        where TMtbl : class, TClnbl
    {
    }

    public class NestedImmtblClnblWrpprMapper<TClnbl, TImmtbl, TMtbl> : NestedClnblWrpprMapperBase<TClnbl, TImmtbl, TMtbl>, INestedImmtblClnblWrpprMapper<TClnbl, TImmtbl, TMtbl>
        where TClnbl : class, ICloneableObject
        where TImmtbl : class, TClnbl
        where TMtbl : class, TClnbl
    {
        public NestedImmtblClnblWrpprMapper(ICloneableMapper mapper, IClonnerFactory clonnerFactory) : base(mapper, clonnerFactory)
        {
        }

        public override INestedClnblWrppr<TClnbl, TImmtbl, TMtbl> GetTrgPropValue(INestedObjMapOpts<INestedClnblWrppr<TClnbl, TImmtbl, TMtbl>> opts)
        {
            var wrppr = opts.SrcPropValue;
            TImmtbl immtbl = null;

            if (wrppr != null)
            {
                immtbl = wrppr.Immtbl;

                if (immtbl == null && wrppr.Mtbl != null)
                {
                    immtbl = Clonner.ToImmtbl(wrppr.Mtbl, typeof(TMtbl));
                }
            }

            var retWrppr = new NestedClnblWrppr<TClnbl, TImmtbl, TMtbl>(
                immtbl, null);

            return retWrppr;
        }
    }
}
