using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Turmerik.AspNetCore.Services
{
    public interface ILocalDiskExplorerBackgroundApiMainController
    {
        bool OpenFolderInOSFileExplorer(FsEntryData fsEntryData);
        bool OpenFolderInTrmrkFileExplorer(FsEntryData fsEntryData);
        bool OpenFileInOSDefaultApp(FsEntryData fsEntryData);
        bool OpenFileInOSDefaultTextEditor(FsEntryData fsEntryData);
        bool OpenFileInOSTrmrkTextEditor(FsEntryData fsEntryData);
    }
}
