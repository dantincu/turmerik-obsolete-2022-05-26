using System;
using System.Collections.Generic;
using System.Text;
using Turmerik.Core.Data.Cloneable.Nested.Wrappers.Mappers;

namespace Turmerik.Core.Data.Cloneable.Nested.Clnbl.Wrappers.Mappers
{
    public interface INestedMtblClnblWrpprMapper : INestedClnblWrpprMapper
    {
    }

    public interface INestedMtblClnblWrpprMapper<TClnbl, TImmtbl, TMtbl> : INestedClnblWrpprMapper<TClnbl, TImmtbl, TMtbl>, INestedMtblClnblWrpprMapper
        where TClnbl : ICloneableObject
        where TImmtbl : TClnbl
        where TMtbl : TClnbl
    {
    }

    public class NestedMtblClnblWrpprMapper<TClnbl, TImmtbl, TMtbl> : NestedClnblWrpprMapperBase<TClnbl, TImmtbl, TMtbl>, INestedMtblClnblWrpprMapper<TClnbl, TImmtbl, TMtbl>
        where TClnbl : ICloneableObject
        where TImmtbl : class, TClnbl
        where TMtbl : class, TClnbl
    {
        public NestedMtblClnblWrpprMapper(IClonnerComponent<TClnbl, TImmtbl, TMtbl> clonner) : base(clonner)
        {
        }

        public override INestedClnblWrppr<TClnbl, TImmtbl, TMtbl> GetTrgPropValue(INestedObjMapOpts<INestedClnblWrppr<TClnbl, TImmtbl, TMtbl>> opts)
        {
            var wrppr = opts.SrcPropValue;
            TMtbl mtbl = null;

            if (wrppr != null)
            {
                mtbl = wrppr.Mtbl;

                if (mtbl == null)
                {
                    var immtbl = wrppr.Immtbl;

                    if (immtbl != null)
                    {
                        mtbl = Clonner.ToMtbl(immtbl);
                    }
                }
            }

            var retWrppr = new NestedClnblWrppr<TClnbl, TImmtbl, TMtbl>(
                null, mtbl);

            return retWrppr;
        }
    }
}
