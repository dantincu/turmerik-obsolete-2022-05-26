using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;
using Turmerik.Core.Data.Cloneable.Nested.Wrappers.Mappers;

namespace Turmerik.Core.Data.Cloneable.Mappers
{
    public interface ICloneableImmtblMapper : ICloneableMapperCore
    {
    }

    public class CloneableImmtblMapper : CloneableMapperCoreBase, ICloneableImmtblMapper
    {
        public CloneableImmtblMapper(IServiceProvider services) : base(services)
        {
        }

        protected override INestedObjWrpprMapperCore GetNestedObjWrpprMapper(INestedObjMapOpts opts)
        {
            throw new NotImplementedException();
        }
    }
}
