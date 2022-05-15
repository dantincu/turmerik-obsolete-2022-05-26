using Microsoft.AspNetCore.Components.Web;
using Microsoft.Extensions.Primitives;
using Turmerik.AspNetCore.Infrastructure;
using Turmerik.AspNetCore.Services.LocalSessionStorage;
using Turmerik.Core.Services.DriveItems;
using Turmerik.Core.Helpers;
using Microsoft.AspNetCore.WebUtilities;
using System.Collections.Concurrent;
using Turmerik.Core.Data;
using Turmerik.Core.Services;
using Microsoft.JSInterop;

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
        protected IJSRuntime JSRuntime { get; set; }
        protected ErrorViewModel ErrorViewModel { get; set; }
        protected DriveExplorerServiceArgs ServiceArgs { get; set; }
        protected bool FoldersGridCollapsed { get; set; }
        protected bool FilesGridCollapsed { get; set; }
        protected string CollapseFoldersGridBtnCssClass => FoldersGridCollapsed ? CssClassH.Hidden : string.Empty;
        protected string ExpandFoldersGridBtnCssClass => FoldersGridCollapsed ? string.Empty : CssClassH.Hidden;
        protected string CollapseFilesGridBtnCssClass => FilesGridCollapsed ? CssClassH.Hidden : string.Empty;
        protected string ExpandFilesGridBtnCssClass => FilesGridCollapsed ? string.Empty : CssClassH.Hidden;

        protected string SelectedDriveFolderId { get; set; }
        protected string SelectedDriveFolderName { get; set; }
        protected string SelectedDriveFolderAddress { get; set; }
        protected string SelectedDriveFolderUri { get; set; }

        protected string SelectedDriveItemId { get; set; }
        protected string SelectedDriveItemName { get; set; }
        protected string SelectedDriveItemAddress { get; set; }
        protected string SelectedDriveItemUri { get; set; }

        protected Guid? TabPageUuid => NavManager.QueryStrings.GetNullableValue(
            QsKeys.TAB_PAGE_UUID, (StringValues str, out Guid value) => Guid.TryParse(str, out value));

        protected async override Task OnAfterRenderAsync(bool firstRender)
        {
            await base.OnAfterRenderAsync(firstRender);

            if (firstRender)
            {
                DriveItemIdentifier identifier = null;
                Guid? tabPageUuid = TabPageUuid;

                bool newTabPage = NavManager.QueryStrings.GetNullableValue(
                    QsKeys.NEW_TAB_PAGE,
                    (StringValues values,
                        out bool val) => bool.TryParse(
                            values,
                            out val)) ?? false;

                if (newTabPage)
                {
                    if (!TabPageUuid.HasValue)
                    {
                        tabPageUuid = Guid.NewGuid();
                    }
                    else
                    {
                        throw new InvalidOperationException("When a new tab is required, the tab page uuid cannot also be set");
                    }
                }

                bool needsRedirect = !TabPageUuid.HasValue;

                string id = NavManager.QueryStrings.GetStringOrNull(QsKeys.DRIVE_ITEM_ID);
                string path = NavManager.QueryStrings.GetStringOrNull(QsKeys.DRIVE_ITEM_PATH);

                string uri = NavManager.QueryStrings.GetStringOrNull(QsKeys.DRIVE_ITEM_URI);
                string address = NavManager.QueryStrings.GetStringOrNull(QsKeys.DRIVE_ITEM_ADDRESS);

                if (new string[] { id, path, uri, address }.Any(str => !string.IsNullOrWhiteSpace(str)))
                {
                    needsRedirect = true;

                    identifier = new DriveItemIdentifier
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
                tabPageUuid,
                needsRedirect);
            }
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
            },
            TabPageUuid);
        }

        protected async Task OnDriveItemClickAsync(DriveItem driveItem)
        {
        }

        protected async Task OnCurrentlyOpenDriveFolderOptionsClickAsync()
        {
            await JSRuntime.InvokeVoidAsync(
                JsH.Get(JsH.OpenModal),
                ModalIds.CURRENTLY_OPEN_DRIVE_FOLDER_OPTIONS);
        }

        protected async Task OnDriveFolderOptionsClickAsync(DriveItem driveFolder)
        {
            var identifier = new DriveItemIdentifier
            {
                Id = driveFolder.Id,
                Name = driveFolder.Name,
                ParentId = ServiceArgs.Data.TabPageItems.CurrentlyOpenFolder.Id,
            };

            SelectedDriveFolderName = driveFolder.Name;
            SelectedDriveFolderId = DriveFolderService.GetDriveItemId(identifier);
            SelectedDriveFolderAddress = DriveFolderService.GetDriveItemAddress(identifier);

            StateHasChanged();

            await JSRuntime.InvokeVoidAsync(
                JsH.Get(JsH.OpenModal),
                ModalIds.DRIVE_FOLDER_OPTIONS);
        }

        protected async Task OnDriveItemOptionsClickAsync(DriveItem driveItem)
        {
            var identifier = new DriveItemIdentifier
            {
                Id = driveItem.Id,
                Name = driveItem.Name,
                ParentId = ServiceArgs.Data.TabPageItems.CurrentlyOpenFolder.Id,
            };

            SelectedDriveItemName = driveItem.Name;
            SelectedDriveItemId = DriveFolderService.GetDriveItemId(identifier);
            SelectedDriveItemAddress = DriveFolderService.GetDriveItemAddress(identifier);

            StateHasChanged();

            await JSRuntime.InvokeVoidAsync(
                JsH.Get(JsH.OpenModal),
                ModalIds.DRIVE_ITEM_OPTIONS);
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

        private async Task NavigateCore(
            Action<DriveExplorerServiceArgs> argsCallback,
            Guid? tabPageUuid,
            bool needsRedirect = false)
        {
            await IfLocalSessionGuidHasValueAsync(async localSessionGuid =>
            {
                var serviceArgs = new DriveExplorerServiceArgs
                {
                    CacheKeyGuid = localSessionGuid,
                    TabPageUuid = tabPageUuid,
                    Data = ServiceArgs?.Data,
                };

                if (ErrorViewModel != null && !string.IsNullOrWhiteSpace(ServiceArgs.FolderIdentifier?.Address))
                {
                    serviceArgs.FolderIdentifier = new DriveItemIdentifier
                    {
                        Address = ServiceArgs.FolderIdentifier.Address
                    };
                }

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
                    ServiceArgs.FolderIdentifier = serviceArgs.FolderIdentifier;
                }

                StateHasChanged();

                if (needsRedirect)
                {
                    NavManager.NavigateTo("files", false, new Dictionary<string, string>
                        {
                            { QsKeys.TAB_PAGE_UUID, serviceArgs.TabPageUuid.Value.ToString("N") }
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

            serviceArgs.FolderIdentifier = new DriveItemIdentifier
            {
                Id = currentlyOpenFolder.Id,
                IsRootFolder = currentlyOpenFolder.IsRootFolder ?? false,
                Name = currentlyOpenFolder.Name,
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
