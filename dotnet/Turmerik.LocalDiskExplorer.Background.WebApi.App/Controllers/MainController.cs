using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Turmerik.AspNetCore.Services;

namespace Turmerik.LocalDiskExplorer.Background.WebApi.App.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class MainController : ControllerBase, ILocalDiskExplorerBackgroundApiMainController
    {
        [HttpPost]
        public ApiResponse<object> OpenFolderInOSFileExplorer([FromBody]FsEntryData fsEntryData)
        {
            return new ApiResponse<object>
            {
                Success = true
            };
        }

        [HttpPost]
        public ApiResponse<object> OpenFolderInTrmrkFileExplorer([FromBody] FsEntryData fsEntryData)
        {
            return new ApiResponse<object>
            {
                Success = true
            };
        }

        [HttpPost]
        public ApiResponse<object> OpenFileInOSDefaultApp([FromBody] FsEntryData fsEntryData)
        {
            return new ApiResponse<object>
            {
                Success = true
            };
        }

        [HttpPost]
        public ApiResponse<object> OpenFileInOSDefaultTextEditor([FromBody] FsEntryData fsEntryData)
        {
            return new ApiResponse<object>
            {
                Success = true
            };
        }

        [HttpPost]
        public ApiResponse<object> OpenFileInOSTrmrkTextEditor([FromBody] FsEntryData fsEntryData)
        {
            return new ApiResponse<object>
            {
                Success = true
            };
        }
    }
}
