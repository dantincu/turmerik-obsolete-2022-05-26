using System;
using System.Collections.Generic;
using System.Text;
using Turmerik.Core.Data.Cloneable.Nested.Clnbl.Wrappers;
using Turmerik.Core.Data.Cloneable.Nested.Clnbl.Wrappers.Mappers;
using Turmerik.Core.Data.Cloneable.Nested.Wrappers.Mappers;
using Turmerik.Core.Infrastucture;

namespace Turmerik.Core.Data.Cloneable.Nested.Clnbl.Nmrbl.Wrappers.Mappers
{
    public interface INestedClnblNmrblWrpprMapper<TOpts, TWrppr, TClnbl, TImmtbl, TMtbl> : INestedObjWrpprMapper<TOpts, TWrppr>
        where TOpts : INestedClnblNmrblMapOpts<TWrppr, TClnbl, TImmtbl, TMtbl>
        where TClnbl : ICloneableObject
        where TWrppr : INestedClnblNmrblWrppr<TClnbl, TImmtbl, TMtbl>
        where TImmtbl : TClnbl
        where TMtbl : TClnbl
    {
    }

    public interface INestedClnblNmrblWrpprMapper<TOpts, TClnbl, TImmtbl, TMtbl> : INestedClnblNmrblWrpprMapper<TOpts, INestedClnblNmrblWrppr<TClnbl, TImmtbl, TMtbl>, TClnbl, TImmtbl, TMtbl>
        where TOpts : INestedClnblNmrblMapOpts<TClnbl, TImmtbl, TMtbl>
        where TClnbl : ICloneableObject
        where TImmtbl : TClnbl
        where TMtbl : TClnbl
    {
    }

    public interface INestedClnblNmrblWrpprMapper<TClnbl, TImmtbl, TMtbl> : INestedClnblNmrblWrpprMapper<INestedClnblNmrblMapOpts<TClnbl, TImmtbl, TMtbl>, TClnbl, TImmtbl, TMtbl>
        where TClnbl : ICloneableObject
        where TImmtbl : TClnbl
        where TMtbl : TClnbl
    {
    }

    public interface INestedClnblNmrblMapOpts<TWrppr, TClnbl, TImmtbl, TMtbl> : INestedObjMapOpts<TWrppr>
        where TClnbl : ICloneableObject
        where TWrppr : INestedClnblNmrblWrppr<TClnbl, TImmtbl, TMtbl>
        where TImmtbl : TClnbl
        where TMtbl : TClnbl
    {
    }

    public interface INestedClnblNmrblMapOpts<TClnbl, TImmtbl, TMtbl> : INestedClnblNmrblMapOpts<INestedClnblNmrblWrppr<TClnbl, TImmtbl, TMtbl>, TClnbl, TImmtbl, TMtbl>
        where TClnbl : ICloneableObject
        where TImmtbl : TClnbl
        where TMtbl : TClnbl
    {
    }

    public class NestedClnblNmrblMapOptsImmtbl<TClnbl, TImmtbl, TMtbl> : NestedObjMapOptsImmtbl<INestedClnblNmrblWrppr<TClnbl, TImmtbl, TMtbl>>, INestedClnblNmrblMapOpts<TClnbl, TImmtbl, TMtbl>
        where TClnbl : ICloneableObject
        where TImmtbl : TClnbl
        where TMtbl : TClnbl
    {
        public NestedClnblNmrblMapOptsImmtbl(INestedClnblNmrblMapOpts<TClnbl, TImmtbl, TMtbl> src) : base(src)
        {
        }
    }

    public class NestedClnblNmrblMapOptsMtbl<TClnbl, TImmtbl, TMtbl> : NestedObjMapOptsMtbl<INestedClnblNmrblWrppr<TClnbl, TImmtbl, TMtbl>>, INestedClnblNmrblMapOpts<TClnbl, TImmtbl, TMtbl>
        where TClnbl : ICloneableObject
        where TImmtbl : TClnbl
        where TMtbl : TClnbl
    {
        public NestedClnblNmrblMapOptsMtbl(INestedClnblNmrblMapOpts<TClnbl, TImmtbl, TMtbl> src) : base(src)
        {
        }
    }

    public abstract class NestedClnblNmrblWrpprMapperBase<TClnbl, TImmtbl, TMtbl> : ComponentBase, INestedClnblNmrblWrpprMapper<TClnbl, TImmtbl, TMtbl>
        where TClnbl : ICloneableObject
        where TImmtbl : TClnbl
        where TMtbl : TClnbl
    {
        public NestedClnblNmrblWrpprMapperBase(IServiceProvider services) : base(services)
        {
        }

        public INestedClnblNmrblWrppr<TClnbl, TImmtbl, TMtbl> GetTrgPropValue(INestedClnblNmrblMapOpts<TClnbl, TImmtbl, TMtbl> opts)
        {
            throw new NotImplementedException();
        }
    }
}
