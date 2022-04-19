using Turmerik.Core.Helpers;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace FsUtils.Core.AppEnv
{
    public interface IAppEnvDir
    {
        string BasePath { get; }
    }

    public class AppEnvDir : IAppEnvDir
    {
        private const string ENV_DIR_LOCATOR_FILE_NAME = "env-dir-locator.json";

        public AppEnvDir()
        {
            BasePath = GetBasePath();
        }

        public string BasePath { get; }

        private string GetBasePath()
        {
            EnvDirLocator envDirLocator;
            string envDirLocatorFilePath = ENV_DIR_LOCATOR_FILE_NAME;

            string json = File.ReadAllText(envDirLocatorFilePath);
            envDirLocator = json.FromJson<EnvDirLocator>();

            string envDirBasePath = envDirLocator.EnvDirBasePath;

            if (!Path.IsPathRooted(envDirBasePath))
            {
                string baseDirPath = Environment.GetFolderPath(
                    Environment.SpecialFolder.ApplicationData);

                envDirBasePath = Path.Combine(
                    baseDirPath,
                    envDirBasePath);
            }

            return envDirBasePath;
        }
    }

    public class EnvDirLocator
    {
        public string EnvDirBasePath { get; set; }
    }
}
