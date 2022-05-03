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
using Turmerik.AspNetCore.Services.LocalSessionStorage;
using Turmerik.Blazor.Core.Pages.Components;
using Turmerik.Core.Services.DriveItems;

namespace Turmerik.Blazor.Core.Pages
{
    public abstract class FilesBase : PageBase
    {
        protected ILocalStorageWrapper LocalStorage { get; set; }
        protected ISessionStorageWrapper SessionStorage { get; set; }
        protected IDriveFolderService DriveFolderService { get; set; }
        protected string AddressStrValue { get; set; }
        protected List<ITabPageHead> TabPageHeadsList { get; set; }
        protected CurrentDriveItemsTuple CurrentDriveItems { get; set; }

        protected async override Task OnInitializedAsync()
        {
            await base.OnInitializedAsync();

            await IfLocalSessionGuidHasValueAsync(async localSessionGuid =>
            {
                CurrentDriveItems = await DriveFolderService.GetCurrentDriveItemsAsync(localSessionGuid, false);
                // TabPageHeadsList = tabPageHeadsList.Cast<ITabPageHead>().ToList();
            });
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

        protected TabPageHeadMtbl DriveItemToTabPageHead(IDriveItemCore driveItem, bool isCurrent)
        {
            var tabPageHead = new TabPageHeadMtbl
            {
                Name = driveItem.Name,
                IsCurrent = isCurrent
            };

            return tabPageHead;
        }
    }
}
