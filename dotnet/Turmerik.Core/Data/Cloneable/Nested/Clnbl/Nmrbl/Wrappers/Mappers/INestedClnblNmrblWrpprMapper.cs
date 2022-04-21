using System;
using System.Collections.Generic;
using System.Text;
using Turmerik.Core.Data.Cloneable.Nested.Clnbl.Wrappers;
using Turmerik.Core.Data.Cloneable.Nested.Clnbl.Wrappers.Mappers;
using Turmerik.Core.Data.Cloneable.Nested.Wrappers;
using Turmerik.Core.Data.Cloneable.Nested.Wrappers.Mappers;
using Turmerik.Core.Infrastucture;

namespace Turmerik.Core.Data.Cloneable.Nested.Clnbl.Nmrbl.Wrappers.Mappers
{
    public interface INestedClnblNmrblWrpprMapper : INestedObjWrpprMapperCore
    {
    }

    public interface INestedClnblNmrblWrpprMapper<TOpts, TWrppr, TClnbl, TImmtbl, TMtbl> : INestedObjWrpprMapper<TOpts, TWrppr>, INestedClnblNmrblWrpprMapper
        where TOpts : INestedObjMapOpts<TWrppr>
        where TClnbl : ICloneableObject
        where TWrppr : INestedClnblNmrblWrppr<TClnbl, TImmtbl, TMtbl>
        where TImmtbl : class, TClnbl
        where TMtbl : class, TClnbl
    {
    }

    public interface INestedClnblNmrblWrpprMapper<TOpts, TClnbl, TImmtbl, TMtbl> : INestedClnblNmrblWrpprMapper<TOpts, INestedClnblNmrblWrppr<TClnbl, TImmtbl, TMtbl>, TClnbl, TImmtbl, TMtbl>
        where TOpts : INestedObjMapOpts<INestedClnblNmrblWrppr<TClnbl, TImmtbl, TMtbl>>
        where TClnbl : ICloneableObject
        where TImmtbl : class, TClnbl
        where TMtbl : class, TClnbl
    {
    }

    public interface INestedClnblNmrblWrpprMapper<TClnbl, TImmtbl, TMtbl> : INestedClnblNmrblWrpprMapper<INestedObjMapOpts<INestedClnblNmrblWrppr<TClnbl, TImmtbl, TMtbl>>, TClnbl, TImmtbl, TMtbl>
        where TClnbl : ICloneableObject
        where TImmtbl : class, TClnbl
        where TMtbl : class, TClnbl
    {
    }

    public abstract class NestedClnblNmrblWrpprMapperBase<TClnbl, TImmtbl, TMtbl> : INestedClnblNmrblWrpprMapper<TClnbl, TImmtbl, TMtbl>
        where TClnbl : ICloneableObject
        where TImmtbl : class, TClnbl
        where TMtbl : class, TClnbl
    {
        protected readonly IClonnerComponent<TClnbl, TImmtbl, TMtbl> Clonner;

        public NestedClnblNmrblWrpprMapperBase(ICloneableMapper mapper, IClonnerFactory clonnerFactory)
        {
            Clonner = clonnerFactory.GetClonner<TClnbl, TImmtbl, TMtbl>(mapper);
        }

        public abstract INestedClnblNmrblWrppr<TClnbl, TImmtbl, TMtbl> GetTrgPropValue(INestedObjMapOpts<INestedClnblNmrblWrppr<TClnbl, TImmtbl, TMtbl>> opts);

        public INestedObjWrpprCore GetTrgPropValue(INestedObjMapOptsCore opts)
        {
            var options = (INestedObjMapOpts<INestedClnblNmrblWrppr<TClnbl, TImmtbl, TMtbl>>)opts;
            var wrppr = GetTrgPropValue(options);

            return wrppr;
        }
    }
}
