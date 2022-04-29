using System;
using System.Threading.Tasks;
using Turmerik.AspNetCore.Services;
using Turmerik.AspNetCore.Settings;
using Turmerik.Core.Cloneable;

namespace Turmerik.OneDriveExplorer.Blazor.Server.App.Data
{
    public interface IDriveFolderService
    {
        Task<IDriveFolder> GetDriveFolderAsync(string id);
        Task<IDriveFolder> GetRootFolderAsync();
    }

    public class DriveFolderService : IDriveFolderService
    {
        private readonly ICloneableMapper mapper;

        public DriveFolderService(
            ICloneableMapper mapper)
        {
            this.mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public Task<IDriveFolder> GetDriveFolderAsync(string id)
        {
            throw new System.NotImplementedException();
        }

        public Task<IDriveFolder> GetRootFolderAsync()
        {
            throw new System.NotImplementedException();
        }
    }
}
