using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Turmerik.AspNetCore.Infrastructure;
using Turmerik.AspNetCore.Services.LocalSessionStorage;
using Turmerik.Core.Services.DriveItems;
using Turmerik.Core.Helpers;

namespace Turmerik.AspNetCore.Services.DriveItems
{
    public abstract partial class DriveExplorerServiceBase : IDriveExplorerService
    {
        protected readonly IWebStorageWrapper WebStorage;

        protected DriveExplorerServiceBase(IWebStorageWrapper webStorage)
        {
            WebStorage = webStorage ?? throw new ArgumentNullException(nameof(webStorage));
        }

        public async Task NavigateAsync(DriveExplorerServiceArgs args)
        {
            args.Data = args.Data ?? new DriveExplorerData
            {
                TabPageItems = GetDriveItemsViewState()
            };

            await WebStorage.AddOrUpdateAsync(
                LocalStorageKeys.DriveItemsViewStateKey(args.CacheKeyGuid),
                async () => GetDriveItemsViewState(),
                async viewState =>
                {
                    var tabPageHeads = args.Data.TabPageItems.Header.TabPageHeads;

                    foreach (var pageHead in viewState.Header.TabPageHeads)
                    {
                        if (tabPageHeads.None(
                            head => head.Uuid == pageHead.Uuid))
                        {
                            tabPageHeads.Add(pageHead);
                        }
                    }

                    await NavigateCoreAsync(args);

                    if (args.Data.TabPageItems.CurrentlyOpenFolder != null)
                    {
                        args.Data.TabPageItems.GoUpButtonEnabled = !(
                            args.Data.TabPageItems.CurrentlyOpenFolder.IsRootFolder ?? false);
                    }

                    return args.Data.TabPageItems;
                });
        }

        public abstract string GetDriveItemId(DriveItemIdentifier identifier);
        public abstract string GetDriveItemAddress(DriveItemIdentifier identifier);
        public abstract string GetDriveItemPath(DriveItemIdentifier identifier);
        public abstract string GetDriveItemUri(DriveItemIdentifier identifier);

        protected abstract Task<DriveFolder> GetDriveFolderCoreAsync(string driveItemId);
        protected abstract Task<DriveFolder> GetRootDriveFolderCoreAsync();

        protected abstract bool TryNormalizeDriveFolderIdentifiersCore(
            ref DriveItemIdentifier identifier,
            out string errorMessage);

        protected abstract bool TryNormalizeDriveFolderNavigationCore(
            ref DriveItemIdentifier identifier,
            DriveFolderNavigation navigation,
            out string errorMessage);

        protected void TryNormalizeDriveFolderIdentifier(
            ref DriveItemIdentifier identifier)
        {
            string errorMessage;

            if (identifier == null)
            {
                errorMessage = "Must provide a drive folder identifier";
                throw new InvalidOperationException(errorMessage);
            }
            else if (!TryNormalizeDriveFolderIdentifiersCore(
                ref identifier,
                out errorMessage))
            {
                throw new InvalidOperationException(errorMessage);
            }
        }

        private void TryNormalizeDriveFolderNavigation(
            ref DriveItemIdentifier identifier,
            DriveFolderNavigation navigation)
        {
            string errorMessage;

            if (!TryNormalizeDriveFolderNavigationCore(
                ref identifier,
                navigation,
                out errorMessage))
            {
                throw new InvalidOperationException(errorMessage);
            }

            TryNormalizeDriveFolderIdentifier(ref identifier);
        }

        private async Task NavigateCoreAsync(
            DriveExplorerServiceArgs args)
        {
            switch (args.ActionType)
            {
                case DriveExplorerActionType.Initialize:
                    await InitializeAsync(args);
                    break;
                case DriveExplorerActionType.ReloadCurrentTab:
                    await ReloadCurrentTabAsync(args);
                    break;
                case DriveExplorerActionType.Navigate:
                    await NavigateToFolderAsync(args);
                    break;
                case DriveExplorerActionType.NavigateBack:
                    await NavigateBackAsync(args);
                    break;
                case DriveExplorerActionType.NavigateForward:
                    await NavigateForwardAsync(args);
                    break;
                case DriveExplorerActionType.ChangeTab:
                    await ChangeTabAsync(args);
                    break;
                case DriveExplorerActionType.NewTab:
                    await NewTabAsync(args);
                    break;
                case DriveExplorerActionType.CloseTab:
                    await CloseTabAsync(args);
                    break;
                case DriveExplorerActionType.CreateNewFolder:
                    await CreateNewFolderAsync(args);
                    break;
                case DriveExplorerActionType.CreateNewTextFile:
                    await CreateNewTextFileAsync(args);
                    break;
                case DriveExplorerActionType.CreateNewMsOfficeFile:
                    await CreateNewMsOfficeFileAsync(args);
                    break;
                case DriveExplorerActionType.DeleteFolder:
                    await DeleteFolderAsync(args);
                    break;
                case DriveExplorerActionType.DeleteFile:
                    await DeleteFileAsync(args);
                    break;
                case DriveExplorerActionType.RenameFolder:
                    await RenameFolderAsync(args);
                    break;
                case DriveExplorerActionType.RenameFile:
                    await RenameFileAsync(args);
                    break;
                case DriveExplorerActionType.MoveFolder:
                    await MoveFolderAsync(args);
                    break;
                case DriveExplorerActionType.MoveFile:
                    await MoveFileAsync(args);
                    break;
                case DriveExplorerActionType.CopyFolder:
                    await CopyFolderAsync(args);
                    break;
                case DriveExplorerActionType.CopyFile:
                    await CopyFileAsync(args);
                    break;
                default:
                    throw new NotSupportedException();
            }
        }
    }
}
