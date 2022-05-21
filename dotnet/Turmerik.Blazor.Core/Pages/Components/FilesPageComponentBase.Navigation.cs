using Microsoft.AspNetCore.Components.Web;
using Microsoft.Extensions.Primitives;
using Microsoft.JSInterop;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Turmerik.AspNetCore.Infrastructure;
using Turmerik.Core.Components;
using Turmerik.Core.Services.DriveItems;
using Turmerik.Core.Helpers;

namespace Turmerik.Blazor.Core.Pages.Components
{
    public partial class FilesPageComponentBase
    {
        protected async Task OnCurrentlyOpenDriveFolderOptionsClickAsync()
        {
            await OpenCurrentFolderOptionsModalAsync();
        }

        protected async Task OnDriveFolderOptionsClickAsync(DriveItem driveFolder)
        {
            await OpenDriveFolderOptionsModalAsync(driveFolder);
        }

        protected async Task OnDriveItemOptionsClickAsync(DriveItem driveItem)
        {
            await OpenDriveItemOptionsModalAsync(driveItem);
        }

        protected async Task OnAddressBarGoBackClick(MouseEventArgs args)
        {
            await NavigateCore(serviceArgs =>
            {
                serviceArgs.ActionType = DriveExplorerActionType.NavigateBack;
                AssignFolderIdentifier(serviceArgs);
            },
            TabPageUuid);
        }

        protected async Task OnAddressBarGoForwardClick(MouseEventArgs args)
        {
            await NavigateCore(serviceArgs =>
            {
                serviceArgs.ActionType = DriveExplorerActionType.NavigateForward;
                AssignFolderIdentifier(serviceArgs);
            },
            TabPageUuid);
        }

        protected async Task OnAddressBarGoUpClick(MouseEventArgs args)
        {
            await NavigateCore(serviceArgs =>
            {
                serviceArgs.ActionType = DriveExplorerActionType.Navigate;
                AssignFolderIdentifier(serviceArgs);

                AssignFolderNavigation(
                    serviceArgs,
                    null,
                    null,
                    true);
            },
            TabPageUuid);
        }

        protected async Task OnAddressBarReloadClick(MouseEventArgs args)
        {
            await NavigateCore(serviceArgs =>
            {
                serviceArgs.ActionType = DriveExplorerActionType.ReloadCurrentTab;
            },
            TabPageUuid);
        }

        protected async Task OnSubmitAddress(TextEventArgsWrapper args)
        {
            await SubmitAddressAsync(args.Value);
        }

        protected async Task SubmitAddressAsync(string address)
        {
            await NavigateCore(serviceArgs =>
            {
                serviceArgs.ActionType = DriveExplorerActionType.Navigate;

                AssignFolderNavigation(
                    serviceArgs,
                    null,
                    address);

                if (string.IsNullOrWhiteSpace(address))
                {
                    serviceArgs.FolderIdentifier = new DriveItemIdentifier
                    {
                        IsRootFolder = true,
                    };
                }
            },
            TabPageUuid);
        }

        protected async Task OnDriveFolderClickAsync(DriveItem driveFolder)
        {
            await OpenDriveFolderAsync(driveFolder);
        }

        protected async Task OnDriveItemClickAsync(DriveItem driveItem)
        {
            await OpenDriveItemAsync(driveItem);
        }

        protected async Task OnTabPageHeadClickAsync(IntEventArgsWrapper args)
        {
            await NavigateCore(serviceArgs =>
            {
                serviceArgs.ActionType = DriveExplorerActionType.ChangeTab;
                serviceArgs.TabPageUuid = ServiceArgs.Data.TabPageItems.Header.TabPageHeads[args.Value].Uuid;
            },
            TabPageUuid);
        }

        protected async Task OnNewTabPageClickAsync(MouseEventArgs args)
        {
            await NavigateCore(serviceArgs =>
            {
                serviceArgs.ActionType = DriveExplorerActionType.NewTab;
            },
            TabPageUuid);
        }

        protected async Task OnCloseTabPageClickAsync(IntEventArgsWrapper args)
        {
            await NavigateCore(serviceArgs =>
            {
                serviceArgs.ActionType = DriveExplorerActionType.CloseTab;
                serviceArgs.TrgTabPageUuid = ServiceArgs.Data.TabPageItems.Header.TabPageHeads[args.Value].Uuid;
            },
            TabPageUuid);
        }
    }
}
