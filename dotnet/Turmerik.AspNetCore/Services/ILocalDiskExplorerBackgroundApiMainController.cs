using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Turmerik.AspNetCore.Services.Api;

namespace Turmerik.AspNetCore.Services
{
    public interface ILocalDiskExplorerBackgroundApiMainController
    {
        ApiResponse<object> OpenFolderInOSFileExplorer(FsEntryData fsEntryData);
        ApiResponse<object> OpenFolderInTrmrkFileExplorer(FsEntryData fsEntryData);
        ApiResponse<object> OpenFileInOSDefaultApp(FsEntryData fsEntryData);
        ApiResponse<object> OpenFileInOSDefaultTextEditor(FsEntryData fsEntryData);
        ApiResponse<object> OpenFileInOSTrmrkTextEditor(FsEntryData fsEntryData);
    }
}
