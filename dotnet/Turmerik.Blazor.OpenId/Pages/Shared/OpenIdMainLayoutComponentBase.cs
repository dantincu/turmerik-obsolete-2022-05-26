using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Turmerik.AspNetCore.Infrastructure;
using Turmerik.AspNetCore.OpenId.UserSession;
using Turmerik.AspNetCore.Settings;
using Turmerik.Blazor.Core.Pages.Shared;

namespace Turmerik.Blazor.OpenId.Pages.Shared
{
    public abstract class OpenIdMainLayoutComponentBase : MainLayoutComponentBase
    {
        protected IUserSessionsManager UserSessionsManager { get; set; }
        protected System.Security.Claims.ClaimsPrincipal User { get; set; }

        [CascadingParameter] protected Task<AuthenticationState> AuthStat { get; set; }
        protected async override Task OnAfterRenderAsync(bool firstRender)
        {
            await base.OnAfterRenderAsync(firstRender);
            await UserSessionsManager.TryAddOrUpdateUserSessionAsync();
        }

        protected async override Task OnInitializedAsync()
        {
            await base.OnInitializedAsync();
            User = (await AuthStat).User;

            if (!User.Identity.IsAuthenticated || !User.Claims.Any())
            {
                NavManager.Manager.NavigateTo(
                    $"{AppSettings.LoginRelUrl}?{QsKeys.RET_URL}={Uri.EscapeDataString(NavManager.Manager.Uri)}");
            }
        }
    }
}
