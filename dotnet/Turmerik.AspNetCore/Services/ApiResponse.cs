using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Turmerik.AspNetCore.Services
{
    public class ApiResponse<TData>
    {
        public bool Success { get; set; }
        public TData Data { get; set; }
        public string Error { get; set; }
    }
}
