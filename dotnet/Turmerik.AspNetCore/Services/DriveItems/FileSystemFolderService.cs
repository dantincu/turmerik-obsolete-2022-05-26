using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Turmerik.AspNetCore.Services.LocalSessionStorage;
using Turmerik.Core.Cloneable;
using Turmerik.Core.Services.DriveItems;
using static System.Environment;

namespace Turmerik.AspNetCore.Services.DriveItems
{
    public class FileSystemFolderService : DriveFolderServiceBase, IDriveFolderService
    {
        public FileSystemFolderService(
            ICloneableMapper clblMapper,
            ISessionStorageWrapper sessionStorageWrapper) : base(clblMapper)
        {
            Storage = sessionStorageWrapper;
        }

        protected override IWebStorageWrapper Storage { get; }

        public override bool TryNormalizeAddress(ref string address, out string pathOrId)
        {
            throw new NotImplementedException();
        }

        public override bool DriveItemsHaveSameAddress(IDriveItemCore trgItem, IDriveItemCore refItem, bool normalizeFirst)
        {
            throw new NotImplementedException();
        }

        protected override async Task<IDriveFolder> GetDriveFolderCoreAsync(string pathOrId)
        {
            throw new NotImplementedException();
        }

        protected override async Task<IDriveFolder> GetRootDriveFolderCoreAsync()
        {
            var mtbl = GetRootDriveFolder();
            var immtbl = new DriveFolderImmtbl(Mapper, mtbl);

            return immtbl;
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

            var drivesArr = drives.Select(
                d => new DriveFolderMtbl
                {
                    Name = d.Name,
                    DisplayName = $"{d.Name} ({d.VolumeLabel})",
                    Path = d.Name,
                    FileSystemFolderType = FileSystemFolderType.DriveRoot
                }).ToArray();

            rootFoldersList.AddRange(drivesArr);
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
