using Turmerik.AspNetCore.Services.DriveItems;
using Turmerik.AspNetCore.Services.LocalSessionStorage;
using Turmerik.Core.Cloneable;
using Turmerik.Core.FileSystem;
using Turmerik.Core.Services.DriveItems;

namespace Turmerik.AspNetCore.MsIdentity.Services.DriveItems
{
    public class OneDriveFolderService : DriveFolderServiceBase, IDriveFolderService
    {
        public OneDriveFolderService(
            ICloneableMapper clblMapper,
            IFsPathNormalizer fsPathNormalizer,
            ISessionStorageWrapper sessionStorageWrapper) : base(
                clblMapper,
                fsPathNormalizer)
        {
            Storage = sessionStorageWrapper;
        }

        public override DriveItemIdentifierType PreferredIdentifierType => DriveItemIdentifierType.Id | DriveItemIdentifierType.Uri;
        protected override IWebStorageWrapper Storage { get; }

        public override bool TryNormalizeAddress(ref string path, out string id)
        {
            throw new NotImplementedException();
        }

        public override bool DriveItemsHaveSameIdentifiers(IDriveItemCore trgItem, IDriveItemCore refItem, bool normalizeFirst)
        {
            throw new NotImplementedException();
        }

        public override string GetDriveItemIdentifier(IDriveItemCore item) => item.Uri;

        protected override async Task<IDriveFolder> GetDriveFolderCoreAsync(string idnf)
        {
            throw new NotImplementedException();
        }

        protected override async Task<IDriveFolder> GetRootDriveFolderCoreAsync()
        {
            throw new NotImplementedException();
        }

        public override bool IdentifiersAreEquivalent(string trgIdnf, string refIdnf, bool normalizeFirst)
        {
            throw new NotImplementedException();
        }
    }
}
