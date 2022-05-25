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

        public override string GetDriveItemId(DriveItemIdentifier identifier)
        {
            string address = GetDriveItemPath(identifier);
            return address;
        }

        public override string GetDriveItemAddress(DriveItemIdentifier identifier)
        {
            string address = GetDriveItemPath(identifier);
            return address;
        }

        public override string GetDriveItemPath(DriveItemIdentifier identifier)
        {
            string path = identifier.Id;

            if (string.IsNullOrWhiteSpace(path))
            {
                path = identifier.ParentId;

                if (!string.IsNullOrWhiteSpace(identifier.Name))
                {
                    if (string.IsNullOrWhiteSpace(path))
                    {
                        path = identifier.Name;
                    }
                    else
                    {
                        path = Path.Combine(path, identifier.Name);
                    }
                }
            }

            return path;
        }

        public override string GetDriveItemUri(DriveItemIdentifier identifier)
        {
            string path = GetDriveItemPath(identifier);
            string uri = $"{FsH.FILE_URI_SCHEME}{path}";

            return uri;
        }

        protected override void AssignDriveFolderIdentifier(
           DriveExplorerServiceArgs args,
           TabPageHistory history)
        {
            string path = history.InitialId;
            string name = path;

            if (history.BackHistory.Any())
            {
                bool @break = false;

                var historyParts = history.BackHistory.Reverse<DriveFolderNavigation>(
                ).TakeWhile(h =>
                {
                    bool retVal = !@break;
                    @break = !string.IsNullOrWhiteSpace(h.Id);

                    return retVal;
                }).Reverse().ToList();

                var pathParts = historyParts.Select(
                    (h, i) =>
                    {
                        string part;

                        if (i == 0 && !string.IsNullOrWhiteSpace(h.Id))
                        {
                            part = h.Id;
                        }
                        else if (h.Up == true)
                        {
                            part = "..";
                        }
                        else
                        {
                            part = h.Name;
                        }

                        return part;
                    }).ToList();

                if (!@break && !string.IsNullOrWhiteSpace(history.InitialId))
                {
                    pathParts.Insert(0, history.InitialId);
                }

                path = Path.Combine(pathParts.ToArray());
                name = pathParts.Last();
            }

            var folderIdentifier = new DriveItemIdentifier
            {
                Id = path,
                IsRootFolder = string.IsNullOrWhiteSpace(path),
                Name = name
            };

            TryNormalizeDriveFolderIdentifier(ref folderIdentifier);
            args.FolderIdentifier = folderIdentifier;
        }

        protected override async Task CreateNewFolderCoreAsync(string parentFolderId, string newFolderName)
        {
            string path = Path.Combine(parentFolderId, newFolderName);
            Directory.CreateDirectory(path);
        }

        protected override async Task CreateNewMsOfficeFileCoreAsync(string parentFolderId, string newMsOfficeFileName)
        {
            throw new NotImplementedException();
        }

        protected override async Task CreateNewTextFileCoreAsync(string parentFolderId, string newTextFileName, string text = null)
        {
            string path = Path.Combine(parentFolderId, newTextFileName);

            using (var sw = new StreamWriter(
                path,
                new FileStreamOptions
                {
                    Mode = FileMode.CreateNew,
                }))
            {
                if (!string.IsNullOrEmpty(text))
                {
                    sw.Write(text);
                }
            }
        }

        protected override async Task DeleteFileCoreAsync(string fileId)
        {
            File.Delete(fileId);
        }

        protected override async Task DeleteFolderCoreAsync(string folderId, bool recursive)
        {
            Directory.Delete(folderId, recursive);
        }

        protected override async Task MoveFileCoreAsync(string fileId, string newParentFolderId, string newFileName)
        {
            string newFilePath = Path.Combine(newParentFolderId, newFileName);
            File.Move(fileId, newFilePath);
        }

        protected override async Task MoveFolderCoreAsync(string folderId, string newParentFolderId, string newFolderName)
        {
            string newFolderPath = Path.Combine(newParentFolderId, newFolderName);
            Directory.Move(folderId, newFolderPath);
        }

        protected override async Task RenameFileCoreAsync(string fileId, string newFileName)
        {
            string path = Path.GetDirectoryName(fileId);
            path = Path.Combine(path, newFileName);

            File.Move(fileId, path);
        }

        protected override async Task CopyFolderCoreAsync(string folderId, string newParentFolderId, string newFolderName)
        {
            throw new NotImplementedException();
        }

        protected override async Task CopyFileCoreAsync(string fileId, string newParentFolderId, string newFileName)
        {
            string newFilePath = Path.Combine(newParentFolderId, newFileName);
            File.Copy(fileId, newFilePath);
        }

        protected override async Task RenameFolderCoreAsync(string folderId, string newFolderName)
        {
            string path = Path.GetDirectoryName(folderId);
            path = Path.Combine(path, newFolderName);

            Directory.Move(folderId, path);
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

            driveFolder.FolderItems.Sort((a, b) => a.Name.CompareTo(b.Name));
            driveFolder.FileItems.Sort((a, b) => a.Name.CompareTo(b.Name));

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
            ref DriveItemIdentifier identifier,
            out string errorMessage)
        {
            bool isValid = true;
            errorMessage = null;

            identifier = identifier ?? new DriveItemIdentifier();
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

            if (string.IsNullOrWhiteSpace(
                path) && (!string.IsNullOrEmpty(
                    identifier.ParentId) && !string.IsNullOrEmpty(identifier.Name)))
            {
                path = Path.Combine(identifier.ParentId, identifier.Name);
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
                    identifier.Name = Path.GetFileName(path);

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
            ref DriveItemIdentifier identifier,
            DriveFolderNavigation navigation,
            out string errorMessage)
        {
            errorMessage = null;
            bool isValid = true;

            string path = string.Empty;
            identifier = identifier ?? new DriveItemIdentifier();

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
