using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Turmerik.AspNetCore.Settings;

namespace Turmerik.Blazor.Core.Pages.Shared
{
    public abstract class MainLayoutComponentBase : LayoutComponentBase
    {
        protected bool SideBarLarge { get; set; } = false;
        protected string? SideBarSizeCssClass => SideBarLarge ? "trmrk-large" : "trmrk-small";

        protected void SizeChangedCallback(MouseEventArgs e)
        {
            SideBarLarge = !SideBarLarge;
        }
    }
}
