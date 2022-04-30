using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Turmerik.AspNetCore.Services.DriveItems
{
    public interface IDriveFolderService
    {
        Task<IDriveFolder> GetDriveFolderAsync(string id);
        Task<IDriveFolder> GetRootFolderAsync();
    }

}
