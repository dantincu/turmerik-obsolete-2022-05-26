using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Turmerik.Blazor.Server.Core.Services
{
    public interface IAppUserSessionsManager
    {
        Task RegisterLogin(byte[] usernameHash, byte[] authTokenHash);
    }

    public class AppUserSessionsManager : IAppUserSessionsManager, IDisposable
    {
        private readonly ITrmrkUserSessionsManager eventsAggregator;

        public AppUserSessionsManager(ITrmrkUserSessionsManager eventsAggregator)
        {
            this.eventsAggregator = eventsAggregator ?? throw new ArgumentNullException(nameof(eventsAggregator));
            eventsAggregator.OnRegisterLogin += RegisterLogin;
        }

        public void Dispose()
        {
            eventsAggregator.OnRegisterLogin -= RegisterLogin;
        }

        public async Task RegisterLogin(byte[] usernameHash, byte[] authTokenHash)
        {
            // throw new NotImplementedException();
        }
    }
}
