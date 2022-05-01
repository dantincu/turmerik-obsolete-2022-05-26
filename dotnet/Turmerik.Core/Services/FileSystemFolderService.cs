using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Turmerik.AspNetCore.Services.DriveItems;

namespace Turmerik.Core.Services
{
    public class FileSystemFolderService : IDriveFolderService
    {
        public Task<IDriveFolder> GetDriveFolderAsync(string address, string id, bool refreshCache = false)
        {
            throw new NotImplementedException();
        }

        public Task<IDriveFolder> GetRootFolderAsync(bool refreshCache = false)
        {
            throw new NotImplementedException();
        }

        public bool TryNormalizeAddress(ref string address, out string id)
        {
            id = null;
            throw new NotImplementedException();
        }
    }
}
