using System;
using System.Collections.Generic;
using System.Text;

namespace FsUtils.Core.AppEnv
{
    public static class EnvDirH
    {
        public static string EnvPath(this EnvDirOpts opts)
        {
            string envPath = EnvDirHelper.Instance.Value.GetPath(opts);
            return envPath;
        }

        public static string EnvPath(
            this EnvDir envDir,
            params string[] pathParts)
        {
            string envPath = envDir.EnvPath(
                null, pathParts);

            return envPath;
        }

        public static string EnvPath(
            this EnvDir envDir,
            Type dirNameType,
            params string[] pathParts)
        {
            var opts = new EnvDirOpts(envDir)
            {
                EnvDir = envDir,
                DirNameType = dirNameType,
                PathParts = pathParts
            };

            string envPath = EnvDirHelper.Instance.Value.GetPath(opts);
            return envPath;
        }
    }
}
