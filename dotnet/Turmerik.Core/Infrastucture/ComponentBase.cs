using System;
using System.Collections.Generic;
using System.Text;
using Turmerik.Core.Cloneable;

namespace Turmerik.Core.Infrastucture
{
    public abstract class ComponentBase
    {
        protected readonly ICloneableMapper Mapper;

        protected ComponentBase(ICloneableMapper mapper)
        {
            Mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }
    }
}
