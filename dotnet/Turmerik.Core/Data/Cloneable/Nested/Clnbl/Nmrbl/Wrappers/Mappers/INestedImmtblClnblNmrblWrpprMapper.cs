using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Turmerik.Core.Data.Cloneable.Nested.Wrappers;
using Turmerik.Core.Data.Cloneable.Nested.Wrappers.Mappers;
using Turmerik.Core.Helpers;

namespace Turmerik.Core.Data.Cloneable.Nested.Clnbl.Nmrbl.Wrappers.Mappers
{
    public interface INestedImmtblClnblNmrblWrpprMapper : INestedClnblNmrblWrpprMapper
    {
    }

    public interface INestedImmtblClnblNmrblWrpprMapper<TClnbl, TImmtbl, TMtbl> : INestedClnblNmrblWrpprMapper<TClnbl, TImmtbl, TMtbl>, INestedImmtblClnblNmrblWrpprMapper
        where TClnbl : ICloneableObject
        where TImmtbl : TClnbl
        where TMtbl : TClnbl
    {
    }

    public class NestedImmtblClnblNmrblWrpprMapper<TClnbl, TImmtbl, TMtbl> : NestedClnblNmrblWrpprMapperBase<TClnbl, TImmtbl, TMtbl>, INestedImmtblClnblNmrblWrpprMapper<TClnbl, TImmtbl, TMtbl>
        where TClnbl : ICloneableObject
        where TImmtbl : TClnbl
        where TMtbl : TClnbl
    {
        public NestedImmtblClnblNmrblWrpprMapper(IClonnerComponent<TClnbl, TImmtbl, TMtbl> clonner) : base(clonner)
        {
        }

        public override INestedClnblNmrblWrppr<TClnbl, TImmtbl, TMtbl> GetTrgPropValue(INestedObjMapOpts<INestedClnblNmrblWrppr<TClnbl, TImmtbl, TMtbl>> opts)
        {
            var wrppr = opts.SrcPropValue;
            INestedImmtblClnblClctn<TClnbl, TImmtbl> immtbl = null;

            if (wrppr != null)
            {
                immtbl = wrppr.Immtbl;

                if (immtbl == null && wrppr.Mtbl?.Mtbl != null)
                {
                    var clctn = wrppr.Mtbl.Mtbl.Select(
                        obj => (INestedImmtblClnbl<TClnbl, TImmtbl>)new NestedImmtblClnbl<TClnbl, TImmtbl>(
                            Clonner.ToImmtbl(obj.GetObj()))).RdnlC();

                    immtbl = new NestedImmtblClnblClctn<TClnbl, TImmtbl>(clctn);
                }
            }

            var retWrppr = new NestedClnblNmrblWrppr<TClnbl, TImmtbl, TMtbl>(immtbl, null);
            return retWrppr;
        }
    }
}
