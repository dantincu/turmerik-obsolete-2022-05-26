using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Turmerik.AspNetCore.Services.Api
{
    public class ApiResponse<TData>
    {
        public bool Success { get; set; }
        public bool ApiBaseUriNotSet { get; set; }
        public string Error { get; set; }
        public Exception Exception { get; set; }
        public HttpStatusCode HttpStatusCode { get; set; }
        public TData Data { get; set; }
    }
}
