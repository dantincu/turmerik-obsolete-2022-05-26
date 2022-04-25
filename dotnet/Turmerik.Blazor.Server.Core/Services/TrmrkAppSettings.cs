using System;

namespace Turmerik.Blazor.Server.Core.Services
{
    public class TrmrkAppSettingsCore
    {
        public string AppBaseUrl { get; set; }
        public string LoginRelUrl { get; set; }
    }

    public class TrmrkAppSettings : TrmrkAppSettingsCore
    {
        public string LoginUrl { get; set; }
    }
}
