using System;
using System.Collections.Generic;
using System.Text;
using Turmerik.AspNetCore.Infrastructure;
using Turmerik.AspNetCore.Services.LocalSessionStorage;
using Turmerik.Core.Cloneable;
using Turmerik.Core.Infrastucture;
using Turmerik.Core.Services.DriveItems;
using Turmerik.Core.Helpers;

namespace Turmerik.AspNetCore.Services.DriveItems
{
    public abstract class DriveFolderServiceBase : ComponentBase, IDriveFolderService
    {
        protected DriveFolderServiceBase(ICloneableMapper clblMapper) : base(clblMapper)
        {
        }

        protected abstract IWebStorageWrapper Storage { get; }

        public abstract bool TryNormalizeAddress(ref string address, out string pathOrId);
        public abstract bool DriveItemsHaveSameAddress(IDriveItemCore trgItem, IDriveItemCore refItem, bool normalizeFirst);

        public async Task<CurrentDriveItemsTuple> GetCurrentDriveItemsAsync(Guid cacheKeyGuid, bool refreshCache)
        {
            var mtbl = await Storage.GetOrCreateAsync(
                LocalStorageKeys.CurrentlyOpenDriveItemKey(cacheKeyGuid),
                async () => FolderToMtbl(await GetRootDriveFolderAsync(cacheKeyGuid, refreshCache)));

            var currentlyOpen = MtblToFolder(mtbl);

            var mtblList = await Storage.AddOrUpdateAsync(
                LocalStorageKeys.CurrentDriveItemsKey(cacheKeyGuid),
                () => new List<DriveItemCoreMtbl>(),
                list =>
                {
                    if (list.None(item => DriveItemsHaveSameAddress(item, currentlyOpen)))
                    {
                        var mtbl = ItemCoreToMtbl(currentlyOpen);
                        list.Add(mtbl);
                    }

                    return list;
                });

            var immtblList = mtblList.Select(MtblToItemCore).RdnlC();
            var tuple = new CurrentDriveItemsTuple(immtblList, currentlyOpen);

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
            var immtbl = new DriveFolderMtbl(Mapper, mtbl);
            return immtbl;
        }

        protected DriveItemCoreMtbl ItemCoreToMtbl(IDriveItemCore immtbl)
        {
            var mtbl = new DriveItemCoreMtbl(Mapper, immtbl);
            return mtbl;
        }

        protected IDriveItemCore MtblToItemCore(DriveItemCoreMtbl mtbl)
        {
            var immtbl = new DriveItemCoreMtbl(Mapper, mtbl);
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
