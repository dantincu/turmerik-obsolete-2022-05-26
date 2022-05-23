using Microsoft.AspNetCore.SignalR;
using Turmerik.AspNetCore.Services;
using Turmerik.Core.Services;
using Turmerik.NetCore.Services;

namespace Turmerik.LocalDiskExplorer.Background.WebApi.App.Hubs
{
    public class MainHub : Hub<ILocalDiskExplorerBackgroundApiClient>
    {
        private readonly ILocalDiskExplorerBackgroundApiClientReference localDiskExplorerBackgroundApiClientReference;

        public MainHub(ILocalDiskExplorerBackgroundApiClientReference localDiskExplorerBackgroundApiClientReference)
        {
            this.localDiskExplorerBackgroundApiClientReference = localDiskExplorerBackgroundApiClientReference ?? throw new ArgumentNullException(
                nameof(localDiskExplorerBackgroundApiClientReference));
        }

        public async override Task OnConnectedAsync()
        {
            await base.OnConnectedAsync();
            localDiskExplorerBackgroundApiClientReference.SetConnectionIdIfNoneRegistered(Context.ConnectionId);
        }

        public async override Task OnDisconnectedAsync(Exception? exception)
        {
            await base.OnDisconnectedAsync(exception);
            localDiskExplorerBackgroundApiClientReference.ClearConnectionIdIfRegistered(Context.ConnectionId);
        }
    }
}
