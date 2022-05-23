using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Turmerik.NetCore.Services
{
    public interface ILocalDiskExplorerBackgroundApi
    {
        Task<ApiResponse<object>> OpenFolderInOSFileExplorer(FsEntryData fsEntryData);
        Task<ApiResponse<object>> OpenFolderInTrmrkFileExplorer(FsEntryData fsEntryData);
        Task<ApiResponse<object>> OpenFileInOSDefaultApp(FsEntryData fsEntryData);
        Task<ApiResponse<object>> OpenFileInOSDefaultTextEditor(FsEntryData fsEntryData);
        Task<ApiResponse<object>> OpenFileInOSTrmrkTextEditor(FsEntryData fsEntryData);
    }

    public interface ILocalDiskExplorerBackgroundApiClient
    {
        Task OpenFolderInOSFileExplorer(FsEntryData fsEntryData);
        Task OpenFolderInTrmrkFileExplorer(FsEntryData fsEntryData);
        Task OpenFileInOSDefaultApp(FsEntryData fsEntryData);
        Task OpenFileInOSDefaultTextEditor(FsEntryData fsEntryData);
        Task OpenFileInOSTrmrkTextEditor(FsEntryData fsEntryData);
    }
}
