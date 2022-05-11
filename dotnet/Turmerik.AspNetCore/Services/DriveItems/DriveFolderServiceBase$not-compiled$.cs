using System;
using System.Collections.Generic;
using System.Text;
using Turmerik.AspNetCore.Infrastructure;
using Turmerik.AspNetCore.Services.LocalSessionStorage;
using Turmerik.Core.Cloneable;
using Turmerik.Core.Infrastucture;
using Turmerik.Core.Services.DriveItems;
using Turmerik.Core.Helpers;
using Turmerik.Core.FileSystem;
using Turmerik.Core.Data;

namespace Turmerik.AspNetCore.Services.DriveItems
{
    public abstract class DriveFolderServiceBase : ComponentBase, IDriveExplorerService
    {
        protected DriveFolderServiceBase(
            ICloneableMapper clblMapper,
            IFsPathNormalizer fsPathNormalizer) : base(clblMapper)
        {
            this.FsPathNormalizer = fsPathNormalizer ?? throw new ArgumentNullException(nameof(fsPathNormalizer));
        }

        public abstract DriveItemIdentifierType PreferredIdentifierType { get; }
        protected abstract IWebStorageWrapper Storage { get; }
        protected IFsPathNormalizer FsPathNormalizer { get; }

        public abstract bool IdentifiersAreEquivalent(IDriveItemIdentifier trgIdnf, IDriveItemIdentifier refIdnf);
        public abstract IDriveItemIdentifier GetDriveItemIdentifier(string address);
        public abstract string GetDriveItemAddress(IDriveItemIdentifier idnf);

        public async Task<ITabViewState> GetCurrentTabViewStateAsync(
            MutableValueWrapper<Guid?> currentlyOpenUuid,
            MutableValueWrapper<IDriveItemIdentifier> currentlyOpenIdnf,
            MutableValueWrapper<string> address,
            Guid cacheKeyGuid)
        {
            var tabViewStateMtbl = await Storage.AddOrUpdateAsync(
                LocalStorageKeys.DriveItemsViewStateKey(cacheKeyGuid),
                async () => new TabViewStateMtbl
                {
                    TabPageHeadsList = new TabPageHeadsList(null, new List<TabPageHeadMtbl>())
                },
                async (TabViewStateMtbl state) =>
                {
                    DriveFolderMtbl currentlyOpenFolder;

                    if (currentlyOpenIdnf != null)
                    {

                    }
                    else if (address != null)
                    {
                        
                    }
                    else if (currentlyOpenUuid != null)
                    {

                    }
                    else
                    {
                        var tabPageHead = state.TabPageHeadsList.Mtbl.FirstOrDefault();

                        if (tabPageHead == null)
                        {
                            currentlyOpenFolder = FolderToMtbl(await GetRootDriveFolderAsync(cacheKeyGuid, false));
                            state.CurrentlyOpenFolder = new DriveFolder(null, currentlyOpenFolder);

                            tabPageHead = new TabPageHeadMtbl
                            {
                                DriveFolder = state.CurrentlyOpenFolder,

                            };
                        }
                    }

                    /* bool foundMatch = false;

                    if (currentlyOpenUuid.Value.HasValue)
                    {
                        var uuid = currentlyOpenUuid.Value.Value;

                        foreach (var mtbl in list)
                        {
                            if (mtbl.Uuid == uuid)
                            {
                                if (currentlyOpen != null)
                                {
                                    if (DriveItemsHaveSameIdentifiers(
                                        mtbl.DriveFolder, currentlyOpen, true))
                                    {
                                        mtbl.IsCurrent = true;
                                        foundMatch = true;
                                    }
                                }
                                else
                                {
                                    mtbl.IsCurrent = true;
                                    foundMatch = true;
                                }
                            }
                            else
                            {
                                mtbl.IsCurrent = false;
                            }
                        }
                    }

                    if (!foundMatch)
                    {
                        if (currentlyOpen != null)
                        {
                            foreach (var mtbl in list)
                            {
                                mtbl.IsCurrent = false;
                            }

                            var kvp = list.FindVal(
                                item => DriveItemsHaveSameIdentifiers(
                                item.DriveFolder,
                                currentlyOpen,
                                true));

                            if (kvp.Key >= 0)
                            {
                                var match = list[kvp.Key];
                                match.IsCurrent = true;

                                currentlyOpenUuid.Value = match.Uuid;
                                match.DriveFolder = GetDriveFolderMtblForTabPageHead(currentlyOpen);
                            }
                            else
                            {
                                var uuid = currentlyOpenUuid.Value ?? Guid.NewGuid();
                                currentlyOpenUuid.Value = uuid;

                                var newMtbl = GetTabPageHeadMtbl(currentlyOpen, uuid);
                                list.Add(newMtbl);
                            }
                        }
                    }*/

                    return state;
                });

            var tabViewStateImmtbl = new TabViewStateImmtbl(Mapper, tabViewStateMtbl);
            return tabViewStateImmtbl;
        }

        public async Task<IDriveFolder> GetDriveFolderAsync(IDriveItemIdentifier identifier, Guid cacheKeyGuid, bool refreshCache)
        {
            IDriveFolder driveFolder = await GetCoreAsync(
                LocalStorageKeys.DriveFoldersKey(cacheKeyGuid, pathOrId),
                refreshCache,
                pathOrId,
                async () => await GetDriveFolderCoreAsync(pathOrId),
                FolderToMtbl,
                MtblToFolder);

            return driveFolder;
        }

        public async Task<IDriveFolder> GetRootDriveFolderAsync(Guid cacheKeyGuid, bool refreshCache)
        {
            IDriveFolder rootFolder = await GetCoreAsync(
                LocalStorageKeys.RootDriveFolderKey(cacheKeyGuid),
                refreshCache,
                null,
                async () => await GetRootDriveFolderCoreAsync(),
                FolderToMtbl,
                MtblToFolder);

            return rootFolder;
        }

        protected abstract Task<DriveFolderMtbl> GetDriveFolderCoreAsync(IDriveItemIdentifier identifier);
        protected abstract Task<DriveFolderMtbl> GetRootDriveFolderCoreAsync();

        protected DriveFolderMtbl FolderToMtbl(IDriveFolder immtbl)
        {
            var mtbl = new DriveFolderMtbl(Mapper, immtbl);
            return mtbl;
        }

        protected IDriveFolder MtblToFolder(DriveFolderMtbl mtbl)
        {
            var immtbl = new DriveFolderImmtbl(Mapper, mtbl);
            return immtbl;
        }

        protected DriveItemCoreMtbl ItemCoreToMtbl(IDriveItemIdentifier immtbl)
        {
            var mtbl = new DriveItemCoreMtbl(Mapper, immtbl);
            return mtbl;
        }

        protected IDriveItemIdentifier MtblToItemCore(DriveItemCoreMtbl mtbl)
        {
            var immtbl = new DriveItemCoreImmtbl(Mapper, mtbl);
            return immtbl;
        }

        private DriveFolderMtbl GetDriveFolderMtblForTabPageHead(IDriveFolder driveFolder)
        {
            var driveFolderMtbl = new DriveFolderMtbl
            {
                Id = driveFolder.Id,
                Name = driveFolder.DisplayName ?? driveFolder.Name,
                Path = driveFolder.Path,
                Uri = driveFolder.Uri
            };

            return driveFolderMtbl;
        }

        private TabPageHeadMtbl GetTabPageHeadMtbl(IDriveFolder driveFolder, Guid uuid)
        {
            var mtbl = new TabPageHeadMtbl
            {
                Uuid = uuid,
                IsCurrent = true,
                DriveFolder = GetDriveFolderMtblForTabPageHead(driveFolder),
                Name = driveFolder.Name,
            };

            return mtbl;
        }

        private async Task<IDriveFolder> GetCoreAsync(
            string key,
            bool refreshCache,
            IDriveItemIdentifier identifier,
            Func<IDriveItemIdentifier, Task<DriveFolderMtbl>> factory)
        {
            DriveFolderMtbl dvFolder = null;

            if (refreshCache)
            {
                dvFolder = await factory(identifier);
                await Storage.Service.SetItemAsync(key, dvFolder);
            }
            else
            {
                dvFolder = await Storage.AddOrUpdateAsync(key,
                    async () => await factory(identifier),
                    async (dvFldr) => dvFldr);
            }

            var dvFolderImmtbl = new DriveFolderImmtbl(Mapper, dvFolder);
            return dvFolderImmtbl;
        }

        private async Task<TClnbl> GetCoreAsync<TClnbl, TMtbl>(
            string key,
            bool refreshCache,
            string idnf,
            Func<Task<TClnbl>> mainFactory,
            Func<TClnbl, TMtbl> mtblFactory,
            Func<TMtbl, TClnbl> clnblFactory)
            where TClnbl : class
            where TMtbl : IDriveItemIdentifier
        {
            TClnbl retVal = null;

            if (refreshCache)
            {
                retVal = await mainFactory();
                TMtbl mtbl = mtblFactory(retVal);

                await Storage.Service.SetItemAsync(key, mtbl);
            }
            else
            {
                TMtbl mtbl = await Storage.AddOrUpdateAsync(
                    key,
                    async () =>
                    {
                        retVal = await mainFactory();
                        TMtbl mtbl = mtblFactory(retVal);

                        return mtbl;
                    },
                    async mtbl =>
                    {
                        if (!string.IsNullOrWhiteSpace(
                            idnf) && !IdentifiersAreEquivalent(
                            idnf, GetDriveItemIdentifier(mtbl), true))
                        {
                            retVal = await mainFactory();
                            mtbl = mtblFactory(retVal);
                        }

                        return mtbl;
                    },
                    true,
                    true);

                if (retVal == null)
                {
                    retVal = clnblFactory(mtbl);
                }
            }

            return retVal;
        }
    }
}
