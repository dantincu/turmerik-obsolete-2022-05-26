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

    public interface INestedImmtblObjWrpprMapperFactory
    {
        INestedImmtblObjWrpprMapper<TObj, TImmtbl, TMtbl> GetMapper<TObj, TImmtbl, TMtbl>()
            where TImmtbl : TObj
            where TMtbl : TObj;
    }

    public class NestedImmtblObjWrpprMapper<TObj, TImmtbl, TMtbl> : NestedObjWrpprMapperBase<TObj, TImmtbl, TMtbl>, INestedImmtblObjWrpprMapper<TObj, TImmtbl, TMtbl>
        where TImmtbl : TObj
        where TMtbl : TObj
    {
    }

    public class NestedImmtblObjWrpprMapperFactory : INestedImmtblObjWrpprMapperFactory
    {
        public INestedImmtblObjWrpprMapper<TObj, TImmtbl, TMtbl> GetMapper<TObj, TImmtbl, TMtbl>()
            where TImmtbl : TObj
            where TMtbl : TObj
        {
            throw new NotImplementedException();
        }
    }
}
