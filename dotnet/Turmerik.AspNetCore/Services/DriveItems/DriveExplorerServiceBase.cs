using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Turmerik.AspNetCore.Infrastructure;
using Turmerik.AspNetCore.Services.LocalSessionStorage;
using Turmerik.Core.Services.DriveItems;
using Turmerik.Core.Services;
using Turmerik.Core.Helpers;

namespace Turmerik.AspNetCore.Services.DriveItems
{
    public abstract class DriveExplorerServiceBase : IDriveExplorerService
    {
        protected readonly IWebStorageWrapper WebStorage;

        protected DriveExplorerServiceBase(IWebStorageWrapper webStorage)
        {
            WebStorage = webStorage ?? throw new ArgumentNullException(nameof(webStorage));
        }

        public async Task NavigateAsync(DriveExplorerServiceArgs args)
        {
            if (args.FolderIdentifier != null)
            {
                TryNormalizeDriveFolderIdentifier(args.FolderIdentifier);
            }

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
                            tabPageHeads.Insert(pageHead.Idx, pageHead);
                        }
                    }

                    await NavigateCoreAsync(args);

                    if (args.Data.TabPageItems.Header.TabPageHeads.None())
                    {
                        var pageHead = new TabPageHead
                        {
                            IsCurrent = true,
                            Name = args.Data.TabPageItems.CurrentlyOpenFolder.Name,
                            Uuid = Guid.NewGuid(),
                            Id = args.Data.TabPageItems.CurrentlyOpenFolder.Id,
                            Idx = 0
                        };

                        tabPageHeads.Add(pageHead);
                    }

                    if (!args.TabPageUuid.HasValue)
                    {
                        args.TabPageUuid = args.Data.TabPageItems.Header.TabPageHeads.Single(
                            head => head.IsCurrent == true).Uuid;
                    }

                    args.Data.TabPageItems.GoUpButtonEnabled = !(args.Data.TabPageItems.CurrentlyOpenFolder.IsRootFolder ?? false);
                    return args.Data.TabPageItems;
                });
        }

        protected abstract Task<DriveFolder> GetDriveFolderCoreAsync(string driveItemId);
        protected abstract Task<DriveFolder> GetRootDriveFolderCoreAsync();

        protected abstract bool TryNormalizeDriveFolderIdentifiersCore(
            DriveFolderIdentifier identifier,
            out string errorMessage);

        protected abstract bool TryNormalizeDriveFolderNavigationCore(
            DriveFolderIdentifier identifier,
            DriveFolderNavigation navigation,
            out string errorMessage);

        private void TryNormalizeDriveFolderIdentifier(
            DriveFolderIdentifier identifier)
        {
            string errorMessage;

            if (identifier == null)
            {
                errorMessage = "Must provide a drive folder identifier";
                throw new InvalidOperationException(errorMessage);
            }
            else if (!TryNormalizeDriveFolderIdentifiersCore(
                identifier,
                out errorMessage))
            {
                throw new InvalidOperationException(errorMessage);
            }
        }

        private void TryNormalizeDriveFolderNavigation(
            DriveFolderIdentifier identifier,
            DriveFolderNavigation navigation)
        {
            string errorMessage;

            if (!TryNormalizeDriveFolderNavigationCore(
                identifier,
                navigation,
                out errorMessage))
            {
                throw new InvalidOperationException(errorMessage);
            }

            TryNormalizeDriveFolderIdentifier(identifier);
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
                    await RefreshCurrentTabAsync(args);
                    break;
                case DriveExplorerActionType.Navigate:
                    args.FolderIdentifier = args.FolderIdentifier ?? new DriveFolderIdentifier();

                    TryNormalizeDriveFolderNavigation(
                        args.FolderIdentifier,
                        args.FolderNavigation);

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
                default:
                    throw new NotSupportedException();
            }
        }

        private async Task InitializeAsync(
            DriveExplorerServiceArgs args)
        {
            args.FolderIdentifier = GetDriveFolderIdentifier(
                args.TabPageUuid, args.Data.TabPageItems,
                (tabPage, idnf) => tabPage.IsCurrent = true,
                tabPage => tabPage.IsCurrent = null,
                false) ?? new DriveFolderIdentifier
                {
                    IsRootFolder = true
                };

            TryNormalizeDriveFolderIdentifier(
                args.FolderIdentifier);

            args.Data.TabPageItems.CurrentlyOpenFolder = await GetDriveFolderAsync(
                args.FolderIdentifier,
                args.CacheKeyGuid,
                false);
        }

        private async Task RefreshCurrentTabAsync(
            DriveExplorerServiceArgs args)
        {
            args.FolderIdentifier = GetDriveFolderIdentifier(
                args.TabPageUuid, args.Data.TabPageItems);

            args.Data.TabPageItems.CurrentlyOpenFolder = await GetDriveFolderAsync(
                args.FolderIdentifier, args.CacheKeyGuid, true);
        }

        private async Task NavigateBackAsync(
            DriveExplorerServiceArgs args)
        {
            var historyInstn = await AddOrUpdateTabPageHistoryAsync(
                args,
                await GetDriveFolderAsync(args.FolderIdentifier, args.CacheKeyGuid, true),
                async (driveFolder, history) =>
                {
                    history.ForwardHistory.Add(history.Current);
                    history.Current = history.BackHistory.Last();

                    args.FolderNavigation = history.Current;
                    return history;
                });
        }

        private async Task NavigateForwardAsync(
            DriveExplorerServiceArgs args)
        {
            var historyInstn = await AddOrUpdateTabPageHistoryAsync(
                args,
                await GetDriveFolderAsync(args.FolderIdentifier, args.CacheKeyGuid, true),
                async (driveFolder, history) =>
                {
                    history.BackHistory.Add(history.Current);
                    history.Current = history.ForwardHistory.First();

                    args.FolderNavigation = history.Current;
                    return history;
                });
        }

        private async Task NavigateToFolderAsync(
            DriveExplorerServiceArgs args)
        {
            var pageHead = args.Data.TabPageItems.Header.TabPageHeads.Single(
                item => item.IsCurrent == true);

            await AddOrUpdateTabPageHistoryAsync(
                args,
                await GetDriveFolderAsync(args.FolderIdentifier, args.CacheKeyGuid, true),
                async (driveFolder, history) =>
                {
                    history.BackHistory.Add(history.Current);

                    history.Current = new DriveFolderNavigation
                    {
                        FolderId = driveFolder.Id
                    };

                    history.ForwardHistory.Clear();
                    return history;
                });

            pageHead.Name = args.Data.TabPageItems.CurrentlyOpenFolder.Name;
            pageHead.Id = args.Data.TabPageItems.CurrentlyOpenFolder.Id;
        }

        private async Task ChangeTabAsync(
            DriveExplorerServiceArgs args)
        {
            args.FolderIdentifier = GetDriveFolderIdentifier(
                args.TabPageUuid, args.Data.TabPageItems);

            await ChangeTabCoreAsync(args);
        }

        private async Task NewTabAsync(
            DriveExplorerServiceArgs args)
        {
            var tabPageHeads = args.Data.TabPageItems.Header.TabPageHeads;
            int idx = args.TabPageIdx ?? tabPageHeads.Count;

            var newHead = new TabPageHead
            {
                Idx = idx,
                Uuid = Guid.NewGuid()
            };

            tabPageHeads.Insert(idx, newHead);
            args.TabPageUuid = newHead.Uuid;

            args.FolderIdentifier = args.FolderIdentifier ?? new DriveFolderIdentifier
            {
                IsRootFolder = true,
            };

            await AddOrUpdateTabPageHistoryAsync(args,
                await GetDriveFolderAsync(args.FolderIdentifier, args.CacheKeyGuid, true),
                async (driveFolder, history) =>
                {
                    history.Current = args.FolderNavigation;
                    return history;
                });

            await ChangeTabCoreAsync(args);
        }

        private async Task CloseTabAsync(
            DriveExplorerServiceArgs args)
        {
            Guid tabPageUuid = args.TabPageUuid ?? ThrowInvalidTabPageUuidException<Guid>(args.TabPageUuid);
            var tabPageHeads = args.Data.TabPageItems.Header.TabPageHeads;

            var kvp = tabPageHeads.FindVal(
                head => head.Uuid == tabPageUuid);

            if (kvp.Key <= 0)
            {
                ThrowInvalidTabPageUuidException<Guid>(args.TabPageUuid);
            }
            else
            {
                tabPageHeads.RemoveAt(kvp.Key);

                if (kvp.Value.IsCurrent == true)
                {
                    int idx = Math.Min(kvp.Key, tabPageHeads.Count - 1);
                    var head = tabPageHeads[idx];

                    args.TabPageUuid = head.Uuid;

                    args.FolderIdentifier = new DriveFolderIdentifier
                    {
                        Id = head.Id
                    };

                    await ChangeTabCoreAsync(args);
                }

                var key = LocalStorageKeys.AddressHistoryStackKey(
                    args.CacheKeyGuid,
                    args.TabPageUuid.Value);

                await WebStorage.TryRemoveItemAsync(key);
            }
        }

        private async Task ChangeTabCoreAsync(
            DriveExplorerServiceArgs args)
        {
            Guid tabPageUuid = args.TabPageUuid ?? ThrowInvalidTabPageUuidException<Guid>(args.TabPageUuid);
            var tabPageHeads = args.Data.TabPageItems.Header.TabPageHeads;

            foreach (var head in tabPageHeads)
            {
                if (head.Uuid == tabPageUuid)
                {
                    head.IsCurrent = true;
                }
                else
                {
                    head.IsCurrent = null;
                }
            }

            args.Data.TabPageItems.CurrentlyOpenFolder = await GetDriveFolderAsync(
                args.FolderIdentifier, args.CacheKeyGuid, args.RefreshCache);
        }

        private async Task<TabPageHistory> AddOrUpdateTabPageHistoryAsync(
            DriveExplorerServiceArgs args,
            DriveFolder driveFolder,
            Func<DriveFolder, TabPageHistory, Task<TabPageHistory>> updateFunc)
        {
            var key = LocalStorageKeys.AddressHistoryStackKey(
                args.CacheKeyGuid,
                args.TabPageUuid.Value);

            var history = await WebStorage.AddOrUpdateAsync(key,
                async () => new TabPageHistory
                {
                    Current = args.FolderNavigation,
                    BackHistory = new List<DriveFolderNavigation>(),
                    ForwardHistory = new List<DriveFolderNavigation>()
                },
                async (historyInstn) => await updateFunc(driveFolder, historyInstn));

            var tabPageItems = args.Data.TabPageItems;
            tabPageItems.CurrentlyOpenFolder = driveFolder;

            tabPageItems.GoBackButtonEnabled = history.BackHistory.Any();
            tabPageItems.GoForwardButtonEnabled = history.ForwardHistory.Any();

            return history;
        }

        private DriveFolderIdentifier GetDriveFolderIdentifier(
            Guid? tabUuid,
            DriveItemsViewState viewState,
            Action<TabPageHead, DriveFolderIdentifier> matchCallback = null,
            Action<TabPageHead> noMatchCallback = null,
            bool throwIfIdnfNull = true)
        {
            DriveFolderIdentifier identifier = null;

            matchCallback = matchCallback.FirstNotNull(
                (tabPage, idnf) => TryNormalizeDriveFolderIdentifier(idnf));

            if (tabUuid.HasValue)
            {
                Guid tabUuidValue = tabUuid.Value;

                foreach (var tabPage in viewState.Header.TabPageHeads)
                {
                    if (tabPage.Uuid == tabUuidValue)
                    {
                        identifier = new DriveFolderIdentifier
                        {
                            Id = tabPage.Id
                        };

                        if (string.IsNullOrWhiteSpace(tabPage.Id))
                        {
                            identifier.IsRootFolder = true;
                        }

                        matchCallback(tabPage, identifier);
                    }
                    else
                    {
                        noMatchCallback(tabPage);
                    }
                }
            }

            if (throwIfIdnfNull && identifier == null)
            {
                ThrowInvalidTabPageUuidException<DriveFolderIdentifier>(tabUuid);
            }

            return identifier;
        }

        private async Task<DriveFolder> GetDriveFolderAsync(
            DriveFolderIdentifier identifier,
            Guid localSessionGuid,
            bool refreshCache)
        {
            DriveFolder driveFolder;

            if (identifier.IsRootFolder)
            {
                driveFolder = await GetRootDriveFolderAsync(localSessionGuid, refreshCache);
            }
            else
            {
                driveFolder = await GetDriveFolderAsync(identifier.Id, localSessionGuid, refreshCache);
                identifier.ParentId = driveFolder.ParentFolderId;
            }

            return driveFolder;
        }

        private async Task<DriveFolder> GetDriveFolderAsync(
            string driveItemId,
            Guid localSessionGuid,
            bool refreshCache)
        {
            string key = LocalStorageKeys.DriveFolderKey(
                localSessionGuid,
                driveItemId);

            DriveFolder driveFolder;

            if (refreshCache)
            {
                driveFolder = await GetDriveFolderCoreAsync(driveItemId);
                await WebStorage.Service.SetItemAsync(key, driveFolder);
            }
            else
            {
                driveFolder = await WebStorage.GetOrCreateAsync(
                    key, async () => await GetDriveFolderCoreAsync(driveItemId));
            }

            return driveFolder;
        }

        private async Task<DriveFolder> GetRootDriveFolderAsync(
            Guid localSessionGuid,
            bool refreshCache)
        {
            string key = LocalStorageKeys.RootDriveFolderKey(localSessionGuid);
            DriveFolder driveFolder;

            if (refreshCache)
            {
                driveFolder = await GetRootDriveFolderCoreAsync();
                await WebStorage.Service.SetItemAsync(key, driveFolder);
            }
            else
            {
                driveFolder = await WebStorage.GetOrCreateAsync(
                    key, async () => await GetRootDriveFolderCoreAsync());
            }

            return driveFolder;
        }

        private T ThrowInvalidTabPageUuidException<T>(Guid? tabPageUuid)
        {
            string uuidStr = null;

            if (tabPageUuid.HasValue)
            {
                uuidStr = tabPageUuid.Value.ToString("N");
            }

            string errorMessage = $"Invalid tab page uuid: {uuidStr}";
            throw new InvalidOperationException(errorMessage);
        }

        private DriveItemsViewState GetDriveItemsViewState()
        {
            var viewState = new DriveItemsViewState
            {
                Header = new TabHeaderViewState
                {
                    TabPageHeads = new List<TabPageHead>()
                }
            };

            return viewState;
        }
    }
}
