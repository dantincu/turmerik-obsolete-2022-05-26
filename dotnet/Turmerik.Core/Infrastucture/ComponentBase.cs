using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace Turmerik.Core.Infrastucture
{
    public abstract class ComponentBase
    {
        protected ComponentBase(IServiceProvider services)
        {
            Services = services ?? throw new ArgumentNullException(nameof(services));
        }

        protected IServiceProvider Services { get; }
    }
}
