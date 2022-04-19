using System;
using System.Collections.Generic;
using System.Text;

namespace Turmerik.Core.Data.Cloneable.Nested.Wrappers.Mappers
{
    public interface INestedImmtblObjDictnrWrpprMapper : INestedObjDictnrWrpprMapper
    {
    }

    public class NestedImmtblObjDictnrWrpprMapper : NestedObjDictnrWrpprMapperBase, INestedImmtblObjDictnrWrpprMapper
    {
        public NestedImmtblObjDictnrWrpprMapper(IServiceProvider services) : base(services)
        {
        }
    }
}
