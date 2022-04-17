using System;
using System.Collections.Generic;
using System.Text;

namespace FsUtils.Core.AppEnv
{
    public class AppStartInfo
    {
        public static readonly Lazy<AppStartInfo> Instance = new Lazy<AppStartInfo>(() => new AppStartInfo());

        private AppStartInfo()
        {
            AppStartTime = DateTime.Now;
            AppStartTicks = AppStartTime.Ticks;
            AppStartGuid = Guid.NewGuid();
        }

        public DateTime AppStartTime { get; }
        public long AppStartTicks { get; }
        public Guid AppStartGuid { get; }
    }
}
