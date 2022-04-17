using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace FsUtils.ConsoleApp
{
    internal class ProgramLauncher
    {
        private const string ASSEMBLY_DIR_LOCATOR_FILE_NAME = "assembly-dir-locator.xml";

        private const string FS_UTILS = "FsUtils";
        private const string APP_LIB = "AppLib";

        private const string PROGRAM_COMPONENT = "ProgramComponent";
        private const string DLL = "dll";
        private const string RUN = "Run";

        private static readonly Dictionary<string, string> appLibsDictnr;

        static ProgramLauncher()
        {
            appLibsDictnr = new Dictionary<string, string>
            {
                { "dp", "DirsPair" }
            }.ToDictionary(
                kvp => kvp.Key,
                kvp => $"{FS_UTILS}.{kvp.Value}.{APP_LIB}");
        }

        public void Run(string[] args)
        {
            string cmdName = args.First();
            string appLibName = appLibsDictnr[cmdName];

            string prevWorkDirPath = Directory.GetCurrentDirectory();
            args[0] = prevWorkDirPath;

            string newWorkDirPath = GetAppWorkDirPath(appLibName);
            Directory.SetCurrentDirectory(newWorkDirPath);

            RunCore(args, appLibName, newWorkDirPath);
        }

        private void RunCore(
            string[] args,
            string appLibName,
            string newWorkDirPath)
        {
            string assemblyPath = Path.Combine(
               newWorkDirPath, $"{appLibName}.{DLL}");

            var assembly = Assembly.LoadFrom(assemblyPath);
            Type? type = assembly.GetType($"{appLibName}.{PROGRAM_COMPONENT}");

            object component = Activator.CreateInstance(type);

            MethodInfo? method = type?.GetMethod(
                RUN, 0, new Type[0]);
            
            method.Invoke(component, args);
        }

        private string GetAppWorkDirPath(string appLibName)
        {
            string assemblyDirBasePath = GetAssemblyDirBasePath();

            string workDirPath = Path.Combine(
                assemblyDirBasePath,
                appLibName);

            return workDirPath;
        }

        private string GetAssemblyDirBasePath()
        {
            var assemblyDirLocator = LoadAssemblyDirLocator();
            string assemblyDirBasePath = assemblyDirLocator.BasePath;

            if (!Path.IsPathRooted(assemblyDirBasePath))
            {
                string baseDirPath = Environment.GetFolderPath(
                    Environment.SpecialFolder.ApplicationData);

                assemblyDirBasePath = Path.Combine(
                    baseDirPath,
                    assemblyDirBasePath);
            }

            return assemblyDirBasePath;
        }

        private AssemblyDirLocator LoadAssemblyDirLocator()
        {
            string filePath = ASSEMBLY_DIR_LOCATOR_FILE_NAME;
            AssemblyDirLocator data;

            if (File.Exists(filePath))
            {
                var serlzr = new XmlSerializer(typeof(AssemblyDirLocator));

                using (var sr = new StreamReader(filePath))
                {
                    data = (AssemblyDirLocator)serlzr.Deserialize(sr);
                }
            }
            else
            {
                data = new AssemblyDirLocator
                {
                    BasePath = "./MyCustomApps/FileSystemUtilities/ENV/Bin"
                };
            }

            return data;
        }
    }

    internal class AssemblyDirLocator
    {
        public string? BasePath { get; set; }
    }
}
