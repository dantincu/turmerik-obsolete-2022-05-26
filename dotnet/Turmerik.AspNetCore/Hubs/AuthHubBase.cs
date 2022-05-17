using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Turmerik.AspNetCore.Hubs
{
    public abstract class AuthHubBase : HubCoreBase
    {
        protected override async Task SendToCurrentUserAsync(
            string method,
            CancellationToken cancellationToken,
            params object[] args)
        {
            await Clients.User(
                Context.UserIdentifier).SendCoreAsync(
                method, args, cancellationToken);
        }

        protected override async Task SendToCurrentUserAsync(
            string method,
            params object[] args)
        {
            await Clients.User(
                Context.UserIdentifier).SendCoreAsync(
                method, args, default(CancellationToken));
        }
    }
}
