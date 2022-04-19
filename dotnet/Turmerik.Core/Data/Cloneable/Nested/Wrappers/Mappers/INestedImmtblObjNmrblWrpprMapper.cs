using System;
using System.Collections.Generic;
using System.Text;

namespace Turmerik.Core.Data.Cloneable.Nested.Wrappers.Mappers
{
    public interface INestedImmtblObjNmrblWrpprMapper : INestedObjNmrblWrpprMapper
    {
    }

    public class NestedImmtblObjNmrblWrpprMapper : NestedObjNmrblWrpprMapperBase, INestedImmtblObjNmrblWrpprMapper
    {
        public NestedImmtblObjNmrblWrpprMapper(IServiceProvider services) : base(services)
        {
        }
    }
}
