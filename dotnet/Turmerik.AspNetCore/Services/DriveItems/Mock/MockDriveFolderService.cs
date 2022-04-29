using System;
using System.Threading.Tasks;
using Turmerik.AspNetCore.Services.DriveItems;
using Turmerik.Core.Cloneable;

namespace Turmerik.AspNetCore.Services.DriveItems.Mock
{
    public class MockDriveFolderService : IDriveFolderService
    {
        private readonly ICloneableMapper mapper;

        public MockDriveFolderService(
            ICloneableMapper mapper)
        {
            this.mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public Task<IDriveFolder> GetDriveFolderAsync(string id)
        {
            throw new NotImplementedException();
        }

        public Task<IDriveFolder> GetRootFolderAsync()
        {
            throw new NotImplementedException();
        }
    }
}
