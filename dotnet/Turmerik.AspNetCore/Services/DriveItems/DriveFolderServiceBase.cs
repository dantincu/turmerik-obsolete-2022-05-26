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
        public abstract string GetDriveItemAddress(IDriveItemCore item);

        public async Task<IReadOnlyCollection<IDriveFolder>> GetCurrentDriveFoldersAsync(
            IDriveFolder currentlyOpen,
            MutableValueWrapper<int> currentlyOpenIdx,
            Guid cacheKeyGuid)
        {
            var mtblList = await Storage.AddOrUpdateAsync(
                LocalStorageKeys.CurrentDriveFoldersKey(cacheKeyGuid),
                async () => new List<DriveFolderMtbl>(),
                async (List<DriveFolderMtbl> list) =>
                {
                    if (currentlyOpenIdx.Value >= 0)
                    {
                        list[currentlyOpenIdx.Value] = new DriveFolderMtbl
                        {
                            Id = currentlyOpen.Id,
                            Name = currentlyOpen.DisplayName ?? currentlyOpen.Name,
                            Path = currentlyOpen.Path,
                            Uri = currentlyOpen.Uri
                        };
                    }
                    else
                    {
                        var kvp = list.FindVal(
                            (item, idx) => DriveItemsHaveSameAddress(
                                item, currentlyOpen, false));

                        if (kvp.Key < 0)
                        {
                            currentlyOpenIdx.Value = list.Count;

                            list.Add(new DriveFolderMtbl
                            {
                                Id = currentlyOpen.Id,
                                Name = currentlyOpen.DisplayName ?? currentlyOpen.Name,
                                Path = currentlyOpen.Path,
                                Uri = currentlyOpen.Uri
                            });
                        }
                        else
                        {
                            currentlyOpenIdx.Value = kvp.Key;
                        }
                    }

                    return list;
                },
                true,
                true);

            var retList = mtblList.Select(MtblToFolder).RdnlC();
            return retList;
        }

        public async Task<IDriveFolder> GetDriveFolderAsync(string pathOrId, Guid cacheKeyGuid, bool refreshCache)
        {
            IDriveFolder driveFolder = await GetCoreAsync(
                LocalStorageKeys.DriveFoldersKey(cacheKeyGuid, pathOrId),
                refreshCache,
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
                async () => await GetRootDriveFolderCoreAsync(),
                FolderToMtbl,
                MtblToFolder);

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
            Func<TClnbl, TMtbl> mtblFactory,
            Func<TMtbl, TClnbl> clnblFactory)
            where TClnbl : class
            where TMtbl : TClnbl
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
                TMtbl mtbl = await Storage.GetOrCreateAsync(key, async () =>
                {
                    retVal = await mainFactory();
                    TMtbl mtbl = mtblFactory(retVal);

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
