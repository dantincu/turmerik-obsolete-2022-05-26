using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Turmerik.AspNetCore.Services;
using Turmerik.Core.Services;
using Turmerik.LocalDiskExplorer.Background.WebApi.App.Hubs;
using Turmerik.NetCore.Services;

namespace Turmerik.LocalDiskExplorer.Background.WebApi.App.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class MainController : ControllerBase, ILocalDiskExplorerBackgroundApi
    {
        private readonly IHubContext<MainHub, ILocalDiskExplorerBackgroundApiClient> mainHubContext;

        public MainController(IHubContext<MainHub, ILocalDiskExplorerBackgroundApiClient> mainHubContext)
        {
            this.mainHubContext = mainHubContext ?? throw new ArgumentNullException(nameof(mainHubContext));
        }

        [HttpPost]
        public async Task<ApiResponse<object>> OpenFolderInOSFileExplorer([FromBody]FsEntryData fsEntryData)
        {
            await SingleBackgroundUIAppGroupClients.OpenFolderInOSFileExplorer(fsEntryData);

            var response = new ApiResponse<object>
            {
                Success = true
            };

            return response;
        }

        [HttpPost]
        public async Task<ApiResponse<object>> OpenFolderInTrmrkFileExplorer([FromBody] FsEntryData fsEntryData)
        {
            await SingleBackgroundUIAppGroupClients.OpenFolderInTrmrkFileExplorer(fsEntryData);

            var response = new ApiResponse<object>
            {
                Success = true
            };

            return response;
        }

        [HttpPost]
        public async Task<ApiResponse<object>> OpenFileInOSDefaultApp([FromBody] FsEntryData fsEntryData)
        {
            await SingleBackgroundUIAppGroupClients.OpenFileInOSDefaultApp(fsEntryData);

            var response = new ApiResponse<object>
            {
                Success = true
            };

            return response;
        }

        [HttpPost]
        public async Task<ApiResponse<object>> OpenFileInOSDefaultTextEditor([FromBody] FsEntryData fsEntryData)
        {
            await SingleBackgroundUIAppGroupClients.OpenFileInOSDefaultTextEditor(fsEntryData);

            var response = new ApiResponse<object>
            {
                Success = true
            };

            return response;
        }

        [HttpPost]
        public async Task<ApiResponse<object>> OpenFileInOSTrmrkTextEditor([FromBody] FsEntryData fsEntryData)
        {
            await SingleBackgroundUIAppGroupClients.OpenFileInOSTrmrkTextEditor(fsEntryData);

            var response = new ApiResponse<object>
            {
                Success = true
            };

            return response;
        }

        private ILocalDiskExplorerBackgroundApiClient SingleBackgroundUIAppGroupClients
        {
            get
            {
                return mainHubContext.Clients.Group(
                MainHub.SINGLE_BACKGROUND_UI_APP_GROUP);
            }
        }
    }
}
