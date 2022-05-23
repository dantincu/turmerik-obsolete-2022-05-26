using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Turmerik.AspNetCore.Infrastructure;
using Turmerik.NetCore.Services;

namespace Turmerik.AspNetCore.Services
{
    public interface ILocalDiskExplorerBackgroundApiRelUriRetriever : ILocalDiskExplorerBackgroundApiMethodNameRetriever
    {
    }

    public class LocalDiskExplorerBackgroundApiRelUriRetriever : ILocalDiskExplorerBackgroundApiRelUriRetriever
    {
        private readonly ILocalDiskExplorerBackgroundApiMethodNameRetriever methodNameRetriever;

        public LocalDiskExplorerBackgroundApiRelUriRetriever(
            ILocalDiskExplorerBackgroundApiMethodNameRetriever localDiskExplorerBackgroundApiMethodNameRetriever)
        {
            this.methodNameRetriever = localDiskExplorerBackgroundApiMethodNameRetriever ?? throw new ArgumentNullException(
                nameof(localDiskExplorerBackgroundApiMethodNameRetriever));

            OpenFolderInOSFileExplorer = GetRelUri(methodNameRetriever.OpenFolderInOSFileExplorer);
            OpenFolderInTrmrkFileExplorer = GetRelUri(methodNameRetriever.OpenFolderInTrmrkFileExplorer);
            OpenFileInOSDefaultApp = GetRelUri(methodNameRetriever.OpenFileInOSDefaultApp);
            OpenFileInOSDefaultTextEditor = GetRelUri(methodNameRetriever.OpenFileInOSDefaultTextEditor);
            OpenFileInOSTrmrkTextEditor = GetRelUri(methodNameRetriever.OpenFileInOSTrmrkTextEditor);
        }

        public Lazy<string> OpenFolderInOSFileExplorer { get; }
        public Lazy<string> OpenFolderInTrmrkFileExplorer { get; }
        public Lazy<string> OpenFileInOSDefaultApp { get; }
        public Lazy<string> OpenFileInOSDefaultTextEditor { get; }
        public Lazy<string> OpenFileInOSTrmrkTextEditor { get; }

        private Lazy<string> GetRelUri(Lazy<string> methodName)
        {
            var relUri = new Lazy<string>(
                () => string.Join("/",
                    TrmrkJsH.Api.Background.LocalDiskExplorer.MAIN_ROUTE_BASE,
                    methodName.Value));

            return relUri;
        }
    }
}
