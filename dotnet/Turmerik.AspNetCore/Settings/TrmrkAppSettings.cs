using System;

namespace Turmerik.AspNetCore.Settings
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
