using System;
using System.Collections.Generic;
using System.Text;
using Turmerik.Core.Data.Cloneable.Nested.Wrappers.Mappers;
using Turmerik.Core.Infrastucture;

namespace Turmerik.Core.Data.Cloneable.Nested.Clnbl.Dictnr.Wrappers.Mappers
{
    public interface INestedClnblDictnrWrpprMapper<TOpts, TWrppr, TKey, TClnbl, TImmtbl, TMtbl> : INestedObjWrpprMapper<TOpts, TWrppr>
        where TOpts : INestedClnblDictnrMapOpts<TWrppr, TKey, TClnbl, TImmtbl, TMtbl>
        where TClnbl : ICloneableObject
        where TWrppr : INestedClnblDictnrWrppr<TKey, TClnbl, TImmtbl, TMtbl>
        where TImmtbl : TClnbl
        where TMtbl : TClnbl
    {
    }

    public interface INestedClnblDictnrWrpprMapper<TOpts, TKey, TClnbl, TImmtbl, TMtbl> : INestedClnblDictnrWrpprMapper<TOpts, INestedClnblDictnrWrppr<TKey, TClnbl, TImmtbl, TMtbl>, TKey, TClnbl, TImmtbl, TMtbl>
        where TOpts : INestedClnblDictnrMapOpts<TKey, TClnbl, TImmtbl, TMtbl>
        where TClnbl : ICloneableObject
        where TImmtbl : TClnbl
        where TMtbl : TClnbl
    {
    }

    public interface INestedClnblDictnrWrpprMapper<TKey, TClnbl, TImmtbl, TMtbl> : INestedClnblDictnrWrpprMapper<INestedClnblDictnrMapOpts<TKey, TClnbl, TImmtbl, TMtbl>, TKey, TClnbl, TImmtbl, TMtbl>
        where TClnbl : ICloneableObject
        where TImmtbl : TClnbl
        where TMtbl : TClnbl
    {
    }

    public interface INestedClnblDictnrMapOpts<TWrppr, TKey, TClnbl, TImmtbl, TMtbl> : INestedObjMapOpts<TWrppr>
        where TClnbl : ICloneableObject
        where TWrppr : INestedClnblDictnrWrppr<TKey, TClnbl, TImmtbl, TMtbl>
        where TImmtbl : TClnbl
        where TMtbl : TClnbl
    {
    }

    public interface INestedClnblDictnrMapOpts<TKey, TClnbl, TImmtbl, TMtbl> : INestedClnblDictnrMapOpts<INestedClnblDictnrWrppr<TKey, TClnbl, TImmtbl, TMtbl>, TKey, TClnbl, TImmtbl, TMtbl>
        where TClnbl : ICloneableObject
        where TImmtbl : TClnbl
        where TMtbl : TClnbl
    {
    }

    public class NestedClnblDictnrMapOptsImmtbl<TKey, TClnbl, TImmtbl, TMtbl> : NestedObjMapOptsImmtbl<INestedClnblDictnrWrppr<TKey, TClnbl, TImmtbl, TMtbl>>, INestedClnblDictnrMapOpts<TKey, TClnbl, TImmtbl, TMtbl>
        where TClnbl : ICloneableObject
        where TImmtbl : TClnbl
        where TMtbl : TClnbl
    {
        public NestedClnblDictnrMapOptsImmtbl(INestedClnblDictnrMapOpts<TKey, TClnbl, TImmtbl, TMtbl> src) : base(src)
        {
        }
    }

    public class NestedClnblDictnrMapOptsMtbl<TKey, TClnbl, TImmtbl, TMtbl> : NestedObjMapOptsMtbl<INestedClnblDictnrWrppr<TKey, TClnbl, TImmtbl, TMtbl>>, INestedClnblDictnrMapOpts<TKey, TClnbl, TImmtbl, TMtbl>
        where TClnbl : ICloneableObject
        where TImmtbl : TClnbl
        where TMtbl : TClnbl
    {
        public NestedClnblDictnrMapOptsMtbl(INestedClnblDictnrMapOpts<TKey, TClnbl, TImmtbl, TMtbl> src) : base(src)
        {
        }
    }

    public abstract class NestedClnblDictnrWrpprMapperBase<TKey, TClnbl, TImmtbl, TMtbl> : INestedClnblDictnrWrpprMapper<TKey, TClnbl, TImmtbl, TMtbl>
        where TClnbl : ICloneableObject
        where TImmtbl : TClnbl
        where TMtbl : TClnbl
    {
        public INestedClnblDictnrWrppr<TKey, TClnbl, TImmtbl, TMtbl> GetTrgPropValue(INestedClnblDictnrMapOpts<TKey, TClnbl, TImmtbl, TMtbl> opts)
        {
            throw new NotImplementedException();
        }
    }
}
