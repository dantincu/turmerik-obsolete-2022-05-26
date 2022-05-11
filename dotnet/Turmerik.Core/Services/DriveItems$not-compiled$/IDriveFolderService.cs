using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Turmerik.Core.Data;

namespace Turmerik.Core.Services.DriveItems
{
    /// <summary>
    /// Service component used for retrieving drive items either from local device's file systen or from a cloud storage account
    /// (like OneDrive accounts) that the user authenticated with and confirmed his consent that this application makes queries on their behalf.
    /// </summary>
    public interface IDriveFolderService
    {
        /// <summary>
        /// Retrieves the information associated with the requested drive folder
        /// </summary>
        /// <param name="cacheKeyGuid">Value used to create the local data cache key to retrieve data from cache
        /// instead of querying from the source every time</param>
        /// <returns>Information associated with the requested drive folder</returns>
        Task<IDriveFolder> GetDriveFolderAsync(IDriveItemIdentifier identifier, Guid cacheKeyGuid, bool refreshCache);

        /// <summary>
        /// Retrieves the information associated with the requested drive folder
        /// </summary>
        /// <param name="cacheKeyGuid">Value used to create the local data cache key to retrieve data from cache
        /// instead of querying from the source every time</param>
        /// <returns>Information associated with the requested drive folder</returns>
        Task<IDriveFolder> GetRootDriveFolderAsync(Guid cacheKeyGuid, bool refreshCache);

        Task<ITabViewState> GetCurrentTabViewStateAsync(
            MutableValueWrapper<Guid?> currentlyOpenUuid,
            MutableValueWrapper<IDriveItemIdentifier> currentlyOpenIdnf,
            MutableValueWrapper<string> address,
            Guid cacheKeyGuid);

        bool IdentifiersAreEquivalent(IDriveItemIdentifier trgIdnf, IDriveItemIdentifier refIdnf);
        IDriveItemIdentifier GetDriveItemIdentifier(string address);
        string GetDriveItemAddress(IDriveItemIdentifier idnf);
        string GetDriveItemStrIndf(IDriveItemIdentifier idnf);
        DriveItemIdentifierType PreferredIdentifierType { get; }
    }

}
