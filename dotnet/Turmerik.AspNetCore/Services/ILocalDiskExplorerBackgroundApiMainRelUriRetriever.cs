using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Turmerik.AspNetCore.Infrastructure;
using Turmerik.Core.Components;

namespace Turmerik.AspNetCore.Services
{
    public interface ILocalDiskExplorerBackgroundApiMainRelUriRetriever
    {
        Lazy<string> OpenFolderInOSFileExplorer { get; }
        Lazy<string> OpenFolderInTrmrkFileExplorer { get; }
        Lazy<string> OpenFileInOSDefaultApp { get; }
        Lazy<string> OpenFileInOSDefaultTextEditor { get; }
        Lazy<string> OpenFileInOSTrmrkTextEditor { get; }
    }

    public class LocalDiskExplorerBackgroundApiMainRelUriRetriever : ApiControllerRelUriRetrieverBase<ILocalDiskExplorerBackgroundApiMainController>, ILocalDiskExplorerBackgroundApiMainRelUriRetriever
    {
        public LocalDiskExplorerBackgroundApiMainRelUriRetriever(
            ILambdaExprHelperFactory lambdaExprHelperFactory) : base(
                lambdaExprHelperFactory)
        {
        }

        public Lazy<string> OpenFolderInOSFileExplorer => new Lazy<string>(
            () => GetControllerRelUri(cntrlr => cntrlr.OpenFolderInOSFileExplorer(null)));

        public Lazy<string> OpenFolderInTrmrkFileExplorer => new Lazy<string>(
            () => GetControllerRelUri(cntrlr => cntrlr.OpenFolderInTrmrkFileExplorer(null)));

        public Lazy<string> OpenFileInOSDefaultApp => new Lazy<string>(
            () => GetControllerRelUri(cntrlr => cntrlr.OpenFileInOSDefaultApp(null)));

        public Lazy<string> OpenFileInOSDefaultTextEditor => new Lazy<string>(
            () => GetControllerRelUri(cntrlr => cntrlr.OpenFileInOSDefaultTextEditor(null)));

        public Lazy<string> OpenFileInOSTrmrkTextEditor => new Lazy<string>(
            () => GetControllerRelUri(cntrlr => cntrlr.OpenFileInOSTrmrkTextEditor(null)));

        protected override string ControllerBaseRelUri => TrmrkJsH.Api.Background.LocalDiskExplorer.MAIN_ROUTE_BASE;
    }
}
