using System;
using System.Collections.Generic;
using System.Text;
using Turmerik.Core.Infrastucture;

namespace Turmerik.Core.Data.Cloneable.Nested.Wrappers.Mappers
{
    public interface INestedImmtblObjWrpprMapper<TObj, TImmtbl, TMtbl> : INestedObjWrpprMapper<TObj, TImmtbl, TMtbl>
        where TImmtbl : TObj
        where TMtbl : TObj
    {
    }

    public class NestedImmtblObjWrpprMapper<TObj, TImmtbl, TMtbl> : NestedObjWrpprMapperBase<TObj, TImmtbl, TMtbl>, INestedImmtblObjWrpprMapper<TObj, TImmtbl, TMtbl>
        where TImmtbl : TObj
        where TMtbl : TObj
    {
        public NestedImmtblObjWrpprMapper(IServiceProvider services) : base(services)
        {
        }
    }
}
