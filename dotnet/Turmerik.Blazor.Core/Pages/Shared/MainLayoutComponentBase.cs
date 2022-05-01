using Blazored.LocalStorage;
using Blazored.SessionStorage;
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
using Turmerik.AspNetCore.Infrastructure;
using Turmerik.AspNetCore.LocalSession;
using Turmerik.AspNetCore.Services;
using Turmerik.AspNetCore.Settings;

namespace Turmerik.Blazor.Core.Pages.Shared
{
    public abstract class MainLayoutComponentBase : LayoutComponentBase
    {
        protected ILocalSessionsManager LocalSessionsManager { get; set; }
        protected INavManager NavManager { get; set; }
        protected ITrmrkAppSettings AppSettings { get; set; }
        protected IHttpContextAccessor HttpContextAccessor { get; set; }
        protected ISessionStorageService SessionStorage { get; set; }
        protected bool SideBarLarge { get; set; } = false;
        protected string? SideBarSizeCssClass => SideBarLarge ? CssClassH.Large : CssClassH.Small;
        protected Guid? LocalSessionGuid { get; set; }

        protected async override Task OnAfterRenderAsync(bool firstRender)
        {
            await base.OnAfterRenderAsync(firstRender);
            LocalSessionGuid = NavManager.LocalSessionGuid;

            if (!LocalSessionGuid.HasValue)
            {
                var tuple = await SessionStorage.TryGetValueAsync<Guid>(
                    LocalStorageKeys.LocalSession, true);

                if (tuple.Item1)
                {
                    LocalSessionGuid = tuple.Item2;
                    await LocalSessionsManager.TryAddOrUpdateLocalSessionAsync(tuple.Item2);

                    NavigateToLocalSessionId(tuple.Item2, false);
                }
            }

            if (!LocalSessionGuid.HasValue)
            {
                var localSessionData = await LocalSessionsManager.TryAddOrUpdateLocalSessionAsync(null);
                NavigateToLocalSessionId(localSessionData.LocalSessionGuid, true);
            }
        }

        protected void SizeChangedCallback(MouseEventArgs e)
        {
            SideBarLarge = !SideBarLarge;
        }

        protected void NavigateToLocalSessionId(Guid localSessionGuid, bool forceLoad)
        {
            string targetUrl = QueryHelpers.AddQueryString(
                    NavManager.AbsUri.AbsoluteUri,
                    QsKeys.LOCAL_SESSION_ID,
                    localSessionGuid.ToString("N"));

            NavManager.Manager.NavigateTo(targetUrl, forceLoad);
        }
    }
}
