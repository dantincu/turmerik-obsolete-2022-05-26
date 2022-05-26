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

        public async Task<Tuple<Exception, DriveFolder>> GetDriveFolderTupleAsync(
            string driveItemId,
            Guid localSessionGuid,
            bool refreshCache)
        {
            string key = LocalStorageKeys.DriveFolderKey(
                localSessionGuid,
                driveItemId);

            Tuple<Exception, DriveFolder> driveFolderTuple = await GetDriveFolderTupleAsync(
                key, refreshCache, () => GetDriveFolderTupleCoreAsync(driveItemId));

            return driveFolderTuple;
        }

        private async Task<Tuple<Exception, DriveFolder>> GetRootDriveFolderTupleAsync(
            Guid localSessionGuid,
            bool refreshCache)
        {
            string key = LocalStorageKeys.RootDriveFolderKey(localSessionGuid);

            Tuple<Exception, DriveFolder> driveFolderTuple = await GetDriveFolderTupleAsync(
                key, refreshCache, GetRootDriveFolderTupleCoreAsync);

            return driveFolderTuple;
        }

        private async Task<Tuple<Exception, DriveFolder>> GetDriveFolderTupleAsync(
            string key, bool refreshCache,
            Func<Task<Tuple<Exception, DriveFolder>>> tupleFactory)
        {
            Tuple<Exception, DriveFolder> driveFolderTuple = null;

            if (refreshCache)
            {
                driveFolderTuple = await tupleFactory();
                await WebStorage.Service.SetItemAsync(key, driveFolderTuple);
            }
            else
            {
                var driveFolder = await WebStorage.GetOrCreateAsync(
                    key, async () =>
                    {
                        driveFolderTuple = await tupleFactory();
                        return driveFolderTuple.Item2;
                    });

                driveFolderTuple = driveFolderTuple ?? new Tuple<Exception, DriveFolder>(
                    null, driveFolder);
            }

            return driveFolderTuple;
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

        protected async Task<Tuple<Exception, DriveFolder>> GetDriveFolderTupleCoreAsync(string driveItemId)
        {
            var tuple = await GetDriveFolderTupleCoreAsync(
                () => GetDriveFolderCoreAsync(driveItemId));

            return tuple;
        }

        protected async Task<Tuple<Exception, DriveFolder>> GetRootDriveFolderTupleCoreAsync()
        {
            var tuple = await GetDriveFolderTupleCoreAsync(
                GetRootDriveFolderCoreAsync);

            return tuple;
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
                case DriveExplorerActionType.CreateNewFolderInCurrent:
                    await CreateNewFolderInCurrentAsync(args);
                    break;
                case DriveExplorerActionType.CreateNewFolderInSelected:
                    await CreateNewFolderInSelectedAsync(args);
                    break;
                case DriveExplorerActionType.CreateNewTextFileInCurrent:
                    await CreateNewTextFileInCurrentAsync(args);
                    break;
                case DriveExplorerActionType.CreateNewTextFileInSelected:
                    await CreateNewTextFileInSelectedAsync(args);
                    break;
                case DriveExplorerActionType.CreateNewMsOfficeFileInCurrent:
                    await CreateNewMsOfficeFileInCurrentAsync(args);
                    break;
                case DriveExplorerActionType.CreateNewMsOfficeFileInSelected:
                    await CreateNewMsOfficeFileInSelectedAsync(args);
                    break;
                case DriveExplorerActionType.DeleteCurrentFolder:
                    await DeleteCurrentFolderAsync(args);
                    break;
                case DriveExplorerActionType.DeleteSelectedFolder:
                    await DeleteSelectedFolderAsync(args);
                    break;
                case DriveExplorerActionType.DeleteFile:
                    await DeleteFileAsync(args);
                    break;
                case DriveExplorerActionType.RenameCurrentFolder:
                    await RenameCurrentFolderAsync(args);
                    break;
                case DriveExplorerActionType.RenameSelectedFolder:
                    await RenameSelectedFolderAsync(args);
                    break;
                case DriveExplorerActionType.RenameFile:
                    await RenameFileAsync(args);
                    break;
                case DriveExplorerActionType.MoveCurrentFolder:
                    await MoveCurrentFolderAsync(args);
                    break;
                case DriveExplorerActionType.MoveSelectedFolder:
                    await MoveSelectedFolderAsync(args);
                    break;
                case DriveExplorerActionType.MoveFile:
                    await MoveFileAsync(args);
                    break;
                case DriveExplorerActionType.CopyCurrentFolder:
                    await CopyCurrentFolderAsync(args);
                    break;
                case DriveExplorerActionType.CopySelectedFolder:
                    await CopySelectedFolderAsync(args);
                    break;
                case DriveExplorerActionType.CopyFile:
                    await CopyFileAsync(args);
                    break;
                default:
                    throw new NotSupportedException();
            }
        }

        protected async Task<Tuple<Exception, DriveFolder>> GetDriveFolderTupleCoreAsync(
            Func<Task<DriveFolder>> factory)
        {
            DriveFolder driveFolder = null;
            Exception exception = null;

            try
            {
                driveFolder = await factory();
            }
            catch (Exception exc)
            {
                exception = exc;
            }

            return new Tuple<Exception, DriveFolder>(
                exception,
                driveFolder);
        }
    }
}
