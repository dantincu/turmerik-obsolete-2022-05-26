using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Turmerik.AspNetCore.Services;

namespace Turmerik.LocalDiskExplorer.Background.WebApi.App.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MainController : ControllerBase, ILocalDiskExplorerBackgroundApiMainController
    {
        [HttpPost]
        public bool OpenFolderInOSFileExplorer([FromBody]FsEntryData fsEntryData)
        {
            return true;
        }

        [HttpPost]
        public bool OpenFolderInTrmrkFileExplorer([FromBody] FsEntryData fsEntryData)
        {
            return true;
        }

        [HttpPost]
        public bool OpenFileInOSDefaultApp([FromBody] FsEntryData fsEntryData)
        {
            return true;
        }

        [HttpPost]
        public bool OpenFileInOSDefaultTextEditor([FromBody] FsEntryData fsEntryData)
        {
            return true;
        }

        [HttpPost]
        public bool OpenFileInOSTrmrkTextEditor([FromBody] FsEntryData fsEntryData)
        {
            return true;
        }
    }
}
