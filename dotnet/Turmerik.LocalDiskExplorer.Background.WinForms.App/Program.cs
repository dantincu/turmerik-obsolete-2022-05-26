using Microsoft.Extensions.DependencyInjection;
using Turmerik.Core.Infrastucture;
using Turmerik.NetCore.Services;

namespace Turmerik.LocalDiskExplorer.Background.WinForms.App
{
    internal static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            var services = new ServiceCollection();
            TrmrkCoreServiceCollectionBuilder.RegisterAll(services);
            services.AddSingleton<ILocalDiskExplorerBackgroundApiClientMethodNameRetriever, LocalDiskExplorerBackgroundApiClientMethodNameRetriever>();

            ServiceProviderContainer.Instance.Value.RegisterServices(services);

            // To customize application configuration such as set high DPI settings or default font,
            // see https://aka.ms/applicationconfiguration.
            ApplicationConfiguration.Initialize();
            Application.Run(new MainForm());
        }
    }
}