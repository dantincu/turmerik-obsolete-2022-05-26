using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;
using Turmerik.Core.Data.Cloneable.Nested.Clnbl.Dictnr.Wrappers;
using Turmerik.Core.Data.Cloneable.Nested.Clnbl.Nmrbl.Wrappers;
using Turmerik.Core.Data.Cloneable.Nested.Clnbl.Wrappers;
using Turmerik.Core.Data.Cloneable.Nested.Dictnr.Wrappers;
using Turmerik.Core.Data.Cloneable.Nested.Nmrbl.Wrappers;
using Turmerik.Core.Infrastucture;

namespace Turmerik.Core.Data.Cloneable.Nested.Wrappers.Mappers
{
    public interface INestedObjWrpprMapper<TOpts, TWrppr>
        where TOpts : INestedObjMapOpts<TWrppr>
        where TWrppr : INestedObjWrpprCore
    {
        TWrppr GetTrgPropValue(TOpts opts);
    }

    public interface INestedObjWrpprMapper<TOpts> : INestedObjWrpprMapper<TOpts, INestedObjWrppr>
        where TOpts : INestedObjMapOpts<INestedObjWrppr>
    {
    }

    public interface INestedObjWrpprMapper : INestedObjWrpprMapper<INestedObjMapOpts>
    {
    }

    public interface INestedObjWrpprMapper<TOpts, TWrppr, TObj, TImmtbl, TMtbl> : INestedObjWrpprMapper<TOpts, TWrppr>
        where TOpts : INestedObjMapOpts<TWrppr, TObj, TImmtbl, TMtbl>
        where TWrppr : INestedObjWrppr<TObj, TImmtbl, TMtbl>
        where TImmtbl : TObj
        where TMtbl : TObj
    {
    }

    public interface INestedObjWrpprMapper<TOpts, TObj, TImmtbl, TMtbl> : INestedObjWrpprMapper<TOpts, INestedObjWrppr<TObj, TImmtbl, TMtbl>, TObj, TImmtbl, TMtbl>
        where TOpts : INestedObjMapOpts<INestedObjWrppr<TObj, TImmtbl, TMtbl>, TObj, TImmtbl, TMtbl>
        where TImmtbl : TObj
        where TMtbl : TObj
    {
    }

    public interface INestedObjWrpprMapper<TObj, TImmtbl, TMtbl> : INestedObjWrpprMapper<INestedObjMapOpts<TObj, TImmtbl, TMtbl>, TObj, TImmtbl, TMtbl>
        where TImmtbl : TObj
        where TMtbl : TObj
    {
    }

    public interface INestedObjNmrblWrpprMapper<TOpts, TWrppr, TObj, TImmtbl, TMtbl> : INestedObjWrpprMapper<TOpts, TWrppr>
        where TOpts : INestedObjNmrblMapOpts<TWrppr, TObj, TImmtbl, TMtbl>
        where TWrppr : INestedObjNmrblWrppr<TObj, TImmtbl, TMtbl>
        where TImmtbl : TObj
        where TMtbl : TObj
    {
    }

    public interface INestedObjNmrblWrpprMapper<TOpts, TObj, TImmtbl, TMtbl> : INestedObjNmrblWrpprMapper<TOpts, INestedObjNmrblWrppr<TObj, TImmtbl, TMtbl>, TObj, TImmtbl, TMtbl>
        where TOpts : INestedObjNmrblMapOpts<INestedObjNmrblWrppr<TObj, TImmtbl, TMtbl>, TObj, TImmtbl, TMtbl>
        where TImmtbl : TObj
        where TMtbl : TObj
    {
    }

    public interface INestedObjNmrblWrpprMapper<TObj, TImmtbl, TMtbl> : INestedObjNmrblWrpprMapper<INestedObjNmrblMapOpts<TObj, TImmtbl, TMtbl>, TObj, TImmtbl, TMtbl>
        where TImmtbl : TObj
        where TMtbl : TObj
    {
    }

    public interface INestedObjDictnrWrpprMapper<TOpts, TWrppr, TKey, TObj, TImmtbl, TMtbl> : INestedObjWrpprMapper<TOpts, TWrppr>
        where TOpts : INestedObjDictnrMapOpts<TWrppr, TKey, TObj, TImmtbl, TMtbl>
        where TWrppr : INestedObjDictnrWrppr<TKey, TObj, TImmtbl, TMtbl>
        where TImmtbl : TObj
        where TMtbl : TObj
    {
    }

    public interface INestedObjDictnrWrpprMapper<TOpts, TKey, TObj, TImmtbl, TMtbl> : INestedObjDictnrWrpprMapper<TOpts, INestedObjDictnrWrppr<TKey, TObj, TImmtbl, TMtbl>, TKey, TObj, TImmtbl, TMtbl>
        where TOpts : INestedObjDictnrMapOpts<TKey, TObj, TImmtbl, TMtbl>
        where TImmtbl : TObj
        where TMtbl : TObj
    {
    }

    public interface INestedObjDictnrWrpprMapper<TKey, TObj, TImmtbl, TMtbl> : INestedObjDictnrWrpprMapper<INestedObjDictnrMapOpts<TKey, TObj, TImmtbl, TMtbl>, TKey, TObj, TImmtbl, TMtbl>
        where TImmtbl : TObj
        where TMtbl : TObj
    {
    }

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

    public interface INestedObjMapOptsCore
    {
        Type SrcPropType { get; }
        Type TrgPropType { get; }
    }

    public interface INestedObjMapOpts<TWrppr> : INestedObjMapOptsCore
        where TWrppr : INestedObjWrpprCore
    {
        TWrppr SrcPropValue { get; }
    }

    public interface INestedObjMapOpts<TWrppr, TObj, TImmtbl, TMtbl> : INestedObjMapOpts<TWrppr>
        where TWrppr : INestedObjWrpprCore<TObj, TImmtbl, TMtbl>
        where TImmtbl : TObj
        where TMtbl : TObj
    {
    }

    public interface INestedObjMapOpts<TObj, TImmtbl, TMtbl> : INestedObjMapOpts<INestedObjWrppr<TObj, TImmtbl, TMtbl>>, INestedObjMapOpts<INestedObjWrppr<TObj, TImmtbl, TMtbl>, TObj, TImmtbl, TMtbl>
        where TImmtbl : TObj
        where TMtbl : TObj
    {
    }

    public interface INestedObjNmrblMapOpts<TWrppr, TObj, TImmtbl, TMtbl> : INestedObjMapOpts<TWrppr>
        where TWrppr : INestedObjNmrblWrppr<TObj, TImmtbl, TMtbl>
        where TImmtbl : TObj
        where TMtbl : TObj
    {
    }

    public interface INestedObjNmrblMapOpts<TObj, TImmtbl, TMtbl> : INestedObjNmrblMapOpts<INestedObjNmrblWrppr<TObj, TImmtbl, TMtbl>, TObj, TImmtbl, TMtbl>
        where TImmtbl : TObj
        where TMtbl : TObj
    {
    }

    public interface INestedObjDictnrMapOpts<TWrppr, TKey, TObj, TImmtbl, TMtbl> : INestedObjMapOpts<TWrppr>
        where TWrppr : INestedObjDictnrWrppr<TKey, TObj, TImmtbl, TMtbl>
        where TImmtbl : TObj
        where TMtbl : TObj
    {
    }

    public interface INestedObjDictnrMapOpts<TKey, TObj, TImmtbl, TMtbl> : INestedObjDictnrMapOpts<INestedObjDictnrWrppr<TKey, TObj, TImmtbl, TMtbl>, TKey, TObj, TImmtbl, TMtbl>
        where TImmtbl : TObj
        where TMtbl : TObj
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

    public interface INestedObjMapOpts : INestedObjMapOpts<INestedObjWrppr>
    {
    }

    public abstract class NestedObjMapOptsCoreImmtblBase : INestedObjMapOptsCore
    {
        protected NestedObjMapOptsCoreImmtblBase(INestedObjMapOptsCore src)
        {
            SrcPropType = src.SrcPropType;
            TrgPropType = src.TrgPropType;
        }

        public Type SrcPropType { get; }
        public Type TrgPropType { get; }
    }

    public abstract class NestedObjMapOptsCoreMtblBase : INestedObjMapOptsCore
    {
        protected NestedObjMapOptsCoreMtblBase()
        {
        }

        protected NestedObjMapOptsCoreMtblBase(INestedObjMapOptsCore src)
        {
            SrcPropType = src.SrcPropType;
            TrgPropType = src.TrgPropType;
        }

        public Type SrcPropType { get; set; }
        public Type TrgPropType { get; set; }
    }

    public class NestedObjMapOptsImmtbl<TWrppr> : NestedObjMapOptsCoreImmtblBase, INestedObjMapOpts<TWrppr>
        where TWrppr : INestedObjWrpprCore
    {
        public NestedObjMapOptsImmtbl(INestedObjMapOpts<TWrppr> src) : base(src)
        {
            SrcPropValue = src.SrcPropValue;
        }

        public TWrppr SrcPropValue { get; }
    }

    public class NestedObjMapOptsMtbl<TWrppr> : NestedObjMapOptsCoreMtblBase, INestedObjMapOpts<TWrppr>
        where TWrppr : INestedObjWrpprCore
    {
        public NestedObjMapOptsMtbl()
        {
        }

        public NestedObjMapOptsMtbl(INestedObjMapOpts<TWrppr> src) : base(src)
        {
            SrcPropValue = src.SrcPropValue;
        }

        public TWrppr SrcPropValue { get; set; }
    }

    public class NestedObjMapOptsImmtbl : NestedObjMapOptsCoreImmtblBase, INestedObjMapOpts
    {
        public NestedObjMapOptsImmtbl(INestedObjMapOpts src) : base(src)
        {
            SrcPropValue = src.SrcPropValue;
        }

        public INestedObjWrppr SrcPropValue { get; }
    }

    public class NestedObjMapOptsMtbl : NestedObjMapOptsCoreMtblBase, INestedObjMapOpts
    {
        public NestedObjMapOptsMtbl()
        {
        }

        public NestedObjMapOptsMtbl(INestedObjMapOpts src) : base(src)
        {
            SrcPropValue = src.SrcPropValue;
        }

        public INestedObjWrppr SrcPropValue { get; set; }
    }

    public class NestedObjMapOptsImmtbl<TObj, TImmtbl, TMtbl> : NestedObjMapOptsImmtbl<INestedObjWrppr<TObj, TImmtbl, TMtbl>>, INestedObjMapOpts<TObj, TImmtbl, TMtbl>
        where TImmtbl : TObj
        where TMtbl : TObj
    {
        public NestedObjMapOptsImmtbl(INestedObjMapOpts<INestedObjWrppr<TObj, TImmtbl, TMtbl>, TObj, TImmtbl, TMtbl> src) : base(src)
        {
        }
    }

    public class NestedObjMapOptsMtbl<TObj, TImmtbl, TMtbl> : NestedObjMapOptsMtbl<INestedObjWrppr<TObj, TImmtbl, TMtbl>>, INestedObjMapOpts<TObj, TImmtbl, TMtbl>
        where TImmtbl : TObj
        where TMtbl : TObj
    {
        public NestedObjMapOptsMtbl(INestedObjMapOpts<INestedObjWrppr<TObj, TImmtbl, TMtbl>, TObj, TImmtbl, TMtbl> src) : base(src)
        {
        }
    }

    public class NestedObjNmrblMapOptsImmtbl<TObj, TImmtbl, TMtbl> : NestedObjMapOptsImmtbl<INestedObjNmrblWrppr<TObj, TImmtbl, TMtbl>>, INestedObjNmrblMapOpts<TObj, TImmtbl, TMtbl>
        where TImmtbl : TObj
        where TMtbl : TObj
    {
        public NestedObjNmrblMapOptsImmtbl(INestedObjNmrblMapOpts<TObj, TImmtbl, TMtbl> src) : base(src)
        {
        }
    }

    public class NestedObjNmrblMapOptsMtbl<TObj, TImmtbl, TMtbl> : NestedObjMapOptsMtbl<INestedObjNmrblWrppr<TObj, TImmtbl, TMtbl>>, INestedObjNmrblMapOpts<TObj, TImmtbl, TMtbl>
        where TImmtbl : TObj
        where TMtbl : TObj
    {
        public NestedObjNmrblMapOptsMtbl()
        {
        }

        public NestedObjNmrblMapOptsMtbl(INestedObjNmrblMapOpts<TObj, TImmtbl, TMtbl> src) : base(src)
        {
        }
    }

    public class NestedObjDictnrMapOptsImmtbl<TKey, TObj, TImmtbl, TMtbl> : NestedObjMapOptsImmtbl<INestedObjDictnrWrppr<TKey, TObj, TImmtbl, TMtbl>>, INestedObjDictnrMapOpts<TKey, TObj, TImmtbl, TMtbl>
        where TImmtbl : TObj
        where TMtbl : TObj
    {
        public NestedObjDictnrMapOptsImmtbl(INestedObjDictnrMapOpts<TKey, TObj, TImmtbl, TMtbl> src) : base(src)
        {
        }
    }

    public class NestedObjDictnrMapOptsMtbl<TKey, TObj, TImmtbl, TMtbl> : NestedObjMapOptsMtbl<INestedObjDictnrWrppr<TKey, TObj, TImmtbl, TMtbl>>, INestedObjDictnrMapOpts<TKey, TObj, TImmtbl, TMtbl>
        where TImmtbl : TObj
        where TMtbl : TObj
    {
        public NestedObjDictnrMapOptsMtbl()
        {
        }

        public NestedObjDictnrMapOptsMtbl(INestedObjDictnrMapOpts<TKey, TObj, TImmtbl, TMtbl> src) : base(src)
        {
        }
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

    public abstract class NestedObjWrpprMapperBase<TObj, TImmtbl, TMtbl> : ComponentBase, INestedObjWrpprMapper<TObj, TImmtbl, TMtbl>
        where TImmtbl : TObj
        where TMtbl : TObj
    {
        public NestedObjWrpprMapperBase(IServiceProvider services) : base(services)
        {
        }

        public INestedObjWrppr<TObj, TImmtbl, TMtbl> GetTrgPropValue(INestedObjMapOpts<TObj, TImmtbl, TMtbl> opts)
        {
            throw new NotImplementedException();
        }
    }
}
