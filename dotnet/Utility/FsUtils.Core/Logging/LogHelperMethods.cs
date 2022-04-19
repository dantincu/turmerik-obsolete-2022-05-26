using FsUtils.Core.AppEnv;
using Microsoft.Extensions.DependencyInjection;
using Serilog.Events;
using System;
using System.Collections.Generic;
using System.Text;
using Turmerik.Core.Helpers;

namespace FsUtils.Core.Logging
{
    public static class LogHelperMethods
    {
        public static void CloseAndFlush()
        {
            Serilog.Log.CloseAndFlush();
        }

        public static LogEventLevel GetLogLevel(this LogLevel logLevel)
        {
            LogEventLevel retVal = (LogEventLevel)((int)logLevel);
            return retVal;
        }
    }
}
