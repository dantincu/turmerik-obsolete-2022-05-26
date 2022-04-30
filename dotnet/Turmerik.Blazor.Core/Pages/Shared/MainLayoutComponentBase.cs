using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.WebUtilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Turmerik.AspNetCore.LocalSession;
using Turmerik.AspNetCore.Settings;

namespace Turmerik.Blazor.Core.Pages.Shared
{
    public abstract class MainLayoutComponentBase : LayoutComponentBase
    {
        protected ILocalSessionsManager LocalSessionsManager { get; set; }
        protected NavigationManager NavManager { get; set; }
        protected ITrmrkAppSettings AppSettings { get; set; }
        protected IHttpContextAccessor HttpContextAccessor { get; set; }
        protected bool SideBarLarge { get; set; } = false;
        protected string? SideBarSizeCssClass => SideBarLarge ? "trmrk-large" : "trmrk-small";
        protected Uri Uri { get; set; }
        protected Dictionary<string, Microsoft.Extensions.Primitives.StringValues> QueryStrings { get; set; }

        protected async override Task OnAfterRenderAsync(bool firstRender)
        {
            await base.OnAfterRenderAsync(firstRender);
            // await LocalSessionsManager.TryAddOrUpdateLocalSessionAsync();
        }
        protected async override Task OnInitializedAsync()
        {
            await base.OnInitializedAsync();

            Uri = NavManager.ToAbsoluteUri(NavManager.Uri);
            QueryStrings = QueryHelpers.ParseQuery(Uri.Query);
        }

        protected void SizeChangedCallback(MouseEventArgs e)
        {
            SideBarLarge = !SideBarLarge;
        }
    }
}
