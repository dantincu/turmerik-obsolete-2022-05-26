using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Turmerik.Core.Data.Cloneable.Nested.Wrappers.Mappers;

namespace Turmerik.Core.Data.Cloneable.Nested.Clnbl.Dictnr.Wrappers.Mappers
{
    public interface INestedMtblClnblDictnrWrpprMapper : INestedClnblDictnrWrpprMapper
    {
    }

    public interface INestedMtblClnblDictnrWrpprMapper<TKey, TClnbl, TImmtbl, TMtbl> : INestedClnblDictnrWrpprMapper<TKey, TClnbl, TImmtbl, TMtbl>, INestedMtblClnblDictnrWrpprMapper
        where TClnbl : ICloneableObject
        where TImmtbl : TClnbl
        where TMtbl : TClnbl
    {
    }

    public class NestedMtblClnblDictnrWrpprMapper<TKey, TClnbl, TImmtbl, TMtbl> : NestedClnblDictnrWrpprMapperBase<TKey, TClnbl, TImmtbl, TMtbl>, INestedMtblClnblDictnrWrpprMapper<TKey, TClnbl, TImmtbl, TMtbl>
        where TClnbl : ICloneableObject
        where TImmtbl : TClnbl
        where TMtbl : TClnbl
    {
        public NestedMtblClnblDictnrWrpprMapper(IClonnerComponent<TClnbl, TImmtbl, TMtbl> clonner) : base(clonner)
        {
        }

        public override INestedClnblDictnrWrppr<TKey, TClnbl, TImmtbl, TMtbl> GetTrgPropValue(
            INestedObjMapOpts<INestedClnblDictnrWrppr<TKey, TClnbl, TImmtbl, TMtbl>> opts)
        {
            var wrppr = opts.SrcPropValue;
            INestedClnblEdtblDictnr<TKey, TClnbl, TMtbl> mtbl = null;

            if (wrppr != null)
            {
                mtbl = wrppr.Mtbl;

                if (mtbl == null && wrppr.Immtbl?.Immtbl != null)
                {
                    var dictnr = wrppr.Immtbl.Immtbl.ToDictionary(
                        kvp => kvp.Key, kvp => (INestedMtblClnbl<TClnbl, TMtbl>)new NestedMtblClnbl<TClnbl, TMtbl>(
                            Clonner.ToMtbl(kvp.Value.Immtbl)));

                    mtbl = new NestedClnblEdtblDictnr<TKey, TClnbl, TMtbl>(dictnr);
                }
            }

            var retWrppr = new NestedClnblDictnrWrppr<TKey, TClnbl, TImmtbl, TMtbl>(null, mtbl);
            return retWrppr;
        }
    }
}
