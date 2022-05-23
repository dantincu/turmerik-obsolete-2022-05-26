using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Turmerik.Core.Threading;
using Turmerik.NetCore.Services;

namespace Turmerik.AspNetCore.Services
{
    public interface ILocalDiskExplorerBackgroundApiClientReference
    {
        string GetConnectionId();
        void ClearConnectionIdIfRegistered(string connectionId);
        void SetConnectionIdIfNoneRegistered(string connectionId);

        ILocalDiskExplorerBackgroundApiClient SingleClient(
            IHubClients<ILocalDiskExplorerBackgroundApiClient> hubClients);
    }

    public class LocalDiskExplorerBackgroundApiClientReference : ILocalDiskExplorerBackgroundApiClientReference
    {
        private static readonly object syncRoot = new object();

        private string connectionId;
        private volatile int hasConnectionId;

        public LocalDiskExplorerBackgroundApiClientReference()
        {
        }

        public string GetConnectionId()
        {
            string connectionId = null;

            lock (syncRoot)
            {
                if (hasConnectionId == 1)
                {
                    connectionId = this.connectionId;
                }
            }

            return connectionId;
        }

        public ILocalDiskExplorerBackgroundApiClient SingleClient(IHubClients<ILocalDiskExplorerBackgroundApiClient> hubClients)
        {
            string connectionId = null;
            ILocalDiskExplorerBackgroundApiClient singleClient = null;

            lock (syncRoot)
            {
                if (hasConnectionId == 1 && this.connectionId != null)
                {
                    connectionId = this.connectionId;
                }
            }

            if (connectionId != null)
            {
                singleClient = hubClients.Client(connectionId);
            }

            return singleClient;
        }

        public void ClearConnectionIdIfRegistered(string connectionId)
        {
            lock (syncRoot)
            {
                if (hasConnectionId == 1 && this.connectionId == connectionId)
                {
                    this.connectionId = null;
                }
            }
        }

        public void SetConnectionIdIfNoneRegistered(string connectionId)
        {
            lock (syncRoot)
            {
                if (Interlocked.CompareExchange(ref hasConnectionId, 1, 0) == 0)
                {
                    this.connectionId = connectionId;
                }
            }
        }
    }
}
