using System;
using System.Collections.Generic;
using System.Text;
using Turmerik.Core.Cloneable;

namespace Turmerik.Core.Infrastucture
{
    public abstract class ComponentBase
    {
        protected readonly ICloneableMapper ClblMppr;

        protected ComponentBase(ICloneableMapper clblMapper)
        {
            ClblMppr = clblMapper ?? throw new ArgumentNullException(nameof(clblMapper));
        }
    }
}
