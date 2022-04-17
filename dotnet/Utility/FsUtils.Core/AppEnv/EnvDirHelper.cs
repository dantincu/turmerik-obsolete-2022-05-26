using Turmerik.Core.Helpers;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace FsUtils.Core.AppEnv
{
    public class EnvDirOpts
    {
        public EnvDirOpts()
        {
        }

        public EnvDirOpts(EnvDir envDir)
        {
            EnvDir = envDir;
        }

        public EnvDir EnvDir { get; set; }
        public Type DirNameType { get; set; }
        public string[] PathParts { get; set; }
    }

    public class EnvDirHelper
    {
        public static readonly Lazy<EnvDirHelper> Instance = new Lazy<EnvDirHelper>(() => new EnvDirHelper());

        private EnvDirHelper()
        {
        }

        public string GetPath(EnvDirOpts opts)
        {
            var data = new TempData(opts,
                AppEnvDir.Instance.Value.BasePath);

            string path = GetPathCore(data);
            return path;
        }

        private string GetPathCore(TempData data)
        {
            if (data.Opts.DirNameType != null)
            {
                data.PathPartsList.Add(data.Opts.DirNameType.GetTypeFullDisplayName());
            }

            if (data.Opts.PathParts != null && data.PathPartsList.Any())
            {
                data.PathPartsList.AddRange(data.Opts.PathParts);
            }

            string path = Path.Combine(data.PathPartsList.ToArray());
            return path;
        }

        private class TempData
        {
            public TempData(EnvDirOpts opts, string envDirBasePath)
            {
                Opts = opts ?? throw new ArgumentNullException(nameof(opts));

                PathPartsList = new List<string>
                {
                    envDirBasePath
                };
            }

            public EnvDirOpts Opts { get; }
            public List<string> PathPartsList { get; }
        }
    }
}
