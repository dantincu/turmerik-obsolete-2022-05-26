using FsUtils.Core.AppEnv;
using Turmerik.Core.Helpers;
using Serilog;
using Serilog.Configuration;
using Serilog.Core;
using Serilog.Formatting;
using Serilog.Formatting.Json;
using Serilog.Sinks.File;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.IO;

namespace FsUtils.Core.Logging
{
    public class AppLogger : IAppLogger
    {
        protected const int FILE_SIZE_LIMIT_BYTES = 1024 * 1024;
        protected const string LOG_FILE_NAME = "log-.json";

        protected readonly string LogBasePath;
        protected readonly AppStartInfo AppStartInfo;
        protected readonly string LoggerName;
        protected readonly ILogger logger;
        
        public AppLogger(
            string logBasePath,
            AppStartInfo appStartInfo,
            string loggerName)
        {
            LogBasePath = logBasePath;
            AppStartInfo = appStartInfo;
            LoggerName = loggerName;
            logger = GetLogger();
        }

        protected virtual bool IsLoggerBuffered => false;
        protected virtual bool IsLoggerShared => true;
        protected virtual TimeSpan? FlushToDiskInterval => null;

        public IAppLogger<TData> WithData<TData>(TData data)
        {
            IAppLogger<TData> appLogger = new AppLogger<TData>(LogBasePath, AppStartInfo, LoggerName, data);
            return appLogger;
        }

        public void Write(LogLevel logLevel, string messageTemplate, params object[] propertyValues)
        {
            this.logger.Write(logLevel.GetLogLevel(), messageTemplate, propertyValues);
        }

        public void Write(LogLevel logLevel, Exception ex, string messageTemplate, params object[] propertyValues)
        {
            this.logger.Write(logLevel.GetLogLevel(), ex, messageTemplate, propertyValues);
        }

        public void Verbose(string messageTemplate, params object[] propertyValues)
        {
            this.logger.Verbose(messageTemplate, propertyValues);
        }

        public void Verbose(Exception exception, string messageTemplate, params object[] propertyValues)
        {
            this.logger.Verbose(exception, messageTemplate, propertyValues);
        }

        public void Debug(string messageTemplate, params object[] propertyValues)
        {
            this.logger.Debug(messageTemplate, propertyValues);
        }

        public void Debug(Exception exception, string messageTemplate, params object[] propertyValues)
        {
            this.logger.Debug(exception, messageTemplate, propertyValues);
        }

        public void Information(string messageTemplate, params object[] propertyValues)
        {
            this.logger.Information(messageTemplate, propertyValues);
        }

        public void Information(Exception exception, string messageTemplate, params object[] propertyValues)
        {
            this.logger.Information(exception, messageTemplate, propertyValues);
        }

        public void Warning(string messageTemplate, params object[] propertyValues)
        {
            this.logger.Warning(messageTemplate, propertyValues);
        }

        public void Warning(Exception exception, string messageTemplate, params object[] propertyValues)
        {
            this.logger.Warning(exception, messageTemplate, propertyValues);
        }

        public void Error(string messageTemplate, params object[] propertyValues)
        {
            this.logger.Error(messageTemplate, propertyValues);
        }

        public void Error(Exception exception, string messageTemplate, params object[] propertyValues)
        {
            this.logger.Error(exception, messageTemplate, propertyValues);
        }

        public void Fatal(string messageTemplate, params object[] propertyValues)
        {
            this.logger.Fatal(messageTemplate, propertyValues);
        }

        public void Fatal(Exception exception, string messageTemplate, params object[] propertyValues)
        {
            this.logger.Fatal(exception, messageTemplate, propertyValues);
        }

        public void Dispose()
        {
            IDisposable disposable = this.logger as IDisposable;
            disposable?.Dispose();
        }

        protected virtual string GetLogFileName()
        {
            return LOG_FILE_NAME;
        }

        protected virtual string GetLogDirName()
        {
            return LoggerName;
        }

        protected virtual string GetAppExecutionDirName()
        {
            var appStartInfo = AppStartInfo;

            long ticks = appStartInfo.AppStartTicks;
            Guid guid = appStartInfo.AppStartGuid;

            string appExecutionDirName = $"[{ticks}]-[{guid}]";
            return appExecutionDirName;
        }

        protected virtual int GetFileSizeLimitBytes()
        {
            return FILE_SIZE_LIMIT_BYTES;
        }

        protected virtual LoggerConfiguration Enrich(LoggerConfiguration loggerConfiguration)
        {
            loggerConfiguration = loggerConfiguration.Enrich.WithProperty(
                nameof(AppStartInfo.Instance.Value.AppStartGuid),
                AppStartInfo.Instance.Value.AppStartGuid).Enrich.WithProperty(
                nameof(AppStartInfo.Instance.Value.AppStartTicks),
                AppStartInfo.Instance.Value.AppStartTicks);

            return loggerConfiguration;
        }

        protected virtual FileLifecycleHooks GetFileLifecycleHooks()
        {
            return null;
        }

        protected virtual ILogger GetLogger()
        {
            ILogger logger = GetLoggerConfiguration().CreateLogger();
            return logger;
        }

        private string GetLogFilePath()
        {
            string appExecutionDirName = GetAppExecutionDirName();
            string logDirName = GetLogDirName();
            string logFileName = GetLogFileName();

            string logFilePath = Path.Combine(
                LogBasePath,
                appExecutionDirName,
                logDirName,
                logFileName);

            return logFilePath;
        }

        private LoggerConfiguration GetLoggerConfiguration()
        {
            // LoggerConfiguration loggerConfiguration = Enrich(new LoggerConfiguration());
            LoggerConfiguration loggerConfiguration = new LoggerConfiguration();
            LoggerSinkConfiguration loggerSinkConfiguration = GetLoggerSinkConfiguration(loggerConfiguration);

            loggerConfiguration = GetFileLoggerConfiguration(loggerSinkConfiguration);
            return loggerConfiguration;
        }

        private LoggerSinkConfiguration GetLoggerSinkConfiguration(LoggerConfiguration loggerConfiguration)
        {
            LoggerSinkConfiguration loggerSinkConfiguration = loggerConfiguration.MinimumLevel.Verbose().WriteTo;
            return loggerSinkConfiguration;
        }

        private LoggerConfiguration GetFileLoggerConfiguration(LoggerSinkConfiguration loggerSinkConfiguration)
        {
            LoggerConfiguration loggerConfiguration = loggerSinkConfiguration.File(
                GetTextFormatter(),
                GetLogFilePath(),
                restrictedToMinimumLevel: Serilog.Events.LogEventLevel.Verbose,
                fileSizeLimitBytes: GetFileSizeLimitBytes(),
                levelSwitch: new LoggingLevelSwitch(Serilog.Events.LogEventLevel.Verbose),
                buffered: IsLoggerBuffered,
                shared: IsLoggerShared,
                flushToDiskInterval: FlushToDiskInterval,
                rollingInterval: RollingInterval.Day,
                rollOnFileSizeLimit: true,
                retainedFileCountLimit: null,
                encoding: null,
                hooks: GetFileLifecycleHooks());

            return loggerConfiguration;
        }

        private ITextFormatter GetTextFormatter()
        {
            return GetJsonFormatter();
        }

        private ITextFormatter GetJsonFormatter()
        {
            ITextFormatter formatter = new JsonFormatter(
                closingDelimiter: $",{Environment.NewLine}",
                renderMessage: true,
                formatProvider: CultureInfo.InvariantCulture);
            return formatter;
        }
    }

    public class AppLogger<TData> : AppLogger, IAppLogger<TData>
    {
        protected const string MESSAGE_DATA_PROPERTY_NAME = "msgData";

        protected readonly TData Data;

        public AppLogger(string logBasePath, AppStartInfo appStartInfo, string loggerName, TData data) : base(logBasePath, appStartInfo, loggerName)
        {
            Data = data;
        }

        protected override ILogger GetLogger()
        {
            ILogger logger = base.GetLogger().ForContext(
                MESSAGE_DATA_PROPERTY_NAME, this.Data, true);

            return logger;
        }
    }
}