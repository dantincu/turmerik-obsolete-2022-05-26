using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Turmerik.Core.Components;

namespace Turmerik.NetCore.Services
{
    public interface ILocalDiskExplorerBackgroundApiMethodNameRetriever
    {
        Lazy<string> OpenFolderInOSFileExplorer { get; }
        Lazy<string> OpenFolderInTrmrkFileExplorer { get; }
        Lazy<string> OpenFileInOSDefaultApp { get; }
        Lazy<string> OpenFileInOSDefaultTextEditor { get; }
        Lazy<string> OpenFileInOSTrmrkTextEditor { get; }
    }

    public interface ILocalDiskExplorerBackgroundApiClientMethodNameRetriever
    {
        Lazy<string> OpenFolderInOSFileExplorer { get; }
        Lazy<string> OpenFolderInTrmrkFileExplorer { get; }
        Lazy<string> OpenFileInOSDefaultApp { get; }
        Lazy<string> OpenFileInOSDefaultTextEditor { get; }
        Lazy<string> OpenFileInOSTrmrkTextEditor { get; }
    }
}
