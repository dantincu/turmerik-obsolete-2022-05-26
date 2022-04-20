using FsUtils.Core.AppEnv;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;
using Turmerik.Core.Helpers;
using Turmerik.Core.Infrastucture;

namespace FsUtils.Core.Logging
{
    public interface ILoggerFactory
    {
        IAppLogger GetLogger(string loggerName);
        IAppLogger GetLogger(Type logDirNameType);
        IAppLogger<TData> GetLogger<TData>(string loggerName, TData data);
        IAppLogger<TData> GetLogger<TData>(Type logDirNameType, TData data);
    }

    public class LoggerFactory : ILoggerFactory
    {
        private readonly EnvDirHelper envDirHelper;
        private readonly AppStartInfo appStartInfo;

        public LoggerFactory(
            EnvDirHelper envDirHelper,
            AppStartInfo appStartInfo)
        {
            this.envDirHelper = envDirHelper ?? throw new ArgumentNullException(nameof(envDirHelper));
            this.appStartInfo = appStartInfo ?? throw new ArgumentNullException(nameof(appStartInfo));
        }

        public IAppLogger GetLogger(string loggerName)
        {
            string logBasePath = envDirHelper.EnvPath(EnvDir.Logs);

            var logger = new AppLogger(
                logBasePath,
                appStartInfo,
                loggerName);

            return logger;
        }

        public IAppLogger GetLogger(Type logDirNameType)
        {
            var logger = GetLogger(
                logDirNameType.GetTypeFullDisplayName());

            return logger;
        }

        public IAppLogger<TData> GetLogger<TData>(string loggerName, TData data)
        {
            string logBasePath = envDirHelper.EnvPath(EnvDir.Logs);

            var logger = new AppLogger<TData>(
                logBasePath,
                appStartInfo,
                loggerName,
                data);

            return logger;
        }

        public IAppLogger<TData> GetLogger<TData>(Type logDirNameType, TData data)
        {
            var logger = GetLogger(
                logDirNameType.GetTypeFullDisplayName(),
                data);

            return logger;
        }
    }
}
