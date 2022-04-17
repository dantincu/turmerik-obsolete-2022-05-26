using Microsoft.Extensions.Configuration;
using System;

namespace Turmerik.OneDriveExplorer.Blazor.Server.App.AppSettings
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
