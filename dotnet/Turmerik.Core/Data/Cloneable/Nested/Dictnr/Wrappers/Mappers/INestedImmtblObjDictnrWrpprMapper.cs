using System;
using System.Collections.Generic;
using System.Text;
using Turmerik.Core.Data.Cloneable.Nested.Wrappers;
using Turmerik.Core.Data.Cloneable.Nested.Wrappers.Mappers;
using Turmerik.Core.Infrastucture;

namespace Turmerik.Core.Data.Cloneable.Nested.Dictnr.Wrappers.Mappers
{
    public interface INestedImmtblObjDictnrWrpprMapper<TKey, TObj, TImmtbl, TMtbl> : INestedObjDictnrWrpprMapper<TKey, TObj, TImmtbl, TMtbl>
        where TImmtbl : TObj
        where TMtbl : TObj
    {
    }

    public class NestedImmtblObjDictnrWrpprMapper<TKey, TObj, TImmtbl, TMtbl> : NestedObjDictnrWrpprMapperBase<TKey, TObj, TImmtbl, TMtbl>, INestedImmtblObjDictnrWrpprMapper<TKey, TObj, TImmtbl, TMtbl>
        where TImmtbl : TObj
        where TMtbl : TObj
    {
        public NestedImmtblObjDictnrWrpprMapper(IServiceProvider services) : base(services)
        {
        }
    }
}
