using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Http;
using Turmerik.AspNetCore.Infrastructure;
using Turmerik.AspNetCore.LocalSession;
using Turmerik.AspNetCore.Services;
using Turmerik.AspNetCore.Services.LocalSessionStorage;
using Turmerik.AspNetCore.Settings;

namespace Turmerik.Blazor.Core.Pages.Shared
{
    public abstract class MainLayoutComponentBase : LayoutComponentBase
    {
        protected ILocalSessionsManager LocalSessionsManager { get; set; }
        protected INavManager NavManager { get; set; }
        protected ITrmrkAppSettings AppSettings { get; set; }
        protected IHttpContextAccessor HttpContextAccessor { get; set; }
        protected ILocalStorageWrapper LocalStorage { get; set; }
        protected ISessionStorageWrapper SessionStorage { get; set; }
        protected IMainLayoutService MainLayoutService { get; set; }
        protected bool SideBarLarge { get; set; } = false;
        protected string? SideBarSizeCssClass => SideBarLarge ? CssClassH.Large : CssClassH.Small;
        protected Guid? LocalSessionGuid { get; set; }

        protected async override Task OnAfterRenderAsync(bool firstRender)
        {
            await base.OnAfterRenderAsync(firstRender);
            LocalSessionGuid = NavManager.LocalSessionGuid;

            if (!LocalSessionGuid.HasValue)
            {
                var localSessionData = await LocalSessionsManager.TryAddOrUpdateLocalSessionAsync(null);
                NavigateToLocalSessionId(localSessionData.LocalSessionGuid, true);
            }
        }

        protected void SizeChangedCallback(MouseEventArgs e)
        {
            SideBarLarge = !SideBarLarge;
            MainLayoutService.SideBarSizeChanged(SideBarLarge);
        }

        protected void NavigateToLocalSessionId(Guid localSessionGuid, bool forceLoad)
        {
            string targetUrl = NavManager.Manager.GetUriWithQueryParameter(
                QsKeys.LOCAL_SESSION_UUID,
                localSessionGuid.ToString("N"));

            NavManager.Manager.NavigateTo(targetUrl, forceLoad);
        }
    }
}
