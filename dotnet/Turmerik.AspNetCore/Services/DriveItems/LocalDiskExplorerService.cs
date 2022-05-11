using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Turmerik.AspNetCore.Services.LocalSessionStorage;
using Turmerik.Core.FileSystem;
using Turmerik.Core.Helpers;
using Turmerik.Core.Services.DriveItems;
using static System.Environment;

namespace Turmerik.AspNetCore.Services.DriveItems
{
    public class LocalDiskExplorerService : DriveExplorerServiceBase
    {
        private readonly IFsPathNormalizer fsPathNormalizer;

        public LocalDiskExplorerService(
            IWebStorageWrapper webStorage,
            IFsPathNormalizer fsPathNormalizer) : base(webStorage)
        {
            this.fsPathNormalizer = fsPathNormalizer ?? throw new ArgumentNullException(nameof(fsPathNormalizer));
        }

        protected override async Task<DriveFolder> GetDriveFolderCoreAsync(string driveItemId)
        {
            DirectoryInfo dirInfo = new DirectoryInfo(driveItemId);
            var fsEntries = dirInfo.EnumerateFileSystemInfos().ToArray();

            var driveFolder = new DriveFolder
            {
                Id = dirInfo.FullName,
                Name = dirInfo.Name,
                CreationTime = dirInfo.CreationTime,
                LastAccessTime = dirInfo.LastAccessTime,
                LastWriteTime = dirInfo.LastWriteTime,
                FileItems = fsEntries.OfType<FileInfo>().Select(
                    f => new DriveItem
                    {
                        Name = f.Name,
                    }).ToList(),
                FolderItems = fsEntries.OfType<DirectoryInfo>().Select(
                    f => new DriveItem
                    {
                        Name = f.Name,
                    }).ToList()
            };

            return driveFolder;
        }

        protected override async Task<DriveFolder> GetRootDriveFolderCoreAsync()
        {
            var foldersList = new List<DriveItem>();

            var drives = DriveInfo.GetDrives(
                ).Where(d => d.IsReady).Select(
                d => new DriveItem
                {
                    Id = d.Name,
                    Name = GetDriveInfoDisplayName(d),
                    DriveItemType = DriveItemType.FsDriveRoot
                });

            var folders = new Dictionary<SpecialFolder, string>
            {
                { SpecialFolder.UserProfile, "User Home" },
                { SpecialFolder.ApplicationData, "Application Data" },
                { SpecialFolder.MyDocuments, "Documents" },
                { SpecialFolder.MyPictures, "Pictures" },
                { SpecialFolder.MyVideos, "Videos" },
                { SpecialFolder.MyMusic, "Music" },
                { SpecialFolder.Desktop, "Desktop" }
            }.Select(
                kvp => new DriveItem
                {
                    Id = GetFolderPath(kvp.Key),
                    Name = kvp.Value,
                    DriveItemType = DriveItemType.FsSpecialFolder
                });

            foldersList.AddRange(drives);
            foldersList.AddRange(folders);

            var rootFolder = new DriveFolder
            {
                Name = "This PC",
                IsRootFolder = true,
                FolderItems = foldersList
            };

            return rootFolder;
        }

        protected override bool TryNormalizeDriveFolderIdentifiersCore(
            DriveFolderIdentifier identifier,
            out string errorMessage)
        {
            bool isValid;

            if (identifier.IsRootFolder)
            {
                identifier.Id = null;
                identifier.Uri = null;
                identifier.Path = null;
                identifier.Address = null;

                isValid = true;
                errorMessage = null;
            }
            else
            {
                string path = identifier.Id;

                if (string.IsNullOrWhiteSpace(path))
                {
                    path = identifier.Path;
                }

                if (string.IsNullOrWhiteSpace(path))
                {
                    path = identifier.Address;
                }

                if (string.IsNullOrWhiteSpace(path))
                {
                    path = identifier.Uri;
                }

                isValid = !string.IsNullOrWhiteSpace(path);

                if (isValid)
                {
                    var normResult = fsPathNormalizer.TryNormalizePath(path, null);
                    isValid = normResult.IsValid && normResult.IsRooted;

                    if (isValid)
                    {
                        identifier.Path = normResult.NormalizedPath;
                        identifier.Id = normResult.NormalizedPath;
                        identifier.Address = normResult.NormalizedPath;
                        identifier.Uri = $"{FsH.FILE_URI_SCHEME}{normResult.NormalizedPath}";

                        errorMessage = null;
                    }
                    else if (!normResult.IsValid)
                    {
                        errorMessage = "Provided path is invalid";
                    }
                    else
                    {
                        errorMessage = "Normalized path is not rooted";
                    }
                }
                else
                {
                    errorMessage = "Provided identifier property values are all empty or only contain whitespaces";
                }
            }

            return isValid;
        }

        protected override bool TryNormalizeDriveFolderNavigationCore(
            DriveFolderIdentifier identifier,
            DriveFolderNavigation navigation,
            DriveFolderIdentifier parentIdentifier,
            out string errorMessage)
        {
            bool isValid;
            string path = navigation.FolderId;

            if (string.IsNullOrWhiteSpace(path))
            {
                path = identifier.Id;
            }

            if (parentIdentifier != null)
            {
                isValid = !string.IsNullOrWhiteSpace(path);

                if (isValid)
                {
                    path = Path.GetDirectoryName(path);
                    errorMessage = null;
                }
                else
                {
                    errorMessage = "Cannot navigate to parent when the current folder is the root folder";
                }
            }
            else
            {
                isValid = !string.IsNullOrWhiteSpace(navigation.SubFolderName);

                if (isValid)
                {
                    path = Path.Combine(path, navigation.SubFolderName);
                    errorMessage = null;
                }
                else
                {
                    errorMessage = "Sub-folder name must be provided when navigating to a sub-folder";
                }
            }

            if (isValid)
            {
                identifier.Id = path;
            }

            return isValid;
        }

        private bool IsWinDriveRootPath(string dirPath)
        {
            bool retVal = dirPath.EndsWith(":");
            return retVal;
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
    }
}
