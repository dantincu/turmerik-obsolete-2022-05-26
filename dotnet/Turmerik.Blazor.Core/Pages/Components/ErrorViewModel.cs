using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Turmerik.Blazor.Core.Pages.Components
{
    public class ErrorViewModel
    {
        public ErrorViewModel(string message, Exception exception, bool printExcStackTrace = false)
        {
            Message = message;
            Exception = exception;
            PrintExcStackTrace = printExcStackTrace;
        }

        public string Message { get; }
        public Exception Exception { get; }
        public bool PrintExcStackTrace { get; }
    }

    public enum ErrorViewSize
    {
        Small = 0,
        Medium = 1,
        Large = 2
    }
}
