using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;
using Turmerik.Core.Infrastucture;

namespace Turmerik.Core.Data.Cloneable.Nested.Wrappers.Mappers
{
    public interface INestedObjWrpprMapper : INestedObjWrpprMapperCore
    {
    }

    public abstract class NestedObjWrpprMapperBase : ComponentBase, INestedObjWrpprMapper
    {
        public NestedObjWrpprMapperBase(IServiceProvider services) : base(services)
        {
        }

        public INestedObjWrpprCore GetTrgPropValue(INestedObjMapOpts opts)
        {
            throw new NotImplementedException();
        }
    }
}
