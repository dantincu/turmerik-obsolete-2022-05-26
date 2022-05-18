using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Http;
using Turmerik.AspNetCore.Infrastructure;
using Turmerik.AspNetCore.LocalSession;
using Turmerik.AspNetCore.Services;
using Turmerik.AspNetCore.Services.LocalSessionStorage;
using Turmerik.AspNetCore.Settings;
using Turmerik.Core.Components;
using Turmerik.Core.Data;

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
        protected IMainLayoutService MainLayoutService { get; private set; }
        protected bool SideBarLarge { get; set; } = false;
        protected string? SideBarSizeCssClass => SideBarLarge ? CssClassH.Large : CssClassH.Small;
        protected Guid? LocalSessionGuid { get; set; }

        protected async override Task OnAfterRenderAsync(bool firstRender)
        {
            await base.OnAfterRenderAsync(firstRender);
        }

        protected async override Task OnInitializedAsync()
        {
            await base.OnInitializedAsync();
            LocalSessionGuid = NavManager.LocalSessionGuid;

            var localSessionData = await LocalSessionsManager.TryAddOrUpdateLocalSessionAsync(LocalSessionGuid);

            if (!LocalSessionGuid.HasValue || LocalSessionGuid.Value != localSessionData.Data.LocalSessionGuid)
            {
                NavigateToLocalSessionId(localSessionData.Data.LocalSessionGuid, true);
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

        protected void OverlayEnabledChanged(bool overlayEnabled)
        {
            StateHasChanged();
        }

        protected void ErrorViewModelChanged(ErrorViewModel errorViewModel)
        {
            StateHasChanged();
        }

        protected void AssignMainLayoutService(IMainLayoutService mainLayoutService)
        {
            MainLayoutService = mainLayoutService;

            mainLayoutService.OnErrorViewModelChanged += ErrorViewModelChanged;
            mainLayoutService.OnOverlayEnabledChanged += OverlayEnabledChanged;
        }
    }
}
