using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Turmerik.AspNetCore.Hubs
{
    public abstract class HubCoreBase : Hub
    {
        protected abstract Task SendToCurrentUserAsync(
            string method,
            CancellationToken cancellationToken,
            params object[] args);

        protected abstract Task SendToCurrentUserAsync(
            string method,
            params object[] args);
    }
}
