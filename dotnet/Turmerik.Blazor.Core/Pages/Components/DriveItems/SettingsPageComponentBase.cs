using Microsoft.AspNetCore.Components.Web;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Turmerik.AspNetCore.Infrastructure;

namespace Turmerik.Blazor.Core.Pages.Components.DriveItems
{
    public abstract class SettingsPageComponentBase : ComponentCoreBase
    {
        protected string LocalDiskExplorerBackgroundAppUri { get; set; }

        protected async Task OnSubmitLocalDiskExplorerBackgroundAppUri(TextEventArgsWrapper args)
        {

        }
    }
}
