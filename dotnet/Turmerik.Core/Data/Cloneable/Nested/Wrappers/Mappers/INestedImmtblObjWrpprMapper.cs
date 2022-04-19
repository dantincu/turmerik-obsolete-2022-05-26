using System;
using System.Collections.Generic;
using System.Text;

namespace Turmerik.Core.Data.Cloneable.Nested.Wrappers.Mappers
{
    public interface INestedImmtblObjWrpprMapper : INestedObjWrpprMapper
    {
    }

    public class NestedImmtblObjWrpprMapper : NestedObjWrpprMapperBase, INestedImmtblObjWrpprMapper
    {
        public NestedImmtblObjWrpprMapper(IServiceProvider services) : base(services)
        {
        }
    }
}
