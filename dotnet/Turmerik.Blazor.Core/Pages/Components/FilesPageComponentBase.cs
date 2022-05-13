﻿using Microsoft.AspNetCore.Components.Web;
using Microsoft.Extensions.Primitives;
using Turmerik.AspNetCore.Infrastructure;
using Turmerik.AspNetCore.Services.LocalSessionStorage;
using Turmerik.Core.Services.DriveItems;
using Turmerik.Core.Helpers;
using Microsoft.AspNetCore.WebUtilities;
using System.Collections.Concurrent;
using Turmerik.Core.Data;
using Turmerik.Core.Services;

namespace Turmerik.Blazor.Core.Pages.Components
{
    public abstract class FilesPageComponentBase : PageComponentBase
    {
        protected const string DEFAULT_ERR_MSG = "An unhandled error has occurred";
        protected const string INVALID_ADDRESS_ERR_MSG = "The provided address is invalid";

        protected FilesPageComponentBase()
        {
        }

        protected ILocalStorageWrapper LocalStorage { get; set; }
        protected ISessionStorageWrapper SessionStorage { get; set; }
        protected IDriveExplorerService DriveFolderService { get; set; }
        protected ErrorViewModel ErrorViewModel { get; set; }
        protected DriveExplorerServiceArgs ServiceArgs { get; set; }
        protected bool FoldersGridCollapsed { get; set; }
        protected bool FilesGridCollapsed { get; set; }
        protected string CollapseFoldersGridBtnCssClass => FoldersGridCollapsed ? string.Empty : CssClassH.Hidden;
        protected string ExpandFoldersGridBtnCssClass => FoldersGridCollapsed ? CssClassH.Hidden : string.Empty;
        protected string CollapseFilesGridBtnCssClass => FilesGridCollapsed ? string.Empty : CssClassH.Hidden;
        protected string ExpandFilesGridBtnCssClass => FilesGridCollapsed ? CssClassH.Hidden : string.Empty;

        protected Guid? TabPageUuid => NavManager.QueryStrings.GetNullableValue(
            QsKeys.TAB_PAGE_UUID, (StringValues str, out Guid value) => Guid.TryParse(str, out value));

        protected async override Task OnAfterRenderAsync(bool firstRender)
        {
            await base.OnAfterRenderAsync(firstRender);

            if (firstRender)
            {
                DriveFolderIdentifier identifier = null;
                bool needsRedirect = !TabPageUuid.HasValue;

                string id = NavManager.QueryStrings.GetStringOrNull(QsKeys.DRIVE_ITEM_ID);
                string path = NavManager.QueryStrings.GetStringOrNull(QsKeys.DRIVE_ITEM_PATH);

                string uri = NavManager.QueryStrings.GetStringOrNull(QsKeys.DRIVE_ITEM_URI);
                string address = NavManager.QueryStrings.GetStringOrNull(QsKeys.DRIVE_ITEM_ADDRESS);

                if (new string[] { id, path, uri, address }.Any(str => !string.IsNullOrWhiteSpace(str)))
                {
                    needsRedirect = true;

                    identifier = new DriveFolderIdentifier
                    {
                        Id = id,
                        Path = path,
                        Uri = uri,
                        Address = address
                    };
                }

                await NavigateCore(serviceArgs =>
                {
                    serviceArgs.ActionType = DriveExplorerActionType.Initialize;
                    serviceArgs.FolderIdentifier = identifier;
                },
                needsRedirect);
            }
        }

        protected async Task OnAddressBarGoBackClick(MouseEventArgs args)
        {
            await NavigateCore(serviceArgs =>
            {
                serviceArgs.ActionType = DriveExplorerActionType.NavigateBack;
                AssignFolderIdentifier(serviceArgs);
            });
        }

        protected async Task OnAddressBarGoForwardClick(MouseEventArgs args)
        {
            await NavigateCore(serviceArgs =>
            {
                serviceArgs.ActionType = DriveExplorerActionType.NavigateForward;
                AssignFolderIdentifier(serviceArgs);
            });
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
            });
        }

        protected async Task OnCurrentlyOpenFolderOptionsClick(MouseEventArgs args)
        {
        }

        protected async Task OnAddressBarReloadClick(MouseEventArgs args)
        {
            await NavigateCore(serviceArgs =>
            {
                serviceArgs.ActionType = DriveExplorerActionType.ReloadCurrentTab;
            });
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
                    serviceArgs.FolderIdentifier = new DriveFolderIdentifier
                    {
                        IsRootFolder = true,
                    };
                }
            });
        }

        protected async Task OnDriveFolderClickAsync(DriveItem driveFolder)
        {
            await NavigateCore(serviceArgs =>
            {
                serviceArgs.ActionType = DriveExplorerActionType.Navigate;
                AssignFolderIdentifier(serviceArgs);

                if (!string.IsNullOrWhiteSpace(driveFolder.Id))
                {
                    AssignFolderNavigation(serviceArgs, null, driveFolder.Id);
                }
                else
                {
                    AssignFolderNavigation(serviceArgs, driveFolder.Name, null);
                }
            });
        }

        protected async Task OnDriveItemClickAsync(DriveItem driveItem)
        {
        }

        protected async Task OnDriveFolderOptionsClickAsync(DriveItem driveFolder)
        {
        }

        protected async Task OnDriveItemOptionsClickAsync(DriveItem driveItem)
        {
        }

        protected async Task OnTabPageHeadClickAsync(IntEventArgsWrapper args)
        {
            await NavigateCore(serviceArgs =>
            {
                serviceArgs.ActionType = DriveExplorerActionType.ChangeTab;
                serviceArgs.TabPageUuid = ServiceArgs.Data.TabPageItems.Header.TabPageHeads[args.Value].Uuid;
            });
        }

        protected async Task OnNewTabPageClickAsync(MouseEventArgs args)
        {
            await NavigateCore(serviceArgs =>
            {
                serviceArgs.ActionType = DriveExplorerActionType.NewTab;
            });
        }

        protected async Task OnCloseTabPageClickAsync(IntEventArgsWrapper args)
        {
            await NavigateCore(serviceArgs =>
            {
                serviceArgs.ActionType = DriveExplorerActionType.CloseTab;
                serviceArgs.TrgTabPageUuid = ServiceArgs.Data.TabPageItems.Header.TabPageHeads[args.Value].Uuid;
            });
        }

        private async Task NavigateCore(
            Action<DriveExplorerServiceArgs> argsCallback,
            bool needsRedirect = false)
        {
            await IfLocalSessionGuidHasValueAsync(async localSessionGuid =>
            {
                var serviceArgs = new DriveExplorerServiceArgs
                {
                    CacheKeyGuid = localSessionGuid,
                    TabPageUuid = TabPageUuid,
                    Data = ServiceArgs?.Data,
                };

                try
                {
                    argsCallback(serviceArgs);
                    await DriveFolderService.NavigateAsync(serviceArgs);

                    serviceArgs.FolderIdentifier = serviceArgs.FolderIdentifier ?? ServiceArgs?.FolderIdentifier;

                    if (ServiceArgs != null)
                    {
                        needsRedirect = needsRedirect || ServiceArgs.TabPageUuid != serviceArgs.TabPageUuid;
                    }

                    ServiceArgs = serviceArgs;
                    ClearError();
                }
                catch (Exception ex)
                {
                    SetError("An unhandled error ocurred", ex);
                }

                if (ErrorViewModel != null)
                {
                    ServiceArgs.Data.TabPageItems.GoUpButtonEnabled = false;
                    string address = serviceArgs.FolderNavigation?.Id;

                    if (string.IsNullOrWhiteSpace(address))
                    {
                        address = serviceArgs.FolderIdentifier?.Id;
                    }

                    if (!string.IsNullOrWhiteSpace(address))
                    {
                        ServiceArgs.FolderIdentifier.Address = address;
                    }
                }

                StateHasChanged();

                if (needsRedirect)
                {
                    NavManager.NavigateTo("files", false, new Dictionary<string, string>
                        {
                            { QsKeys.TAB_PAGE_UUID, ServiceArgs.TabPageUuid.Value.ToString("N") }
                        });
                }
            });
        }

        private void ClearError()
        {
            ErrorViewModel = null;
        }

        private void SetError(
            string errorMessage,
            Exception exc = null)
        {
            ErrorViewModel = new ErrorViewModel(
                errorMessage, exc,
                AppSettings.IsDevMode);
        }

        private DriveFolder AssignFolderIdentifier(DriveExplorerServiceArgs serviceArgs)
        {
            var currentlyOpenFolder = ServiceArgs.Data.TabPageItems.CurrentlyOpenFolder;

            serviceArgs.FolderIdentifier = new DriveFolderIdentifier
            {
                Id = currentlyOpenFolder.Id,
                IsRootFolder = currentlyOpenFolder.IsRootFolder ?? false
            };

            return currentlyOpenFolder;
        }

        private DriveFolderNavigation AssignFolderNavigation(
            DriveExplorerServiceArgs serviceArgs,
            string folderName,
            string folderId = null,
            bool? navigateToParent = null)
        {
            serviceArgs.FolderNavigation = new DriveFolderNavigation
            {
                Id = folderId,
                Name = folderName,
                Up = navigateToParent
            };

            return serviceArgs.FolderNavigation;
        }
    }
}
