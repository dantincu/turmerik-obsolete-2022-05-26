using System;

namespace Turmerik.AspNetCore.Settings
{
    public interface ITrmrkAppSettingsCore
    {
        string AppBaseUrl { get; }
        string LocalDiskExplorerAppBaseUrl { get; }
        string LocalDiskExplorerBackgroundAppBaseUrl { get; }
        string LoginRelUrl { get; }
        string LogoutRelUrl { get; }
        int JsInteropTextChunkMaxCharsCount { get; }
        bool IsDevMode { get; }
        bool UseMockData { get; }
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
            LocalDiskExplorerBackgroundAppBaseUrl = src.LocalDiskExplorerBackgroundAppBaseUrl;
            LocalDiskExplorerAppBaseUrl = src.LocalDiskExplorerAppBaseUrl;
            LoginRelUrl = src.LoginRelUrl;
            LogoutRelUrl = src.LogoutRelUrl;
            JsInteropTextChunkMaxCharsCount = src.JsInteropTextChunkMaxCharsCount;
            IsDevMode = src.IsDevMode;
            UseMockData = src.UseMockData;
        }

        public string AppBaseUrl { get; }
        public string LocalDiskExplorerBackgroundAppBaseUrl { get; }
        public string LocalDiskExplorerAppBaseUrl { get; }
        public string LoginRelUrl { get; }
        public string LogoutRelUrl { get; }
        public int JsInteropTextChunkMaxCharsCount { get; }
        public bool IsDevMode { get; }
        public bool UseMockData { get; }
    }

    public class TrmrkAppSettingsCoreMtbl : ITrmrkAppSettingsCore
    {
        public TrmrkAppSettingsCoreMtbl()
        {
        }

        public TrmrkAppSettingsCoreMtbl(ITrmrkAppSettingsCore src)
        {
            AppBaseUrl = src.AppBaseUrl;
            LocalDiskExplorerBackgroundAppBaseUrl = src.LocalDiskExplorerBackgroundAppBaseUrl;
            LocalDiskExplorerAppBaseUrl = src.LocalDiskExplorerAppBaseUrl;
            LoginRelUrl = src.LoginRelUrl;
            LogoutRelUrl = src.LogoutRelUrl;
            JsInteropTextChunkMaxCharsCount = src.JsInteropTextChunkMaxCharsCount;
            IsDevMode = src.IsDevMode;
            UseMockData = src.UseMockData;
        }

        public string AppBaseUrl { get; set; }
        public string LocalDiskExplorerBackgroundAppBaseUrl { get; set; }
        public string LocalDiskExplorerAppBaseUrl { get; set; }
        public string LoginRelUrl { get; set; }
        public string LogoutRelUrl { get; set; }
        public int JsInteropTextChunkMaxCharsCount { get; set; }
        public bool IsDevMode { get; set; }
        public bool UseMockData { get; set; }
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
