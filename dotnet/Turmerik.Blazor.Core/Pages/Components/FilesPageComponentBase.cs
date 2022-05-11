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

        protected Guid? TabPageUuid => NavManager.QueryStrings.GetNullableValue(
            QsKeys.TAB_PAGE_UUID, (StringValues str, out Guid value) => Guid.TryParse(str, out value));

        protected async override Task OnAfterRenderAsync(bool firstRender)
        {
            await base.OnAfterRenderAsync(firstRender);

            if (firstRender)
            {
                DriveFolderIdentifier identifier = null;
                bool needsRedirectUrl = false;

                string id = NavManager.QueryStrings.GetStringOrNull(QsKeys.DRIVE_ITEM_ID);
                string path = NavManager.QueryStrings.GetStringOrNull(QsKeys.DRIVE_ITEM_PATH);

                string uri = NavManager.QueryStrings.GetStringOrNull(QsKeys.DRIVE_ITEM_URI);
                string address = NavManager.QueryStrings.GetStringOrNull(QsKeys.DRIVE_ITEM_ADDRESS);

                if (new string[] { id, path, uri, address }.Any(str => !string.IsNullOrWhiteSpace(str)))
                {
                    needsRedirectUrl = true;

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
                async () =>
                {
                    if (needsRedirectUrl)
                    {
                        NavManager.NavigateTo("files", false, new Dictionary<string, string>
                        {
                            { QsKeys.TAB_PAGE_UUID, ServiceArgs.TabPageUuid.Value.ToString("N") }
                        });
                    }
                });
            }
        }

        protected async Task OnAddressBarGoBackClick(MouseEventArgs args)
        {
            await NavigateCore(serviceArgs =>
            {
                serviceArgs.ActionType = DriveExplorerActionType.NavigateBack;

                serviceArgs.FolderIdentifier = new DriveFolderIdentifier
                {
                    Id = ServiceArgs.Data.TabPageItems.CurrentlyOpenFolder.Id
                };
            });
        }

        protected async Task OnAddressBarGoForwardClick(MouseEventArgs args)
        {
            await NavigateCore(serviceArgs =>
            {
                serviceArgs.ActionType = DriveExplorerActionType.NavigateForward;

                serviceArgs.FolderIdentifier = new DriveFolderIdentifier
                {
                    Id = ServiceArgs.Data.TabPageItems.CurrentlyOpenFolder.Id
                };
            });
        }

        protected async Task OnAddressBarGoUpClick(MouseEventArgs args)
        {
            await NavigateCore(serviceArgs =>
            {
                serviceArgs.ActionType = DriveExplorerActionType.Navigate;

                serviceArgs.FolderIdentifier = new DriveFolderIdentifier
                {
                    Id = ServiceArgs.Data.TabPageItems.CurrentlyOpenFolder.Id
                };

                serviceArgs.FolderNavigation = new DriveFolderNavigation
                {
                    NavigateToParent = true
                };
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
                serviceArgs.ActionType = DriveExplorerActionType.ReloadCurrentTab;

                serviceArgs.FolderIdentifier = new DriveFolderIdentifier
                {
                    Address = address
                };
            });
        }

        protected async Task OnDriveFolderClickAsync(DriveItem driveFolder)
        {
            await NavigateCore(serviceArgs =>
            {
                serviceArgs.ActionType = DriveExplorerActionType.ReloadCurrentTab;

                serviceArgs.FolderIdentifier = new DriveFolderIdentifier
                {
                    Id = ServiceArgs.Data.TabPageItems.CurrentlyOpenFolder.Id
                };

                serviceArgs.FolderNavigation = new DriveFolderNavigation
                {
                    SubFolderName = driveFolder.Name,
                };
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

        protected async Task OnNewTabPageClickAsync(MouseEventArgs args)
        {
        }

        protected async Task OnCloseTabPageClickAsync(IntEventArgsWrapper args)
        {
        }

        protected async Task NavigateCore(
            Action<DriveExplorerServiceArgs> argsCallback,
            Action callback = null)
        {
            await IfLocalSessionGuidHasValueAsync(async localSessionGuid =>
            {
                var serviceArgs = new DriveExplorerServiceArgs
                {
                    CacheKeyGuid = localSessionGuid,
                    TabPageUuid = TabPageUuid,
                };

                argsCallback(serviceArgs);
                await DriveFolderService.NavigateAsync(serviceArgs);

                ServiceArgs = serviceArgs;
                StateHasChanged();

                callback?.Invoke();
            });
        }

        protected void ClearError()
        {
            ErrorViewModel = null;
        }

        protected void SetError(
            string errorMessage,
            Exception exc = null)
        {
            ErrorViewModel = new ErrorViewModel(
                errorMessage, exc,
                AppSettings.IsDevMode);
        }
    }
}
