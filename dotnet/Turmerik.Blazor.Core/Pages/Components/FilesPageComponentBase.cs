using Microsoft.AspNetCore.Components.Web;
using Microsoft.Extensions.Primitives;
using Turmerik.AspNetCore.Infrastructure;
using Turmerik.AspNetCore.Services.LocalSessionStorage;
using Turmerik.Blazor.Core.Services;
using Turmerik.Core.Services.DriveItems;
using Turmerik.Core.Helpers;
using Microsoft.AspNetCore.WebUtilities;

namespace Turmerik.Blazor.Core.Pages.Components
{
    public abstract class FilesPageComponentBase : PageComponentBase
    {
        protected const string DEFAULT_ERR_MSG = "An unhandled error has occurred";
        protected const string INVALID_ADDRESS_ERR_MSG = "The provided address is invalid";

        protected FilesPageComponentBase()
        {
            TabPageViewModel = new DriveFolderTabPageViewModel();
        }

        protected ILocalStorageWrapper LocalStorage { get; set; }
        protected ISessionStorageWrapper SessionStorage { get; set; }
        protected IDriveFolderService DriveFolderService { get; set; }
        protected string AddressStrValue { get; set; }
        protected string DriveItemId { get; set; }
        protected List<ITabPageHead> TabPageHeadsList { get; set; }
        protected IReadOnlyCollection<IDriveFolder> CurrentDriveFolders { get; set; }
        protected IDriveFolder CurrentlyOpenDriveFolder { get; set; }
        protected DriveFolderTabPageViewModel TabPageViewModel { get; set; }
        protected bool InvalidAddress { get; set; }
        protected ErrorViewModel ErrorViewModel { get; set; }

        protected IEnumerable<IDriveFolder>? TabViewPageDriveFolders
        {
            get => CurrentlyOpenDriveFolder?.DriveFoldersList?.Immtbl ?? Enumerable.Empty<IDriveFolder>();
        }

        protected IEnumerable<IDriveItem>? TabViewPageDriveItems
        {
            get => CurrentlyOpenDriveFolder?.DriveItemsList?.Immtbl ?? Enumerable.Empty<IDriveItem>();
        }

        protected async override Task OnInitializedAsync()
        {
            await base.OnInitializedAsync();

            await IfLocalSessionGuidHasValueAsync(async localSessionGuid =>
            {
                string initialAddress = QueryStrings.GetStringOrNull(QsKeys.DRIVE_ITEM_ID);

                string normAddress = initialAddress;
                string driveItemId = null;

                if (!string.IsNullOrWhiteSpace(
                    normAddress))
                {
                    if (DriveFolderService.TryNormalizeAddress(
                        ref normAddress,
                        out driveItemId))
                    {
                        AddressStrValue = normAddress;
                    }
                    else
                    {
                        driveItemId = null;
                        SetError(INVALID_ADDRESS_ERR_MSG);
                    }
                }

                if (ErrorViewModel == null)
                {
                    try
                    {
                        if (string.IsNullOrWhiteSpace(driveItemId))
                        {
                            CurrentlyOpenDriveFolder = await DriveFolderService.GetRootDriveFolderAsync(
                                localSessionGuid, false);
                        }
                        else
                        {
                            CurrentlyOpenDriveFolder = await DriveFolderService.GetDriveFolderAsync(
                                driveItemId, localSessionGuid, false);
                        }

                        CurrentDriveFolders = await DriveFolderService.GetCurrentDriveFoldersAsync(
                            CurrentlyOpenDriveFolder,
                            localSessionGuid);

                        TabPageHeadsList = CurrentDriveFolders.Select(
                            (item) => (ITabPageHead)DriveItemToTabPageHead(
                                item, DriveFolderService.DriveItemsHaveSameAddress(
                                    item, CurrentlyOpenDriveFolder, false))).ToList();

                        TabPageViewModel.Data = CurrentlyOpenDriveFolder;
                    }
                    catch (Exception exc)
                    {
                        SetError(DEFAULT_ERR_MSG, exc);
                    }
                }
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
            string address = args.Value;
            string pathOrId;

            if (DriveFolderService.TryNormalizeAddress(ref address, out pathOrId))
            {
                string targetUrl = QueryHelpers.AddQueryString(
                        NavManager.AbsUri.AbsoluteUri,
                        QsKeys.DRIVE_ITEM_ID,
                        pathOrId);

                ClearError();
                NavManager.Manager.NavigateTo(targetUrl, true);
            }
            else
            {
                SetError(INVALID_ADDRESS_ERR_MSG);
            }
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
