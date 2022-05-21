using Turmerik.AspNetCore.Services.DriveItems;
using Turmerik.AspNetCore.Services.LocalSessionStorage;
using Turmerik.Core.Cloneable;
using Turmerik.Core.FileSystem;
using Turmerik.Core.Services.DriveItems;

namespace Turmerik.AspNetCore.MsIdentity.Services.DriveItems
{
    public class OneDriveExplorerService : DriveExplorerServiceBase
    {
        public OneDriveExplorerService(ISessionStorageWrapper sessionStorageWrapper) : base(sessionStorageWrapper)
        {
        }

        public override string GetDriveItemId(DriveItemIdentifier identifier)
        {
            throw new NotImplementedException();
        }

        public override string GetDriveItemAddress(DriveItemIdentifier identifier)
        {
            throw new NotImplementedException();
        }

        public override string GetDriveItemPath(DriveItemIdentifier identifier)
        {
            throw new NotImplementedException();
        }

        public override string GetDriveItemUri(DriveItemIdentifier identifier)
        {
            throw new NotImplementedException();
        }

        protected override Task CreateNewFolderCoreAsync(string parentFolderId, string newFolderName)
        {
            throw new NotImplementedException();
        }

        protected override Task CreateNewMsOfficeFileCoreAsync(string parentFolderId, string newMsOfficeFileName)
        {
            throw new NotImplementedException();
        }

        protected override Task CreateNewTextFileCoreAsync(string parentFolderId, string newTextFileName, string text = null)
        {
            throw new NotImplementedException();
        }

        protected override Task DeleteFileCoreAsync(string fileId)
        {
            throw new NotImplementedException();
        }

        protected override Task DeleteFolderCoreAsync(string folderId, bool recursive)
        {
            throw new NotImplementedException();
        }

        protected override Task<DriveFolder> GetDriveFolderCoreAsync(string driveItemId)
        {
            throw new NotImplementedException();
        }

        protected override Task<DriveFolder> GetRootDriveFolderCoreAsync()
        {
            throw new NotImplementedException();
        }

        protected override Task MoveFileCoreAsync(string fileId, string newParentFolderId, string newFolderName)
        {
            throw new NotImplementedException();
        }

        protected override Task MoveFolderCoreAsync(string folderId, string newParentFolderId, string newFileName)
        {
            throw new NotImplementedException();
        }

        protected override Task RenameFileCoreAsync(string fileId, string newFileName)
        {
            throw new NotImplementedException();
        }

        protected override Task RenameFolderCoreAsync(string folderId, string newFolderName)
        {
            throw new NotImplementedException();
        }

        protected override Task CopyFolderCoreAsync(string folderId, string newParentFolderId, string newFolderName)
        {
            throw new NotImplementedException();
        }

        protected override Task CopyFileCoreAsync(string fileId, string newParentFolderId, string newFileName)
        {
            throw new NotImplementedException();
        }

        protected override bool TryNormalizeDriveFolderIdentifiersCore(
            ref DriveItemIdentifier identifier,
            out string errorMessage)
        {
            throw new NotImplementedException();
        }

        protected override bool TryNormalizeDriveFolderNavigationCore(
            ref DriveItemIdentifier identifier,
            DriveFolderNavigation navigation,
            out string errorMessage)
        {
            throw new NotImplementedException();
        }

        protected override void AssignDriveFolderIdentifier(DriveExplorerServiceArgs args, TabPageHistory history)
        {
            throw new NotImplementedException();
        }
    }
}
