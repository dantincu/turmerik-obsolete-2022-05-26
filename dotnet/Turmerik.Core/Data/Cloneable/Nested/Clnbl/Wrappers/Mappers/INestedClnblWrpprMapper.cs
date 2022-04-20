using System;
using System.Collections.Generic;
using System.Text;
using Turmerik.Core.Data.Cloneable.Nested.Wrappers.Mappers;
using Turmerik.Core.Infrastucture;

namespace Turmerik.Core.Data.Cloneable.Nested.Clnbl.Wrappers.Mappers
{
    public interface INestedClnblWrpprMapper<TOpts, TWrppr, TClnbl, TImmtbl, TMtbl> : INestedObjWrpprMapper<TOpts, TWrppr>
        where TOpts : INestedClnblMapOpts<TWrppr, TClnbl, TImmtbl, TMtbl>
        where TClnbl : ICloneableObject
        where TWrppr : INestedClnblWrppr<TClnbl, TImmtbl, TMtbl>
        where TImmtbl : TClnbl
        where TMtbl : TClnbl
    {
    }

    public interface INestedClnblWrpprMapper<TOpts, TClnbl, TImmtbl, TMtbl> : INestedClnblWrpprMapper<TOpts, INestedClnblWrppr<TClnbl, TImmtbl, TMtbl>, TClnbl, TImmtbl, TMtbl>
        where TOpts : INestedClnblMapOpts<TClnbl, TImmtbl, TMtbl>
        where TClnbl : ICloneableObject
        where TImmtbl : TClnbl
        where TMtbl : TClnbl
    {
    }

    public interface INestedClnblWrpprMapper<TClnbl, TImmtbl, TMtbl> : INestedClnblWrpprMapper<INestedClnblMapOpts<TClnbl, TImmtbl, TMtbl>, TClnbl, TImmtbl, TMtbl>
        where TClnbl : ICloneableObject
        where TImmtbl : TClnbl
        where TMtbl : TClnbl
    {
    }

    public interface INestedClnblMapOpts<TWrppr, TClnbl, TImmtbl, TMtbl> : INestedObjMapOpts<TWrppr>
        where TClnbl : ICloneableObject
        where TWrppr : INestedClnblWrpprCore<TClnbl, TImmtbl, TMtbl>
        where TImmtbl : TClnbl
        where TMtbl : TClnbl
    {
    }

    public interface INestedClnblMapOpts<TClnbl, TImmtbl, TMtbl> : INestedClnblMapOpts<INestedClnblWrppr<TClnbl, TImmtbl, TMtbl>, TClnbl, TImmtbl, TMtbl>
        where TClnbl : ICloneableObject
        where TImmtbl : TClnbl
        where TMtbl : TClnbl
    {
    }

    public class NestedClnblMapOptsImmtbl<TClnbl, TImmtbl, TMtbl> : NestedObjMapOptsImmtbl<INestedClnblWrppr<TClnbl, TImmtbl, TMtbl>>, INestedClnblMapOpts<TClnbl, TImmtbl, TMtbl>
        where TClnbl : ICloneableObject
        where TImmtbl : TClnbl
        where TMtbl : TClnbl
    {
        public NestedClnblMapOptsImmtbl(INestedClnblMapOpts<TClnbl, TImmtbl, TMtbl> src) : base(src)
        {
        }
    }

    public class NestedClnblMapOptsMtbl<TClnbl, TImmtbl, TMtbl> : NestedObjMapOptsMtbl<INestedClnblWrppr<TClnbl, TImmtbl, TMtbl>>, INestedClnblMapOpts<TClnbl, TImmtbl, TMtbl>
        where TClnbl : ICloneableObject
        where TImmtbl : TClnbl
        where TMtbl : TClnbl
    {
        public NestedClnblMapOptsMtbl(INestedClnblMapOpts<TClnbl, TImmtbl, TMtbl> src) : base(src)
        {
        }
    }

    public abstract class NestedClnblWrpprMapperBase<TClnbl, TImmtbl, TMtbl> : INestedClnblWrpprMapper<TClnbl, TImmtbl, TMtbl>
        where TClnbl : ICloneableObject
        where TImmtbl : TClnbl
        where TMtbl : TClnbl
    {
        public INestedClnblWrppr<TClnbl, TImmtbl, TMtbl> GetTrgPropValue(INestedClnblMapOpts<TClnbl, TImmtbl, TMtbl> opts)
        {
            throw new NotImplementedException();
        }
    }
}
