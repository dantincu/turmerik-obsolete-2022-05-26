using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Turmerik.Core.Components;

namespace Turmerik.AspNetCore.Services
{
    public class UIBlockingOverlayViewModel
    {
        public ErrorViewModel Error { get; set; }
        public bool Enabled { get; set; }
        public bool ShowBackBtn { get; set; }
        public SetApiBaseUriViewModel SetApiBaseUri { get; set; }
    }

    public class SetApiBaseUriViewModel
    {
        public string ApiKey { get; set; }
        public string ApiBaseUri { get; set; }
    }
}
