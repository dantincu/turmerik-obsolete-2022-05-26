using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Turmerik.Core.Components;

namespace Turmerik.Blazor.Core.Pages.Components
{
    public abstract class ComponentCoreBase : ComponentBase
    {
        public ComponentCoreBase()
        {
            Uuid = Guid.NewGuid();
            UuidStr = Uuid.ToString("N");
        }

        public Guid Uuid { get; }
        public string UuidStr { get; }
    }
}
