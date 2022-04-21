using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Turmerik.Core.Data.Cloneable.Nested.Wrappers;
using Turmerik.Core.Data.Cloneable.Nested.Wrappers.Mappers;
using Turmerik.Core.Helpers;

namespace Turmerik.Core.Data.Cloneable.Nested.Clnbl.Nmrbl.Wrappers.Mappers
{
    public interface INestedMtblClnblNmrblWrpprMapper : INestedClnblNmrblWrpprMapper
    {
    }

    public interface INestedMtblClnblNmrblWrpprMapper<TClnbl, TImmtbl, TMtbl> : INestedClnblNmrblWrpprMapper<TClnbl, TImmtbl, TMtbl>, INestedMtblClnblNmrblWrpprMapper
        where TClnbl : ICloneableObject
        where TImmtbl : TClnbl
        where TMtbl : TClnbl
    {
    }

    public class NestedMtblClnblNmrblWrpprMapper<TClnbl, TImmtbl, TMtbl> : NestedClnblNmrblWrpprMapperBase<TClnbl, TImmtbl, TMtbl>, INestedMtblClnblNmrblWrpprMapper<TClnbl, TImmtbl, TMtbl>
        where TClnbl : ICloneableObject
        where TImmtbl : TClnbl
        where TMtbl : TClnbl
    {
        public NestedMtblClnblNmrblWrpprMapper(IClonnerComponent<TClnbl, TImmtbl, TMtbl> clonner) : base(clonner)
        {
        }

        public override INestedClnblNmrblWrppr<TClnbl, TImmtbl, TMtbl> GetTrgPropValue(INestedObjMapOpts<INestedClnblNmrblWrppr<TClnbl, TImmtbl, TMtbl>> opts)
        {
            var wrppr = opts.SrcPropValue;
            INestedMtblClnblList<TClnbl, TMtbl> mtbl = null;

            if (wrppr != null)
            {
                mtbl = wrppr.Mtbl;

                if (mtbl == null && wrppr.Immtbl?.Immtbl != null)
                {
                    var clctn = wrppr.Immtbl.Immtbl.Select(
                        obj => (INestedMtblClnbl<TClnbl, TMtbl>)new NestedMtblClnbl<TClnbl, TMtbl>(
                            Clonner.ToMtbl(obj.GetObj()))).RdnlC();

                    mtbl = new NestedMtblClnblList<TClnbl, TMtbl>(clctn);
                }
            }

            var retWrppr = new NestedClnblNmrblWrppr<TClnbl, TImmtbl, TMtbl>(null, mtbl);
            return retWrppr;
        }
    }
}
