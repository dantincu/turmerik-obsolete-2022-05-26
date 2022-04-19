using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace FsUtils.Core.AppEnv
{
    public static class EnvDirH
    {
        public static string EnvPath(
            this EnvDirHelper services, EnvDir envDir,
            params string[] pathParts)
        {
            string envPath = services.EnvPath(
                envDir, null, pathParts);

            return envPath;
        }

        public static string EnvPath(
            this EnvDirHelper helper, EnvDir envDir,
            Type dirNameType,
            params string[] pathParts)
        {
            var opts = new EnvDirOpts(envDir)
            {
                EnvDir = envDir,
                DirNameType = dirNameType,
                PathParts = pathParts
            };

            string envPath = helper.GetPath(opts);
            return envPath;
        }
    }
}
