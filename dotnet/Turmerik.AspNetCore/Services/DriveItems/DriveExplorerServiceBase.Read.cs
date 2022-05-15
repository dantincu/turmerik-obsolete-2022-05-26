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
using Turmerik.Core.Delegates;

namespace Turmerik.AspNetCore.Services.DriveItems
{
    public abstract partial class DriveExplorerServiceBase
    {
        protected abstract void AssignDriveFolderIdentifier(
            DriveExplorerServiceArgs args,
            TabPageHistory history);

        private async Task InitializeAsync(
            DriveExplorerServiceArgs args)
        {
            var folderIdentifier = args.FolderIdentifier ?? GetDriveFolderIdentifier(
                args.TabPageUuid, args.Data.TabPageItems,
                (ref TabPageHead tabPage, ref DriveItemIdentifier idnf) => tabPage.IsCurrent = true,
                tabPage => tabPage.IsCurrent = null,
                false) ?? new DriveItemIdentifier
                {
                    IsRootFolder = true
                };

            TryNormalizeDriveFolderIdentifier(
                ref folderIdentifier);

            args.FolderIdentifier = folderIdentifier;

            args.Data.TabPageItems.CurrentlyOpenFolder = await GetDriveFolderAsync(
                args.FolderIdentifier,
                args.CacheKeyGuid,
                false);

            var tabPageHeads = args.Data.TabPageItems.Header.TabPageHeads;

            if (args.FolderIdentifier != null)
            {
                bool addTab = !args.TabPageUuid.HasValue || tabPageHeads.None(
                    head => head.Uuid == args.TabPageUuid.Value);

                if (addTab)
                {
                    foreach (var head in tabPageHeads)
                    {
                        head.IsCurrent = false;
                    }

                    var pageHead = new TabPageHead
                    {
                        IsCurrent = true,
                        Name = args.Data.TabPageItems.CurrentlyOpenFolder.Name,
                        Uuid = args.TabPageUuid ?? Guid.NewGuid(),
                        Id = args.Data.TabPageItems.CurrentlyOpenFolder.Id
                    };

                    tabPageHeads.Add(pageHead);

                    if (!args.TabPageUuid.HasValue)
                    {
                        args.TabPageUuid = pageHead.Uuid;
                    }
                }
            }

            if (!args.TabPageUuid.HasValue)
            {
                args.TabPageUuid = args.Data.TabPageItems.Header.TabPageHeads.Single(
                    head => head.IsCurrent == true).Uuid;
            }

            await AddOrUpdateTabPageHistoryAsync(
                args, async history => history);
        }

        private async Task ReloadCurrentTabAsync(
            DriveExplorerServiceArgs args)
        {
            if (args.FolderIdentifier == null)
            {
                args.FolderIdentifier = GetDriveFolderIdentifier(
                    args.TabPageUuid, args.Data.TabPageItems);
            }
            else
            {
                var identifier = args.FolderIdentifier;
                TryNormalizeDriveFolderIdentifier(ref identifier);

                args.FolderIdentifier = identifier;
            }

            args.Data.TabPageItems.CurrentlyOpenFolder = await GetDriveFolderAsync(
                args.FolderIdentifier, args.CacheKeyGuid, true);
        }

        private async Task NavigateBackAsync(
            DriveExplorerServiceArgs args)
        {
            var historyInstn = await AddOrUpdateTabPageHistoryAsync(
                args,
                async history =>
                {
                    var current = history.BackHistory.Last();
                    history.BackHistory.RemoveAt(history.BackHistory.Count - 1);

                    history.ForwardHistory.Insert(0, current);
                    AssignDriveFolderIdentifier(args, history);

                    return history;
                });
        }

        private async Task NavigateForwardAsync(
            DriveExplorerServiceArgs args)
        {
            var historyInstn = await AddOrUpdateTabPageHistoryAsync(
                args,
                async history =>
                {
                    var current = history.ForwardHistory.First();
                    history.ForwardHistory.RemoveAt(0);

                    history.BackHistory.Add(current);
                    AssignDriveFolderIdentifier(args, history);

                    return history;
                });
        }

        private async Task NavigateToFolderAsync(
            DriveExplorerServiceArgs args)
        {
            var folderIdentifier = args.FolderIdentifier;

            TryNormalizeDriveFolderNavigation(
                ref folderIdentifier,
                args.FolderNavigation);

            args.FolderIdentifier = folderIdentifier;

            var pageHead = args.Data.TabPageItems.Header.TabPageHeads.Single(
                item => item.IsCurrent == true);

            await AddOrUpdateTabPageHistoryAsync(
                args,
                async history =>
                {
                    history.BackHistory.Add(args.FolderNavigation);
                    history.ForwardHistory.Clear();

                    return history;
                });
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

            var newHead = new TabPageHead
            {
                Uuid = Guid.NewGuid()
            };

            tabPageHeads.Add(newHead);
            args.TabPageUuid = newHead.Uuid;

            args.FolderIdentifier = args.FolderIdentifier ?? new DriveItemIdentifier
            {
                IsRootFolder = true,
            };

            await ChangeTabCoreAsync(args);
        }

        private async Task CloseTabAsync(
            DriveExplorerServiceArgs args)
        {
            Guid trgTabPageUuid = args.TrgTabPageUuid ?? ThrowInvalidTabPageUuidException<Guid>(args.TrgTabPageUuid);
            var tabPageHeads = args.Data.TabPageItems.Header.TabPageHeads;

            var kvp = tabPageHeads.FindVal(
                head => head.Uuid == trgTabPageUuid);

            if (kvp.Key < 0)
            {
                ThrowInvalidTabPageUuidException<Guid>(args.TabPageUuid);
            }
            else
            {
                tabPageHeads.RemoveAt(kvp.Key);

                if (kvp.Value.IsCurrent == true)
                {
                    if (tabPageHeads.Count > 0)
                    {
                        int idx = Math.Min(kvp.Key, tabPageHeads.Count - 1);
                        var head = tabPageHeads[idx];

                        args.TabPageUuid = head.Uuid;

                        args.FolderIdentifier = new DriveItemIdentifier
                        {
                            Id = head.Id,
                            Name = head.Name,
                        };

                        args.FolderIdentifier.Address = GetDriveItemAddress(args.FolderIdentifier);
                    }
                    else
                    {
                        var newHead = new TabPageHead
                        {
                            Uuid = Guid.NewGuid()
                        };

                        tabPageHeads.Add(newHead);
                        args.TabPageUuid = newHead.Uuid;

                        args.FolderIdentifier = args.FolderIdentifier ?? new DriveItemIdentifier
                        {
                            IsRootFolder = true,
                        };
                    }

                    await ChangeTabCoreAsync(args);
                }

                var key = LocalStorageKeys.AddressHistoryStackKey(
                    args.CacheKeyGuid,
                    trgTabPageUuid);

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

            var tabPageItems = args.Data.TabPageItems;

            args.Data.TabPageItems.CurrentlyOpenFolder = await GetDriveFolderAsync(
                args.FolderIdentifier, args.CacheKeyGuid, args.RefreshCache);

            var pageHead = tabPageItems.Header.TabPageHeads.Single(
                head => head.IsCurrent == true);

            pageHead.Name = tabPageItems.CurrentlyOpenFolder.Name;
            pageHead.Id = tabPageItems.CurrentlyOpenFolder.Id;

            await AddOrUpdateTabPageHistoryAsync(args,
                async history => history);
        }

        private async Task<TabPageHistory> AddOrUpdateTabPageHistoryAsync(
            DriveExplorerServiceArgs args,
            Func<TabPageHistory, Task<TabPageHistory>> updateFunc)
        {
            var key = LocalStorageKeys.AddressHistoryStackKey(
                args.CacheKeyGuid,
                args.TabPageUuid.Value);

            var history = await WebStorage.AddOrUpdateAsync(key,
                async () => new TabPageHistory
                {
                    InitialId = args.FolderIdentifier.Id,
                    BackHistory = new List<DriveFolderNavigation>(),
                    ForwardHistory = new List<DriveFolderNavigation>()
                },
                async h =>
                {
                    h = await updateFunc(h);
                    return h;
                });

            var tabPageItems = args.Data.TabPageItems;

            tabPageItems.GoBackButtonEnabled = history.BackHistory.Any();
            tabPageItems.GoForwardButtonEnabled = history.ForwardHistory.Any();

            var pageHead = tabPageItems.Header.TabPageHeads.Single(
                head => head.IsCurrent == true);

            try
            {
                tabPageItems.CurrentlyOpenFolder = await GetDriveFolderAsync(
                    args.FolderIdentifier, args.CacheKeyGuid, args.RefreshCache);

                pageHead.Id = tabPageItems.CurrentlyOpenFolder.Id;
                pageHead.Name = tabPageItems.CurrentlyOpenFolder.Name;
            }
            catch
            {
                pageHead.Id = args.FolderIdentifier.Id;
                pageHead.Name = args.FolderIdentifier.Name;

                throw;
            }

            return history;
        }

        private DriveItemIdentifier GetDriveFolderIdentifier(
            Guid? tabUuid,
            DriveItemsViewState viewState,
            RefAction<TabPageHead, DriveItemIdentifier> matchCallback = null,
            Action<TabPageHead> noMatchCallback = null,
            bool throwIfIdnfNull = true)
        {
            DriveItemIdentifier identifier = null;

            matchCallback = matchCallback.FirstNotNull(
                (ref TabPageHead tabPage, ref DriveItemIdentifier idnf) => TryNormalizeDriveFolderIdentifier(ref idnf));

            if (tabUuid.HasValue)
            {
                Guid tabUuidValue = tabUuid.Value;

                foreach (var tabPage in viewState.Header.TabPageHeads)
                {
                    var page = tabPage;

                    if (tabPage.Uuid == tabUuidValue)
                    {
                        identifier = new DriveItemIdentifier
                        {
                            Id = tabPage.Id,
                            Name = tabPage.Name,
                        };

                        if (string.IsNullOrWhiteSpace(tabPage.Id))
                        {
                            identifier.IsRootFolder = true;
                        }

                        matchCallback(ref page, ref identifier);
                    }
                    else
                    {
                        noMatchCallback?.Invoke(tabPage);
                    }
                }
            }

            if (throwIfIdnfNull && identifier == null)
            {
                ThrowInvalidTabPageUuidException<DriveItemIdentifier>(tabUuid);
            }

            return identifier;
        }

        private async Task<DriveFolder> GetDriveFolderAsync(
            DriveItemIdentifier identifier,
            Guid localSessionGuid,
            bool refreshCache)
        {
            DriveFolder driveFolder;

            if (identifier.IsRootFolder || string.IsNullOrWhiteSpace(identifier.Id))
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
