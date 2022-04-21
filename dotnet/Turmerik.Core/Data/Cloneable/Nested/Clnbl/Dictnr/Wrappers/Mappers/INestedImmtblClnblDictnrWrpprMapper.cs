using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Turmerik.Core.Data.Cloneable.Nested.Wrappers.Mappers;

namespace Turmerik.Core.Data.Cloneable.Nested.Clnbl.Dictnr.Wrappers.Mappers
{
    public interface INestedImmtblClnblDictnrWrpprMapper : INestedClnblDictnrWrpprMapper
    {
    }

    public interface INestedImmtblClnblDictnrWrpprMapper<TKey, TClnbl, TImmtbl, TMtbl> : INestedClnblDictnrWrpprMapper<TKey, TClnbl, TImmtbl, TMtbl>, INestedImmtblClnblDictnrWrpprMapper
        where TClnbl : ICloneableObject
        where TImmtbl : class, TClnbl
        where TMtbl : class, TClnbl
    {
    }

    public class NestedImmtblClnblDictnrWrpprMapper<TKey, TClnbl, TImmtbl, TMtbl> : NestedClnblDictnrWrpprMapperBase<TKey, TClnbl, TImmtbl, TMtbl>, INestedImmtblClnblDictnrWrpprMapper<TKey, TClnbl, TImmtbl, TMtbl>
        where TClnbl : ICloneableObject
        where TImmtbl : class, TClnbl
        where TMtbl : class, TClnbl
    {
        public NestedImmtblClnblDictnrWrpprMapper(ICloneableMapper mapper, IClonnerFactory clonnerFactory) : base(mapper, clonnerFactory)
        {
        }

        public override INestedClnblDictnrWrppr<TKey, TClnbl, TImmtbl, TMtbl> GetTrgPropValue(
            INestedObjMapOpts<INestedClnblDictnrWrppr<TKey, TClnbl, TImmtbl, TMtbl>> opts)
        {
            var wrppr = opts.SrcPropValue;
            INestedClnblRdnlDictnr<TKey, TClnbl, TImmtbl> immtbl = null;

            if (wrppr != null)
            {
                immtbl = wrppr.Immtbl;

                if (immtbl == null && wrppr.Mtbl?.Mtbl != null)
                {
                    var dictnr = wrppr.Mtbl.Mtbl.ToDictionary(
                        kvp => kvp.Key, kvp => (INestedImmtblClnbl<TClnbl, TImmtbl>)new NestedImmtblClnbl<TClnbl, TImmtbl>(
                            Clonner.ToImmtbl(kvp.Value.Mtbl)));

                    immtbl = new NestedClnblRdnlDictnr<TKey, TClnbl, TImmtbl>(dictnr);
                }
            }

            var retWrppr = new NestedClnblDictnrWrppr<TKey, TClnbl, TImmtbl, TMtbl>(immtbl, null);
            return retWrppr;
        }
    }
}
