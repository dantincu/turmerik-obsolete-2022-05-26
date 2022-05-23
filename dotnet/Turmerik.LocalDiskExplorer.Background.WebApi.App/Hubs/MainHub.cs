using Microsoft.AspNetCore.SignalR;
using Turmerik.Core.Services;
using Turmerik.NetCore.Services;

namespace Turmerik.LocalDiskExplorer.Background.WebApi.App.Hubs
{
    public class MainHub : Hub<ILocalDiskExplorerBackgroundApiClient>
    {
        public const string SINGLE_BACKGROUND_UI_APP_GROUP = "SingleBackgroundUIAppGroup";

        private static readonly object syncRoot = new object();

        private static volatile int singleBackgroundUIAppConnected;
        private static string singleBackgroundUIAppConnectionId;

        public async override Task OnConnectedAsync()
        {
            await base.OnConnectedAsync();
            bool addToGroup = false;

            lock (syncRoot)
            {
                if (Interlocked.CompareExchange(ref singleBackgroundUIAppConnected, 1, 0) == 0)
                {
                    addToGroup = true;
                    singleBackgroundUIAppConnectionId = Context.ConnectionId;
                }
            }

            if (addToGroup)
            {
                await Groups.AddToGroupAsync(Context.ConnectionId, SINGLE_BACKGROUND_UI_APP_GROUP);
            }
        }

        public async override Task OnDisconnectedAsync(Exception? exception)
        {
            await base.OnDisconnectedAsync(exception);
            bool removeFromGroup = false;

            lock (syncRoot)
            {
                if ((singleBackgroundUIAppConnected == 1) && (
                    singleBackgroundUIAppConnectionId == Context.ConnectionId))
                {
                    singleBackgroundUIAppConnectionId = null;
                    removeFromGroup = true;

                    singleBackgroundUIAppConnected = 0;
                }
            }

            if (removeFromGroup)
            {
                await Groups.RemoveFromGroupAsync(Context.ConnectionId, SINGLE_BACKGROUND_UI_APP_GROUP);
            }
        }
    }
}
