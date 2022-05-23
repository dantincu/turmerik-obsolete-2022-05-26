using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Turmerik.AspNetCore.Services.Api
{
    public class ApiResponseWrapper<TData>
    {
        public bool Success { get; set; }
        public bool ApiBaseUriNotSet { get; set; }
        public string Error { get; set; }
        public Exception Exception { get; set; }
        public HttpStatusCode HttpStatusCode { get; set; }
        public ApiResponse<TData> Response { get; set; }
    }
}
