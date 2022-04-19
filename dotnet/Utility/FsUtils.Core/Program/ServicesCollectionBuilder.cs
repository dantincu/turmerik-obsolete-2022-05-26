using FsUtils.Core.AppEnv;
using FsUtils.Core.Logging;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;
using ServiceCollectionBuilderCore = Turmerik.Core.Infrastucture.ServiceCollectionBuilder;

namespace FsUtils.Core.Program
{
    internal class ServicesCollectionBuilder
    {
        public static void RegisterAllServices(IServiceCollection services)
        {
            ServiceCollectionBuilderCore.RegisterAllServices(services);

            services.AddSingleton<IAppStartInfo, AppStartInfo>();
            services.AddSingleton<IAppEnvDir, AppEnvDir>();
            services.AddSingleton<IEnvDirHelper, EnvDirHelper>();
            services.AddSingleton<ILoggerFactory, LoggerFactory>();
        }
    }
}
