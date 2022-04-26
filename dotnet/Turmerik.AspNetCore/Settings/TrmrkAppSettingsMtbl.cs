using System;

namespace Turmerik.AspNetCore.Settings
{
    public interface ITrmrkAppSettingsCore
    {
        string AppBaseUrl { get; }
        string LoginRelUrl { get; }
        string LogoutRelUrl { get; }
    }

    public interface ITrmrkAppSettings : ITrmrkAppSettingsCore
    {
        string LoginUrl { get; }
        string LogoutUrl { get; }
    }

    public class TrmrkAppSettingsCoreImmtbl : ITrmrkAppSettingsCore
    {
        public TrmrkAppSettingsCoreImmtbl(ITrmrkAppSettingsCore src)
        {
            AppBaseUrl = src.AppBaseUrl;
            LoginRelUrl = src.LoginRelUrl;
            LogoutRelUrl = src.LogoutRelUrl;
        }

        public string AppBaseUrl { get; }
        public string LoginRelUrl { get; }
        public string LogoutRelUrl { get; }
    }

    public class TrmrkAppSettingsCoreMtbl : ITrmrkAppSettingsCore
    {
        public TrmrkAppSettingsCoreMtbl()
        {
        }

        public TrmrkAppSettingsCoreMtbl(ITrmrkAppSettingsCore src)
        {
            AppBaseUrl = src.AppBaseUrl;
            LoginRelUrl = src.LoginRelUrl;
            LogoutRelUrl = src.LogoutRelUrl;
        }

        public string AppBaseUrl { get; set; }
        public string LoginRelUrl { get; set; }
        public string LogoutRelUrl { get; set; }
    }

    public class TrmrkAppSettingsImmtbl : TrmrkAppSettingsCoreImmtbl, ITrmrkAppSettings
    {
        public TrmrkAppSettingsImmtbl(ITrmrkAppSettings src) : base(src)
        {
            LoginUrl = src.LoginUrl;
            LogoutUrl = src.LogoutUrl;
        }

        public string LoginUrl { get; }
        public string LogoutUrl { get; }
    }

    public class TrmrkAppSettingsMtbl : TrmrkAppSettingsCoreMtbl, ITrmrkAppSettings
    {
        public TrmrkAppSettingsMtbl()
        {
        }

        public TrmrkAppSettingsMtbl(ITrmrkAppSettings src) : base(src)
        {
            LoginUrl = src.LoginUrl;
            LogoutUrl = src.LogoutUrl;
        }

        public string LoginUrl { get; set; }
        public string LogoutUrl { get; set; }
    }
}
