using System;
using System.Collections.Generic;
using System.Text;
using Turmerik.Core.Data.Cloneable.Nested.Wrappers;
using Turmerik.Core.Data.Cloneable.Nested.Wrappers.Mappers;
using Turmerik.Core.Infrastucture;

namespace Turmerik.Core.Data.Cloneable.Nested.Clnbl.Wrappers.Mappers
{
    public interface INestedClnblWrpprMapper : INestedObjWrpprMapperCore
    {
    }

    public interface INestedClnblWrpprMapper<TWrppr, TClnbl, TImmtbl, TMtbl> : INestedObjWrpprMapper<INestedObjMapOpts<TWrppr>, TWrppr, TClnbl>, INestedClnblWrpprMapper
        where TClnbl : class, ICloneableObject
        where TImmtbl : class, TClnbl
        where TMtbl : class, TClnbl
        where TWrppr : INestedClnblWrppr<TClnbl, TImmtbl, TMtbl>
    {
    }

    public interface INestedClnblWrpprMapper<TClnbl, TImmtbl, TMtbl> : INestedClnblWrpprMapper<INestedClnblWrppr<TClnbl, TImmtbl, TMtbl>, TClnbl, TImmtbl, TMtbl>
        where TClnbl : class, ICloneableObject
        where TImmtbl : class, TClnbl
        where TMtbl : class, TClnbl
    {
    }

    public interface INestedClnblWrpprMapper<TClnbl> : INestedClnblWrpprMapper<INestedClnblWrppr<TClnbl>, TClnbl, TClnbl, TClnbl>
        where TClnbl : class, ICloneableObject
    {
    }

    public abstract class NestedClnblWrpprMapperBase<TClnbl, TImmtbl, TMtbl> : INestedClnblWrpprMapper<TClnbl, TImmtbl, TMtbl>
        where TClnbl : class, ICloneableObject
        where TImmtbl : class, TClnbl
        where TMtbl : class, TClnbl
    {
        protected readonly IClonnerComponent<TClnbl, TImmtbl, TMtbl> Clonner;

        protected NestedClnblWrpprMapperBase(ICloneableMapper mapper, IClonnerFactory clonnerFactory)
        {
            Clonner = clonnerFactory.GetClonner<TClnbl, TImmtbl, TMtbl>(mapper);
        }

        public abstract INestedClnblWrppr<TClnbl, TImmtbl, TMtbl> GetTrgPropValue(INestedObjMapOpts<INestedClnblWrppr<TClnbl, TImmtbl, TMtbl>> opts);

        public INestedObjWrpprCore GetTrgPropValue(INestedObjMapOptsCore opts)
        {
            var options = (INestedObjMapOpts<INestedObjWrppr<INestedClnblWrppr<TClnbl>>>)opts;
            var retWrppr = GetTrgPropValue(options);

            return retWrppr;
        }
    }
}
