using Blazored.LocalStorage;
using Blazored.SessionStorage;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Turmerik.AspNetCore.Infrastructure;
using Turmerik.AspNetCore.Services.DriveItems;

namespace Turmerik.Blazor.Core.Pages
{
    public abstract class FilesBase : ComponentBase
    {
        protected ILocalStorageService LocalStorage { get; set; }
        protected ISessionStorageService SessionStorage { get; set; }
        protected IDriveFolderService DriveFolderService { get; set; }
        protected string AddressStrValue { get; set; }

        protected override Task OnAfterRenderAsync(bool firstRender)
        {
            return base.OnAfterRenderAsync(firstRender);
        }

        protected async Task OnAddressBarGoBackClick(MouseEventArgs args)
        {

        }

        protected async Task OnAddressBarGoUpClick(MouseEventArgs args)
        {

        }

        protected async Task OnAddressBarGoForwardClick(MouseEventArgs args)
        {

        }

        protected async Task OnAddressBarReloadClick(MouseEventArgs args)
        {

        }

        protected async Task OnSubmitAddress(TextEventArgsWrapper args)
        {

        }
    }
}
