using System;
using System.Collections.Generic;
using System.Text;

namespace Turmerik.Core.Components
{
    public class InternalAppError : Exception
    {
    }

    public class ErrorViewModel
    {
        public ErrorViewModel(
            string message,
            Exception exception,
            bool printExcStackTrace = false)
        {
            Message = message;
            Exception = exception;
            PrintExcStackTrace = printExcStackTrace;
        }

        public string Message { get; }
        public Exception Exception { get; }
        public bool PrintExcStackTrace { get; }
    }
}
