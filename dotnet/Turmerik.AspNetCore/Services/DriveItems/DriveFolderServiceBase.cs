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

namespace Turmerik.AspNetCore.Services.DriveItems
{
    public abstract class DriveFolderServiceBase : ComponentBase, IDriveFolderService
    {
        protected DriveFolderServiceBase(
            ICloneableMapper clblMapper,
            IFsPathNormalizer fsPathNormalizer) : base(clblMapper)
        {
            this.FsPathNormalizer = fsPathNormalizer ?? throw new ArgumentNullException(nameof(fsPathNormalizer));
        }

        protected abstract IWebStorageWrapper Storage { get; }
        protected IFsPathNormalizer FsPathNormalizer { get; }

        public abstract bool TryNormalizeAddress(ref string address, out string pathOrId);
        public abstract bool DriveItemsHaveSameAddress(IDriveItemCore trgItem, IDriveItemCore refItem, bool normalizeFirst);

        public async Task<CurrentDriveFoldersTuple> GetCurrentDriveFoldersAsync(Guid cacheKeyGuid, bool refreshCache)
        {
            var mtbl = await Storage.GetOrCreateAsync(
                LocalStorageKeys.CurrentlyOpenDriveFolderKey(cacheKeyGuid),
                async () => FolderToMtbl(await GetRootDriveFolderAsync(cacheKeyGuid, refreshCache)));

            var currentlyOpen = MtblToFolder(mtbl);
            int currentlyOpenIdx = -1;

            var mtblList = await Storage.AddOrUpdateAsync(
                LocalStorageKeys.CurrentDriveFoldersKey(cacheKeyGuid),
                () => new List<DriveFolderMtbl>(),
                list =>
                {
                    list = list ?? new List<DriveFolderMtbl>();

                    var match = list.FindVal(item => DriveItemsHaveSameAddress(item, currentlyOpen, false));
                    currentlyOpenIdx = match.Key;

                    if (currentlyOpenIdx < 0)
                    {
                        var mtbl = new DriveFolderMtbl
                        {
                            Id = currentlyOpen.Id,
                            Name = currentlyOpen.DisplayName ?? currentlyOpen.Name,
                            Path = currentlyOpen.Path,
                            Uri = currentlyOpen.Uri
                        };

                        currentlyOpenIdx = list.Count;
                        list.Add(mtbl);
                    }

                    return list;
                });

            var immtblList = mtblList.Select(MtblToFolder).RdnlC();

            var tuple = new CurrentDriveFoldersTuple(
                immtblList,
                currentlyOpen,
                currentlyOpenIdx);

            return tuple;
        }

        public async Task<IDriveFolder> GetDriveFolderAsync(string pathOrId, Guid cacheKeyGuid, bool refreshCache)
        {
            IDriveFolder driveFolder = await GetCoreAsync(
                LocalStorageKeys.DriveFolderKey(cacheKeyGuid, pathOrId),
                refreshCache,
                async () => await GetDriveFolderCoreAsync(pathOrId),
                FolderToMtbl);

            return driveFolder;
        }

        public async Task<IDriveFolder> GetRootDriveFolderAsync(Guid cacheKeyGuid, bool refreshCache)
        {
            IDriveFolder rootFolder = await GetCoreAsync(
                LocalStorageKeys.RootDriveFolderKey(cacheKeyGuid),
                refreshCache,
                async () => await GetRootDriveFolderCoreAsync(),
                FolderToMtbl);

            return rootFolder;
        }

        protected abstract Task<IDriveFolder> GetDriveFolderCoreAsync(string pathOrId);
        protected abstract Task<IDriveFolder> GetRootDriveFolderCoreAsync();

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

        protected DriveItemCoreMtbl ItemCoreToMtbl(IDriveItemCore immtbl)
        {
            var mtbl = new DriveItemCoreMtbl(Mapper, immtbl);
            return mtbl;
        }

        protected IDriveItemCore MtblToItemCore(DriveItemCoreMtbl mtbl)
        {
            var immtbl = new DriveItemCoreImmtbl(Mapper, mtbl);
            return immtbl;
        }

        private async Task<TClnbl> GetCoreAsync<TClnbl, TMtbl>(
            string key,
            bool refreshCache,
            Func<Task<TClnbl>> mainFactory,
            Func<TClnbl, TMtbl> mtblFactory)
            where TClnbl : class
            where TMtbl : TClnbl
        {
            TClnbl retVal = null;

            Func<Task<TMtbl>> factory = async () =>
            {
                retVal = await mainFactory();
                TMtbl mtbl = mtblFactory(retVal);

                return mtbl;
            };

            if (refreshCache)
            {
                TMtbl mtbl = await factory();
                await Storage.Service.SetItemAsync(key, mtbl);
            }
            else
            {
                await Storage.GetOrCreateAsync(key, factory);
            }

            return retVal;
        }
    }
}
