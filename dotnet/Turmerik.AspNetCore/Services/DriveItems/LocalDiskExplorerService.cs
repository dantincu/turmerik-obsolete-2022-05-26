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
            ISessionStorageWrapper webStorage,
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
                FileItems = fsEntries.OfType<FileInfo>().Select(GetDriveItem).ToList(),
                FolderItems = fsEntries.OfType<DirectoryInfo>().Select(GetDriveItem).ToList(),
            };

            return driveFolder;
        }

        protected override async Task<DriveFolder> GetRootDriveFolderCoreAsync()
        {
            var foldersList = new List<DriveItem>();

            var drives = DriveInfo.GetDrives(
                ).Where(d => d.IsReady).Select(
                d =>
                {
                    string label = d.VolumeLabel;

                    if (!string.IsNullOrWhiteSpace(label))
                    {
                        label = $"({label})";
                    }
                    else
                    {
                        label = null;
                    }

                    var item = new DriveItem
                    {
                        Id = d.Name,
                        Name = d.Name,
                        Label = label,
                        DriveItemType = DriveItemType.FsDriveRoot
                    };

                    return item;
                });

            string userHomePath = GetFolderPath(SpecialFolder.UserProfile);

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
                kvp =>
                {
                    string path = GetFolderPath(kvp.Key);
                    string label = path;

                    if (label.StartsWith(userHomePath))
                    {
                        label = label.Substring(userHomePath.Length).TrimStart('/', '\\');
                        label = $"~{Path.DirectorySeparatorChar}{label}";
                    }

                    label = $"({label})";
                    DirectoryInfo d = new DirectoryInfo(path);

                    var item = GetDriveItem(d);
                    item.Id = path;

                    item.Name = kvp.Value;
                    item.Label = label;
                    item.DriveItemType = DriveItemType.FsSpecialFolder;

                    return item;
                });

            foldersList.AddRange(drives);
            foldersList.AddRange(folders);

            var rootFolder = new DriveFolder
            {
                Name = "This PC",
                IsRootFolder = true,
                FolderItems = foldersList,
                FileItems = new List<DriveItem>()
            };

            return rootFolder;
        }

        protected override bool TryNormalizeDriveFolderIdentifiersCore(
            ref DriveFolderIdentifier identifier,
            out string errorMessage)
        {
            bool isValid = true;
            errorMessage = null;

            identifier = identifier ?? new DriveFolderIdentifier();

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

            if (!string.IsNullOrWhiteSpace(path))
            {
                var normResult = fsPathNormalizer.TryNormalizePath(path, null, true);
                isValid = normResult.IsValid && normResult.NormalizedPath.FirstOrDefault() != '.';

                if (isValid)
                {
                    path = normResult.NormalizedPath;

                    if (normResult.IsAbsUri == true)
                    {
                        isValid = path.StartsWithStr(FsH.FILE_URI_SCHEME, true);

                        if (isValid)
                        {
                            path = path.Substring(FsH.FILE_URI_SCHEME.Length);
                        }
                    }
                }

                if (isValid)
                {
                    if (path.IsWinDrive())
                    {
                        path = $"{path}\\";
                    }

                    identifier.Path = path;
                    identifier.Id = path;
                    identifier.Address = path;
                    identifier.Uri = $"{FsH.FILE_URI_SCHEME}{path}";

                    errorMessage = null;
                }
                else if (!normResult.IsValid)
                {
                    errorMessage = "Provided path is invalid";
                }
                else if (normResult.IsAbsUri == true)
                {
                    errorMessage = "Only file path or file path uri is accepted";
                }
                else
                {
                    errorMessage = "Normalized path is not rooted";
                }
            }

            return isValid;
        }

        protected override bool TryNormalizeDriveFolderNavigationCore(
            ref DriveFolderIdentifier identifier,
            DriveFolderNavigation navigation,
            out string errorMessage)
        {
            errorMessage = null;
            bool isValid = true;

            string path = string.Empty;
            identifier = identifier ?? new DriveFolderIdentifier();

            if (navigation.Up == true)
            {
                if (!string.IsNullOrWhiteSpace(identifier.Id))
                {
                    path = Path.GetDirectoryName(identifier.Id) ?? string.Empty;
                }
            }
            else if (!string.IsNullOrWhiteSpace(navigation.Id))
            {
                path = navigation.Id;
            }
            else if (!string.IsNullOrWhiteSpace(identifier.Id))
            {
                if (!string.IsNullOrWhiteSpace(navigation.Name))
                {
                    path = Path.Combine(identifier.Id, navigation.Name);
                }
                else
                {
                    path = identifier.Id;
                }
            }
            else if (!string.IsNullOrWhiteSpace(navigation.Id))
            {
                path = navigation.Id;
            }
            else if (!string.IsNullOrWhiteSpace(navigation.Name))
            {
                path = navigation.Name;
            }

            if (isValid)
            {
                identifier.Id = path;
                identifier.IsRootFolder = string.IsNullOrWhiteSpace(path);
            }
            else
            {
                errorMessage = "Invalid navigation object";
            }

            return isValid;
        }

        private DriveItem GetDriveItem(FileSystemInfo fsi)
        {
            var item = new DriveItem
            {
                Name = fsi.Name,
                CreationTime = fsi.CreationTime,
                LastAccessTime = fsi.LastAccessTime,
                LastWriteTime = fsi.LastWriteTime
            };

            return item;
        }
    }
}
