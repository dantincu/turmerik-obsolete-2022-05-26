using Serilog.Events;
using System;
using System.Collections.Generic;
using System.Text;

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
