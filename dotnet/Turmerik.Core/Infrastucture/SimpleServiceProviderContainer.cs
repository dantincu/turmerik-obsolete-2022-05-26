using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;
using Turmerik.Core.Threading;

namespace Turmerik.Core.Infrastucture
{
    /// <summary>
    /// To be used ONLY where the Host Builder patter is not applicable (like in unit tests)
    /// </summary>
    public class SimpleServiceProviderContainer
    {
        private readonly SynchronizerComponent synchronizer;
        private IServiceProvider serviceProvider;

        protected SimpleServiceProviderContainer()
        {
            synchronizer = new SynchronizerComponent();
        }

        public void RegisterServices(ServiceCollection services)
        {
            synchronizer.Invoke(
                () => serviceProvider = services.BuildServiceProvider(),
                () => this.serviceProvider == null,
                () => throw new InvalidOperationException(
                    "The service collection can only be registered once"));
        }

        public IServiceProvider Services
        {
            get
            {
                var services = synchronizer.Invoke(
                    () => this.serviceProvider,
                    () => this.serviceProvider != null,
                    () => throw new InvalidOperationException(
                        "The service provider is available only after the service collection has been registered"));

                return services;
            }
        }
    }
}
