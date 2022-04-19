using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;
using Turmerik.Core.Infrastucture;

namespace Turmerik.Core.Data.Cloneable.Nested.Wrappers.Mappers
{
    public interface INestedObjDictnrWrpprMapper : INestedObjWrpprMapperCore
    {
    }

    public abstract class NestedObjDictnrWrpprMapperBase : ComponentBase, INestedObjDictnrWrpprMapper
    {
        public NestedObjDictnrWrpprMapperBase(IServiceProvider services) : base(services)
        {
        }

        public INestedObjWrpprCore GetTrgPropValue(INestedObjMapOpts opts)
        {
            throw new NotImplementedException();
        }
    }
}
