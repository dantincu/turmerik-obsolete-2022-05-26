using System.Xml.Serialization;

namespace FsUtils.TestApp
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            string filePath = "root-env-dir-locator.xml";

            var data = new RootEnvDirLocator
            {
                BasePath = "./MyCustomApps/FileSystemUtilities/ENV/DEV"
            };

            var serlzr = new XmlSerializer(typeof(RootEnvDirLocator));

            using (var sw = new StreamWriter(filePath))
            {
                serlzr.Serialize(sw, data);
            }
        }
    }

    public class RootEnvDirLocator
    {
        public string BasePath { get; set; }
    }
}