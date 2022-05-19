using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Turmerik.LocalDiskExplorer.Background.WebApi.App.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MainController : ControllerBase
    {
        [HttpGet]
        public bool Ping()
        {
            return true;
        }
    }
}
