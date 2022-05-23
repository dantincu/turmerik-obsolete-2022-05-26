using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Turmerik.AspNetCore.Services;
using Turmerik.Core.Services;
using Turmerik.LocalDiskExplorer.Background.WebApi.App.Hubs;
using Turmerik.NetCore.Services;
using Turmerik.Core.Helpers;

namespace Turmerik.LocalDiskExplorer.Background.WebApi.App.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class MainController : ControllerBase, ILocalDiskExplorerBackgroundApi
    {
        private readonly IHubContext<MainHub, ILocalDiskExplorerBackgroundApiClient> mainHubContext;
        private readonly ILocalDiskExplorerBackgroundApiClientReference localDiskExplorerBackgroundApiClientReference;

        public MainController(
            IHubContext<MainHub, ILocalDiskExplorerBackgroundApiClient> mainHubContext,
            ILocalDiskExplorerBackgroundApiClientReference localDiskExplorerBackgroundApiClientReference)
        {
            this.mainHubContext = mainHubContext ?? throw new ArgumentNullException(nameof(mainHubContext));

            this.localDiskExplorerBackgroundApiClientReference = localDiskExplorerBackgroundApiClientReference ?? throw new ArgumentNullException(
                nameof(localDiskExplorerBackgroundApiClientReference));
        }

        private ILocalDiskExplorerBackgroundApiClient SingleClient
        {
            get
            {
                var singleClient = localDiskExplorerBackgroundApiClientReference.SingleClient(
                    mainHubContext.Clients);

                return singleClient;
            }
        }

        [HttpPost]
        public async Task<ApiResponse<object>> OpenFolderInOSFileExplorer([FromBody]FsEntryData fsEntryData)
        {
            var singleClient = SingleClient;

            if (singleClient != null)
            {
                await singleClient.OpenFolderInOSFileExplorer(fsEntryData);
            }

            var response = GetApiResponse();
            return response;
        }

        [HttpPost]
        public async Task<ApiResponse<object>> OpenFolderInTrmrkFileExplorer([FromBody] FsEntryData fsEntryData)
        {
            var singleClient = SingleClient;

            if (singleClient != null)
            {
                await singleClient.OpenFolderInOSFileExplorer(fsEntryData);
            }

            var response = GetApiResponse();
            return response;
        }

        [HttpPost]
        public async Task<ApiResponse<object>> OpenFileInOSDefaultApp([FromBody] FsEntryData fsEntryData)
        {
            var singleClient = SingleClient;

            if (singleClient != null)
            {
                await singleClient.OpenFolderInOSFileExplorer(fsEntryData);
            }

            var response = GetApiResponse();
            return response;
        }

        [HttpPost]
        public async Task<ApiResponse<object>> OpenFileInOSDefaultTextEditor([FromBody] FsEntryData fsEntryData)
        {
            var singleClient = SingleClient;

            if (singleClient != null)
            {
                await singleClient.OpenFolderInOSFileExplorer(fsEntryData);
            }

            var response = GetApiResponse();
            return response;
        }

        [HttpPost]
        public async Task<ApiResponse<object>> OpenFileInOSTrmrkTextEditor([FromBody] FsEntryData fsEntryData)
        {
            var singleClient = SingleClient;

            if (singleClient != null)
            {
                await singleClient.OpenFolderInOSFileExplorer(fsEntryData);
            }

            var response = GetApiResponse();
            return response;
        }

        private ApiResponse<object> GetApiResponse()
        {
            var response = new ApiResponse<object>
            {
                Success = true
            };

            return response;
        }
    }
}
