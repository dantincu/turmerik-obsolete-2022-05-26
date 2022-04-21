using System;
using System.Collections.Generic;
using System.Text;
using Turmerik.Core.Infrastucture;

namespace Turmerik.Core.Data.Cloneable.Nested.Wrappers.Mappers
{
    public interface INestedMtblObjWrpprMapper : INestedObjWrpprMapperCore
    {
    }

    public interface INestedMtblObjWrpprMapper<TObj> : INestedObjWrpprMapper<TObj>, INestedMtblObjWrpprMapper
    {
    }

    public class NestedMtblObjWrpprMapper<TObj> : NestedObjWrpprMapper<TObj>, INestedMtblObjWrpprMapper<TObj>
    {
    }
}
