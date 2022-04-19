using System;
using System.Collections.Generic;
using System.Text;

namespace Turmerik.Core.Data.Cloneable.Nested.Wrappers.Mappers
{
    public interface INestedMtblObjNmrblWrpprMapper : INestedObjNmrblWrpprMapper
    {
    }

    public class NestedMtblObjNmrblWrpprMapper : NestedObjNmrblWrpprMapperBase, INestedMtblObjNmrblWrpprMapper
    {
        public NestedMtblObjNmrblWrpprMapper(IServiceProvider services) : base(services)
        {
        }
    }
}
