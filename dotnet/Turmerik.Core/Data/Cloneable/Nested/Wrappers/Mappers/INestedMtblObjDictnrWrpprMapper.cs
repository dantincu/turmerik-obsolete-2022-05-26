using System;
using System.Collections.Generic;
using System.Text;

namespace Turmerik.Core.Data.Cloneable.Nested.Wrappers.Mappers
{
    public interface INestedMtblObjDictnrWrpprMapper : INestedObjDictnrWrpprMapper
    {
    }

    public class NestedMtblObjDictnrWrpprMapper : NestedObjDictnrWrpprMapperBase, INestedMtblObjDictnrWrpprMapper
    {
        public NestedMtblObjDictnrWrpprMapper(IServiceProvider services) : base(services)
        {
        }
    }
}
