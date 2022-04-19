using System;
using System.Collections.Generic;
using System.Text;

namespace FsUtils.Core.AppEnv
{
    public interface IAppStartInfo
    {
        DateTime AppStartTime { get; }
        long AppStartTicks { get; }
        Guid AppStartGuid { get; }
    }

    public class AppStartInfo : IAppStartInfo
    {
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
