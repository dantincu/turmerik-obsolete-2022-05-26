using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Http;
using Microsoft.JSInterop;
using Newtonsoft.Json;
using Turmerik.AspNetCore.Infrastructure;
using Turmerik.AspNetCore.LocalSession;
using Turmerik.AspNetCore.Services;
using Turmerik.AspNetCore.Services.LocalSessionStorage;
using Turmerik.AspNetCore.Settings;
using Turmerik.Core.Cloneable;
using Turmerik.Core.Components;
using Turmerik.Core.Data;
using Turmerik.Core.Helpers;

namespace Turmerik.Blazor.Core.Pages.Shared
{
    public abstract class MainLayoutComponentBase : LayoutComponentBase
    {
        protected ICloneableMapper Mapper { get; set; }
        protected ITypesStaticDataCache TypesStaticDataCache { get; set; }
        protected ILocalSessionsManager LocalSessionsManager { get; set; }
        protected INavManager NavManager { get; set; }
        protected ITrmrkAppSettings AppSettings { get; set; }
        protected IHttpContextAccessor HttpContextAccessor { get; set; }
        protected ILocalStorageWrapper LocalStorage { get; set; }
        protected ISessionStorageWrapper SessionStorage { get; set; }
        protected IJSRuntime JSRuntime { get; set; }
        protected IMainLayoutService MainLayoutService { get; private set; }
        protected bool SideBarLarge { get; set; } = false;
        protected string? SideBarSizeCssClass => SideBarLarge ? CssClassH.Large : CssClassH.Small;
        protected Guid? LocalSessionGuid { get; set; }
        protected bool OverlayEnabled => MainLayoutService?.UIBlockingOverlayViewModel?.Enabled ?? false;
        protected ErrorViewModel? ErrorViewModel => MainLayoutService?.UIBlockingOverlayViewModel.Error;
        protected string PageCssClass => (OverlayEnabled && ErrorViewModel != null) ? CssClassH.Hidden : string.Empty;
        protected Dictionary<string, string> ApiBaseUriKeysToAddOnPageLoad { get; set; }
        protected INavMenuItemsViewModel MenuItems { get; private set; }

        protected async override Task OnAfterRenderAsync(bool firstRender)
        {
            await base.OnAfterRenderAsync(firstRender);
        }

        protected async override Task OnInitializedAsync()
        {
            await base.OnInitializedAsync();
            MenuItems = await GetMenuItemsAsync();

            LocalSessionGuid = NavManager.LocalSessionGuid;
            var localSessionData = await LocalSessionsManager.TryAddOrUpdateLocalSessionAsync(LocalSessionGuid);

            if (!LocalSessionGuid.HasValue || LocalSessionGuid.Value != localSessionData.Data.LocalSessionGuid)
            {
                NavigateToLocalSessionId(localSessionData.Data.LocalSessionGuid, true);
            }
            else if (ApiBaseUriKeysToAddOnPageLoad != null)
            {
                var propInfosClctn = TypesStaticDataCache.Get(typeof(TrmrkAppSettingsImmtbl)).InstPubGetProps.Value;

                var map = ApiBaseUriKeysToAddOnPageLoad.ToDictionary(
                    kvp => kvp.Key,
                    kvp => propInfosClctn.SingleOrDefault(
                        wrppr => wrppr.Name == kvp.Value)?.Getter.Value.Data.Invoke(
                            AppSettings, new object[0])?.ToString()).Where(
                    kvp => !string.IsNullOrWhiteSpace(kvp.Value)).Dictnr();

                if (map.Any())
                {
                    await JSRuntime.InvokeVoidAsync(JsH.Get(JsH.Api.AddBaseUrisMap), map);
                }
            }
        }

        protected virtual async Task<INavMenuItemsViewModel> GetMenuItemsAsync()
        {
            var menuItems = GetMenuItemsCore(
                new NavMenuItemMtbl
                {
                    Title = "Files",
                    Url = "files",
                    IconCssClass = "oi oi-browser"
                },
                new NavMenuItemMtbl
                {
                    Title = "Settings",
                    Url = "settings",
                    IconCssClass = "oi oi-cog"
                });

            return menuItems;
        }

        protected INavMenuItemsViewModel GetMenuItemsCore(params NavMenuItemMtbl[] menuItemsArr)
        {
            var menuItems = new NavMenuItemsViewModelImmtbl(
                Mapper, new NavMenuItemsViewModelMtbl
                {
                    NavMenuItemsDictnr = new NavMenuItemsDictnr(null, menuItemsArr.ToDictionary(
                        item => item.Url,
                        item => item))
                });

            return menuItems;
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
        }
    }
}
