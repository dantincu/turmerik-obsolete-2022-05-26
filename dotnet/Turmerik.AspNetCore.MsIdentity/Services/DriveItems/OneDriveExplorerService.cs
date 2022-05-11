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

        protected override Task<DriveFolder> GetDriveFolderCoreAsync(string driveItemId)
        {
            throw new NotImplementedException();
        }

        protected override Task<DriveFolder> GetRootDriveFolderCoreAsync()
        {
            throw new NotImplementedException();
        }

        protected override bool TryNormalizeDriveFolderIdentifiersCore(DriveFolderIdentifier identifier, out string errorMessage)
        {
            throw new NotImplementedException();
        }

        protected override bool TryNormalizeDriveFolderNavigationCore(
            DriveFolderIdentifier identifier,
            DriveFolderNavigation navigation,
            DriveFolderIdentifier parentIdentifier,
            out string errorMessage)
        {
            throw new NotImplementedException();
        }
    }
}
