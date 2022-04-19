using System;
using System.Collections.Generic;
using System.Text;

namespace Turmerik.Core.Data.Cloneable.Nested.Wrappers.Mappers
{
    public interface INestedMtblObjWrpprMapper : INestedObjWrpprMapper
    {
    }

    public class NestedMtblObjWrpprMapper : NestedObjWrpprMapperBase, INestedMtblObjWrpprMapper
    {
        public NestedMtblObjWrpprMapper(IServiceProvider services) : base(services)
        {
        }
    }
}
