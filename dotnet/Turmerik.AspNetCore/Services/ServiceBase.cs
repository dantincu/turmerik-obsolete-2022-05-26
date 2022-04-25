using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

namespace Turmerik.AspNetCore.Services
{
    /// <summary>
    /// How to set up logging in MS Azure:
    /// <a href="https://stackoverflow.com/questions/49111671/where-does-the-asp-net-core-logging-api-as-default-store-logs" />
    /// </summary>
    public abstract class ServiceBase
    {
        protected readonly ILogger<ApplicationLog> Logger;

        protected ServiceBase(ILogger<ApplicationLog> logger)
        {
            Logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }
    }
}
