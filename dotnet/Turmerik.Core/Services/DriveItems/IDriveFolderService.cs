using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Turmerik.AspNetCore.Services.DriveItems
{
    public interface IDriveFolderService
    {
        Task<IDriveFolder> GetDriveFolderAsync(string id, bool refreshCache = false);
        Task<IDriveFolder> GetRootFolderAsync(bool refreshCache = false);
    }

}
