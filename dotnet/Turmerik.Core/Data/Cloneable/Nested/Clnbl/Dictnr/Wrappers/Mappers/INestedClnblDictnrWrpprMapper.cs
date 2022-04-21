using System;
using System.Collections.Generic;
using System.Text;
using Turmerik.Core.Data.Cloneable.Nested.Wrappers;
using Turmerik.Core.Data.Cloneable.Nested.Wrappers.Mappers;
using Turmerik.Core.Infrastucture;

namespace Turmerik.Core.Data.Cloneable.Nested.Clnbl.Dictnr.Wrappers.Mappers
{
    public interface INestedClnblDictnrWrpprMapper : INestedObjWrpprMapperCore
    {
    }

    public interface INestedClnblDictnrWrpprMapper<TOpts, TWrppr, TKey, TClnbl, TImmtbl, TMtbl> : INestedObjWrpprMapper<TOpts, TWrppr>, INestedClnblDictnrWrpprMapper
        where TOpts : INestedObjMapOpts<TWrppr>
        where TClnbl : ICloneableObject
        where TWrppr : INestedClnblDictnrWrppr<TKey, TClnbl, TImmtbl, TMtbl>
        where TImmtbl : TClnbl
        where TMtbl : TClnbl
    {
    }

    public interface INestedClnblDictnrWrpprMapper<TOpts, TKey, TClnbl, TImmtbl, TMtbl> : INestedClnblDictnrWrpprMapper<TOpts, INestedClnblDictnrWrppr<TKey, TClnbl, TImmtbl, TMtbl>, TKey, TClnbl, TImmtbl, TMtbl>
        where TOpts : INestedObjMapOpts<INestedClnblDictnrWrppr<TKey, TClnbl, TImmtbl, TMtbl>>
        where TClnbl : ICloneableObject
        where TImmtbl : TClnbl
        where TMtbl : TClnbl
    {
    }

    public interface INestedClnblDictnrWrpprMapper<TKey, TClnbl, TImmtbl, TMtbl> : INestedClnblDictnrWrpprMapper<INestedObjMapOpts<INestedClnblDictnrWrppr<TKey, TClnbl, TImmtbl, TMtbl>>, TKey, TClnbl, TImmtbl, TMtbl>
        where TClnbl : ICloneableObject
        where TImmtbl : TClnbl
        where TMtbl : TClnbl
    {
    }

    public abstract class NestedClnblDictnrWrpprMapperBase<TKey, TClnbl, TImmtbl, TMtbl> : INestedClnblDictnrWrpprMapper<TKey, TClnbl, TImmtbl, TMtbl>
        where TClnbl : ICloneableObject
        where TImmtbl : TClnbl
        where TMtbl : TClnbl
    {
        protected readonly IClonnerComponent<TClnbl, TImmtbl, TMtbl> Clonner;

        protected NestedClnblDictnrWrpprMapperBase(IClonnerComponent<TClnbl, TImmtbl, TMtbl> clonner)
        {
            this.Clonner = clonner ?? throw new ArgumentNullException(nameof(clonner));
        }

        public abstract INestedClnblDictnrWrppr<TKey, TClnbl, TImmtbl, TMtbl> GetTrgPropValue(INestedObjMapOpts<INestedClnblDictnrWrppr<TKey, TClnbl, TImmtbl, TMtbl>> opts);

        public INestedObjWrpprCore GetTrgPropValue(INestedObjMapOptsCore opts)
        {
            var options = (INestedObjMapOpts<INestedClnblDictnrWrppr<TKey, TClnbl, TImmtbl, TMtbl>>)opts;
            var retVal = GetTrgPropValue(options);

            return retVal;
        }
    }
}
