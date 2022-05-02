using Turmerik.AspNetCore.Services.DriveItems;
using Turmerik.AspNetCore.Services.LocalSessionStorage;
using Turmerik.Core.Cloneable;
using Turmerik.Core.Services.DriveItems;

namespace Turmerik.AspNetCore.MsIdentity.Services.DriveItems
{
    public class OneDriveFolderService : DriveFolderServiceBase, IDriveFolderService
    {
        public OneDriveFolderService(
            ICloneableMapper mapper,
            ISessionStorageWrapper localStorageWrapper) : base(mapper)
        {
            Storage = localStorageWrapper;
        }

        protected override IWebStorageWrapper Storage { get; }

        public override bool TryNormalizeAddress(ref string path, out string id)
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
            throw new NotImplementedException();
        }
    }
}
