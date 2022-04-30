using Turmerik.AspNetCore.Services.DriveItems;
using Turmerik.Core.Cloneable;

namespace Turmerik.AspNetCore.MsIdentity.Services.DriveItems
{
    public class OneDriveFolderService : IDriveFolderService
    {
        private readonly ICloneableMapper mapper;

        public OneDriveFolderService(
            ICloneableMapper mapper)
        {
            this.mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public Task<IDriveFolder> GetDriveFolderAsync(string id, bool refreshCache = false)
        {
            throw new NotImplementedException();
        }

        public Task<IDriveFolder> GetRootFolderAsync(bool refreshCache = false)
        {
            throw new NotImplementedException();
        }
    }
}
