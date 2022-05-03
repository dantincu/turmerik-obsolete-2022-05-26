using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Turmerik.AspNetCore.Services.LocalSessionStorage;
using Turmerik.Core.Cloneable;
using Turmerik.Core.FileSystem;
using Turmerik.Core.Helpers;
using Turmerik.Core.Services.DriveItems;
using static System.Environment;

namespace Turmerik.AspNetCore.Services.DriveItems
{
    public class FileSystemFolderService : DriveFolderServiceBase, IDriveFolderService
    {
        public FileSystemFolderService(
            ICloneableMapper clblMapper,
            IFsPathNormalizer fsPathNormalizer,
            ISessionStorageWrapper sessionStorageWrapper) : base(
                clblMapper,
                fsPathNormalizer)
        {
            Storage = sessionStorageWrapper;
        }

        protected override IWebStorageWrapper Storage { get; }

        public override bool TryNormalizeAddress(ref string address, out string pathOrId)
        {
            var normalizerResult = FsPathNormalizer.TryNormalizePath(address);
            bool retVal = normalizerResult.IsValid;

            if (retVal)
            {
                address = normalizerResult.NormalizedPath;

                if (normalizerResult.IsAbsUri == true && address.StartsWith(
                    FsH.FILE_URI_SCHEME,
                    StringComparison.InvariantCultureIgnoreCase))
                {
                    address = address.Substring(FsH.FILE_URI_SCHEME.Length);
                }
            }
            else
            {
                address = null;
            }

            pathOrId = address;
            return retVal;
        }

        public override bool DriveItemsHaveSameAddress(IDriveItemCore trgItem, IDriveItemCore refItem, bool normalizeFirst)
        {
            string trgPath = trgItem.Path;
            string refPath = refItem.Path;

            bool retVal = true;

            if (normalizeFirst)
            {
                retVal = TryNormalizeAddress(ref trgPath, out trgPath);
                retVal = retVal && TryNormalizeAddress(ref refPath, out refPath);
            }

            retVal = retVal && trgPath.StrEquals(refPath, true);
            return retVal;
        }

        protected override async Task<IDriveFolder> GetDriveFolderCoreAsync(string pathOrId)
        {
            IDriveFolder driveFolderImmtbl = null;

            if (TryNormalizeAddress(ref pathOrId, out pathOrId))
            {
                string[] files = Directory.GetFiles(pathOrId);
                string[] folders = Directory.GetDirectories(pathOrId);

                var mtblFiles = files.Select(GetDriveItemMtbl).ToList();
                var mtblFolders = folders.Select(GetDriveFolderMtbl).ToList();

                var driveFolder = GetDriveFolderMtbl(pathOrId);

                driveFolder.DriveFoldersList = new DriveFoldersList(null, mtblFolders);
                driveFolder.DriveItemsList = new DriveItemsList(null, mtblFiles);

                driveFolderImmtbl = new DriveFolderImmtbl(Mapper, driveFolder);
            }

            return driveFolderImmtbl;
        }

        protected override async Task<IDriveFolder> GetRootDriveFolderCoreAsync()
        {
            var mtbl = GetRootDriveFolder();
            var immtbl = new DriveFolderImmtbl(Mapper, mtbl);

            return immtbl;
        }

        private DriveFolderMtbl GetDriveFolderMtbl(string fsEntryPath)
        {
            var mtbl = new DriveFolderMtbl
            {
                IsFolder = true,
            };

            FillDriveItemCoreMtblProps(mtbl, fsEntryPath);
            return mtbl;
        }

        private DriveItemMtbl GetDriveItemMtbl(string fsEntryPath)
        {
            var mtbl = new DriveItemMtbl
            {
                Extension = Path.GetExtension(fsEntryPath)?.TrimStart('.'),
                NameWithoutExtension = Path.GetFileNameWithoutExtension(fsEntryPath),
                IsFolder = false,
            };

            FillDriveItemCoreMtblProps(mtbl, fsEntryPath);
            return mtbl;
        }

        private void FillDriveItemCoreMtblProps(DriveItemCoreMtbl mtbl, string fsEntryPath)
        {
            mtbl.Name = Path.GetFileName(fsEntryPath);
            mtbl.Path = fsEntryPath;
        }

        private List<DriveFolderMtbl> GetRootDriveFoldersList()
        {
            var rootFoldersList = new List<DriveFolderMtbl>();

            AddFileSystemDrives(rootFoldersList);
            AddSpecialFolders(rootFoldersList);

            return rootFoldersList;
        }

        private void AddSpecialFolders(List<DriveFolderMtbl> rootFoldersList)
        {
            AddSpecialFolder(rootFoldersList, SpecialFolder.UserProfile, "User Home");
            AddSpecialFolder(rootFoldersList, SpecialFolder.ApplicationData, "Application Data");
            AddSpecialFolder(rootFoldersList, SpecialFolder.MyDocuments, "Documents");
            AddSpecialFolder(rootFoldersList, SpecialFolder.MyPictures, "Pictures");
            AddSpecialFolder(rootFoldersList, SpecialFolder.MyVideos, "Videos");
            AddSpecialFolder(rootFoldersList, SpecialFolder.MyMusic, "Music");
            AddSpecialFolder(rootFoldersList, SpecialFolder.Desktop, "Desktop");
        }

        private void AddSpecialFolder(
            List<DriveFolderMtbl> rootFoldersList,
            SpecialFolder specialFolder,
            string displayName)
        {
            string path = GetFolderPath(specialFolder);
            string name = Path.GetFileName(path);

            var mtbl = new DriveFolderMtbl
            {
                Name = name,
                DisplayName = displayName,
                Path = path,
                FileSystemFolderType = FileSystemFolderType.SpecialFolder
            };

            rootFoldersList.Add(mtbl);
        }

        private void AddFileSystemDrives(List<DriveFolderMtbl> rootFoldersList)
        {
            DriveInfo[] drives = DriveInfo.GetDrives();

            var drivesArr = drives.Where(d => d.IsReady).Select(
                d => new DriveFolderMtbl
                {
                    Name = d.Name,
                    DisplayName = GetDriveInfoDisplayName(d),
                    Path = d.Name,
                    FileSystemFolderType = FileSystemFolderType.DriveRoot
                }).ToArray();

            rootFoldersList.AddRange(drivesArr);
        }

        private string GetDriveInfoDisplayName(DriveInfo info)
        {
            string displayName = info.Name;

            if (!string.IsNullOrWhiteSpace(info.VolumeLabel))
            {
                displayName = string.Join(
                    " ",
                    displayName,
                    $"{(info.VolumeLabel)}");
            }

            return displayName;
        }

        private DriveFolderMtbl GetRootDriveFolder()
        {
            var rootFoldersList = GetRootDriveFoldersList();

            var mtbl = new DriveFolderMtbl
            {
                Name = "This PC",
                IsFolder = true,
                IsRootFolder = true,
                DriveFoldersList = new DriveFoldersList(null, rootFoldersList)
            };

            return mtbl;
        }
    }
}
