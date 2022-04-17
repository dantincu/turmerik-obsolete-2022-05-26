﻿using System;
using System.Collections.Generic;
using System.Text;

namespace FsUtils.Core.Logging
{
    public interface IAppLogger : IDisposable
    {
        void Write(LogLevel logLevel, string messageTemplate, params object[] propertyValues);
        void Write(LogLevel logLevel, Exception ex, string messageTemplate, params object[] propertyValues);
        void Verbose(string messageTemplate, params object[] propertyValues);
        void Verbose(Exception exception, string messageTemplate, params object[] propertyValues);
        void Debug(string messageTemplate, params object[] propertyValues);
        void Debug(Exception exception, string messageTemplate, params object[] propertyValues);
        void Information(string messageTemplate, params object[] propertyValues);
        void Information(Exception exception, string messageTemplate, params object[] propertyValues);
        void Warning(string messageTemplate, params object[] propertyValues);
        void Warning(Exception exception, string messageTemplate, params object[] propertyValues);
        void Error(string messageTemplate, params object[] propertyValues);
        void Error(Exception exception, string messageTemplate, params object[] propertyValues);
        void Fatal(string messageTemplate, params object[] propertyValues);
        void Fatal(Exception exception, string messageTemplate, params object[] propertyValues);
        IAppLogger<TData> WithData<TData>(TData data);
    }

    public interface IAppLogger<TData> : IAppLogger
    {
    }
}
