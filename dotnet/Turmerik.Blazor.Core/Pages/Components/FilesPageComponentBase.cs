using Microsoft.AspNetCore.Components.Web;
using Microsoft.Extensions.Primitives;
using Turmerik.AspNetCore.Infrastructure;
using Turmerik.AspNetCore.Services.LocalSessionStorage;
using Turmerik.Blazor.Core.Services;
using Turmerik.Core.Services.DriveItems;
using Turmerik.Core.Helpers;
using Microsoft.AspNetCore.WebUtilities;
using System.Collections.Concurrent;
using Turmerik.Core.Data;

namespace Turmerik.Blazor.Core.Pages.Components
{
    public abstract class FilesPageComponentBase : PageComponentBase
    {
        protected const string DEFAULT_ERR_MSG = "An unhandled error has occurred";
        protected const string INVALID_ADDRESS_ERR_MSG = "The provided address is invalid";

        protected FilesPageComponentBase()
        {
            AddressBackHistoryStack = new Stack<string>();
            AddressForwardHistoryStack = new Stack<string>();
            TabPageViewModel = new DriveFolderTabPageViewModel();
            CurrentlyOpenIdx = new MutableValueWrapper<int> { Value = -1 };
        }

        protected readonly object SyncRoot = new object();
        protected Stack<string> AddressBackHistoryStack { get; }
        protected Stack<string> AddressForwardHistoryStack { get; }
        protected DriveFolderTabPageViewModel TabPageViewModel { get; }
        protected MutableValueWrapper<int> CurrentlyOpenIdx { get; }

        protected ILocalStorageWrapper LocalStorage { get; set; }
        protected ISessionStorageWrapper SessionStorage { get; set; }
        protected IDriveFolderService DriveFolderService { get; set; }
        protected string InputAddress { get; set; }
        protected string AddressStrValue { get; set; }
        protected string DriveItemId { get; set; }
        protected List<ITabPageHead> TabPageHeadsList { get; set; }
        protected IReadOnlyCollection<IDriveFolder> CurrentDriveFolders { get; set; }
        protected IDriveFolder CurrentlyOpenDriveFolder { get; set; }
        protected bool InvalidAddress { get; set; }
        protected ErrorViewModel ErrorViewModel { get; set; }
        protected bool AddressGoBackBtnDisabled { get; set; } = true;
        protected bool AddressGoParentBtnDisabled { get; set; } = true;
        protected bool AddressGoForwardBtnDisabled { get; set; } = true;

        protected IEnumerable<IDriveFolder>? TabViewPageDriveFolders
        {
            get => CurrentlyOpenDriveFolder?.DriveFoldersList?.Immtbl ?? Enumerable.Empty<IDriveFolder>();
        }

        protected IEnumerable<IDriveItem>? TabViewPageDriveItems
        {
            get => CurrentlyOpenDriveFolder?.DriveItemsList?.Immtbl ?? Enumerable.Empty<IDriveItem>();
        }

        protected async override Task OnAfterRenderAsync(bool firstRender)
        {
            await base.OnAfterRenderAsync(firstRender);

            if (firstRender)
            {
                await IfLocalSessionGuidHasValueAsync(async localSessionGuid =>
                {
                    TryPushAddressToHistory(InputAddress);

                    await LoadCurrentlyOpenFolderAsync(
                        localSessionGuid,
                        InputAddress,
                        false,
                        false);
                });
            }
        }

        protected async Task OnAddressBarGoBackClick(MouseEventArgs args)
        {
            await TryPopAddressFromHistoryAsync(true);
        }

        protected async Task OnAddressBarGoUpClick(MouseEventArgs args)
        {
            var parentFolder = CurrentlyOpenDriveFolder?.ParentFolder.Immtbl;

            if (parentFolder != null)
            {
                string address = DriveFolderService.GetDriveItemAddress(parentFolder);
                await SubmitAddressAsync(address);
            }
        }

        protected async Task OnAddressBarGoForwardClick(MouseEventArgs args)
        {
            await TryPopAddressFromHistoryAsync(false);
        }

        protected async Task OnCurrentlyOpenFolderOptionsClick(MouseEventArgs args)
        {
        }

        protected async Task OnAddressBarReloadClick(MouseEventArgs args)
        {
            await IfLocalSessionGuidHasValueAsync(async localSessionGuid =>
            {
                await LoadCurrentlyOpenFolderAsync(localSessionGuid, DriveItemId, true, false);
            });
        }

        protected async Task OnSubmitAddress(TextEventArgsWrapper args)
        {
            string address = args.Value;
            await SubmitAddressAsync(address);
        }

        protected async Task SubmitAddressAsync(string address)
        {
            await IfLocalSessionGuidHasValueAsync(async localSessionGuid =>
            {
                string absUri = NavManager.AbsUri.AbsoluteUri;
                string uriWithoutQueryString = absUri.GetUriWithoutQueryString(true);

                var queryString = NavManager.QueryStrings;
                queryString[QsKeys.DRIVE_ITEM_ID] = address;

                string targetUrl = QueryHelpers.AddQueryString(
                    uriWithoutQueryString, queryString);

                await LoadCurrentlyOpenFolderAsync(localSessionGuid, address, false, false);
                NavManager.Manager.NavigateTo(targetUrl, false);
            });
        }

        protected async Task OnDriveFolderClickAsync(IDriveItemCore driveFolder)
        {
            string address = DriveFolderService.GetDriveItemAddress(driveFolder);
            await SubmitAddressAsync(address);
        }

        protected async Task OnDriveItemClickAsync(IDriveItemCore driveItem)
        {
        }

        protected async Task LoadCurrentlyOpenFolderAsync(
            Guid localSessionGuid,
            string driveItemId,
            bool refreshCache,
            bool clearErrors)
        {
            if (clearErrors)
            {
                ClearError();
            }

            TryNormalizeAddress(driveItemId);

            if (ErrorViewModel == null)
            {
                await LoadCurrentlyOpenFolderCoreAsync(localSessionGuid, driveItemId, refreshCache);
            }

            StateHasChanged();
        }

        protected async Task LoadCurrentlyOpenFolderCoreAsync(
            Guid localSessionGuid,
            string driveItemId,
            bool refreshCache)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(driveItemId))
                {
                    CurrentlyOpenDriveFolder = await DriveFolderService.GetRootDriveFolderAsync(
                        localSessionGuid, refreshCache);
                }
                else
                {
                    CurrentlyOpenDriveFolder = await DriveFolderService.GetDriveFolderAsync(
                        driveItemId, localSessionGuid, refreshCache);
                }

                AddressGoParentBtnDisabled = CurrentlyOpenDriveFolder.IsRootFolder ?? false;

                CurrentDriveFolders = await DriveFolderService.GetCurrentDriveFoldersAsync(
                    CurrentlyOpenDriveFolder,
                    CurrentlyOpenIdx,
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

        protected void TryNormalizeAddress(string inputAddress)
        {
            InputAddress = inputAddress;

            string normAddress = inputAddress;
            string driveItemId;

            if (!string.IsNullOrWhiteSpace(
                normAddress))
            {
                if (DriveFolderService.TryNormalizeAddress(
                    ref normAddress,
                    out driveItemId))
                {
                    AddressStrValue = normAddress;
                    DriveItemId = driveItemId;
                }
                else
                {
                    SetError(INVALID_ADDRESS_ERR_MSG);
                }
            }
            else
            {
                AddressStrValue = null;
                DriveItemId = null;
            }
        }

        protected void TryPushAddressToHistory(string address)
        {
            lock (SyncRoot)
            {
                AddressBackHistoryStack.Push(address);
                AddressForwardHistoryStack.Clear();
            }
        }

        protected async Task TryPopAddressFromHistoryAsync(bool isBackHistory)
        {
            string address;

            if (TryPopAddressFromHistory(false, out address))
            {
                await SubmitAddressAsync(address);
            }
        }

        protected bool TryPopAddressFromHistory(
            bool isBackHistory,
            out string address)
        {
            bool hasAny;
            bool hasMore;

            lock (SyncRoot)
            {
                if (isBackHistory)
                {
                    hasAny = TryPopAddressFromHistory(
                        AddressBackHistoryStack,
                        AddressForwardHistoryStack,
                        out address,
                        out hasMore);

                    AddressGoForwardBtnDisabled = !hasMore;
                }
                else
                {
                    hasAny = TryPopAddressFromHistory(
                        AddressForwardHistoryStack,
                        AddressBackHistoryStack,
                        out address,
                        out hasMore);

                    AddressGoBackBtnDisabled = !hasMore;
                }
            }

            return hasAny;
        }

        private bool TryPopAddressFromHistory(
            Stack<string> srcAddressHistoryStack,
            Stack<string> destAddressHistoryStack,
            out string address,
            out bool hasMore)
        {
            bool hasAny = srcAddressHistoryStack.TryPop(out address);
            hasMore = srcAddressHistoryStack.Any();

            if (hasAny)
            {
                destAddressHistoryStack.Push(address);
            }

            return hasAny;
        }
    }
}
