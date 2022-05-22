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
using Turmerik.Core.Components;
using Turmerik.AspNetCore.Services;
using Microsoft.AspNetCore.Components;

namespace Turmerik.Blazor.Core.Pages.Components
{
    public abstract partial class FilesPageComponentBase : PageComponentBase
    {
        protected const string DEFAULT_ERR_MSG = "An unhandled error has occurred";
        protected const string INVALID_ADDRESS_ERR_MSG = "The provided address is invalid";

        protected FilesPageComponentBase()
        {
        }

        [Parameter]
        public bool IsLocalDiskExplorer { get; set; }

        protected ILocalStorageWrapper LocalStorage { get; set; }
        protected ISessionStorageWrapper SessionStorage { get; set; }
        protected IDriveExplorerService DriveFolderService { get; set; }
        protected IJSRuntime JSRuntime { get; set; }
        protected IMainLayoutService MainLayoutService { get; set; }
        protected ErrorViewModel ErrorViewModel { get; set; }
        protected DriveExplorerServiceArgs ServiceArgs { get; set; }

        protected AddressBar AddressBar { get; set; }
        protected bool IsEditingAddressBar { get; set; }
        protected bool CurrentlyOpenDriveFolderOptionsModelIsOpen { get; set; }
        protected bool DriveFolderItemOptionsModelIsOpen { get; set; }
        protected bool DriveItemOptionsModelIsOpen { get; set; }

        protected List<List<IDriveItemCommand>> CurrentlyOpenDriveFolderCommandsMx { get; set; }
        protected List<List<IDriveItemCommand>> SelectedDriveFolderCommandsMx { get; set; }
        protected List<List<IDriveItemCommand>> SelectedDriveItemCommandsMx { get; set; }

        protected DriveItem SelectedDriveFolder { get; set; }
        protected string SelectedDriveFolderId { get; set; }
        protected string SelectedDriveFolderName { get; set; }
        protected string SelectedDriveFolderAddress { get; set; }
        protected string SelectedDriveFolderUri { get; set; }

        protected DriveItem SelectedDriveItem { get; set; }
        protected string SelectedDriveItemId { get; set; }
        protected string SelectedDriveItemName { get; set; }
        protected string SelectedDriveItemAddress { get; set; }
        protected string SelectedDriveItemUri { get; set; }

        protected Guid? TabPageUuid => NavManager.QueryStrings.GetNullableValue(
            QsKeys.TAB_PAGE_UUID, (StringValues str, out Guid value) => Guid.TryParse(str, out value));

        protected override async Task OnInitializedAsync()
        {
            await base.OnInitializedAsync();
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
            needsRedirect,
            () =>
            {
                CurrentlyOpenDriveFolderCommandsMx = GetCurrentlyOpenDriveFolderCommandsMx();

                SelectedDriveFolderCommandsMx = GetSelectedDriveFolderCommandsMx();
                SelectedDriveItemCommandsMx = GetSelectedDriveItemCommandsMx();
            });
        }

        protected async Task OpenDriveFolderAsync(DriveItem driveFolder)
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

        protected async Task OpenDriveItemAsync(DriveItem driveItem)
        {
            await OpenFileInOSDefaultApp(driveItem);
        }

        private async Task NavigateCore(
            Action<DriveExplorerServiceArgs> argsCallback,
            Guid? tabPageUuid,
            bool needsRedirect = false,
            Action callback = null)
        {
            await IfLocalSessionGuidHasValueAsync(async localSessionGuid =>
            {
                await MainLayoutService.ExecuteWithUIBlockingOverlay(
                    async (uiOverlayVM) =>
                    {
                        if (IsEditingAddressBar)
                        {
                            IsEditingAddressBar = false;
                            StateHasChanged();
                        }

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
                            var task = AssureAllModalsAreClosedAsync();

                            argsCallback(serviceArgs);
                            await DriveFolderService.NavigateAsync(serviceArgs);

                            serviceArgs.FolderIdentifier = serviceArgs.FolderIdentifier ?? ServiceArgs?.FolderIdentifier;

                            if (ServiceArgs != null)
                            {
                                needsRedirect = needsRedirect || ServiceArgs.TabPageUuid != serviceArgs.TabPageUuid;
                            }

                            ServiceArgs = serviceArgs;
                            callback?.Invoke();

                            ClearError();
                            await task;
                        }
                        catch (Exception ex)
                        {
                            SetError("An unhandled error ocurred", ex);
                        }

                        if (ErrorViewModel != null && ServiceArgs != null)
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

                        uiOverlayVM.Enabled = false;
                    });
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
