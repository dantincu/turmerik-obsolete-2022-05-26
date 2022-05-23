using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Turmerik.Core.Components;

namespace Turmerik.NetCore.Services
{
    public class LocalDiskExplorerBackgroundApiMethodNameRetriever : ILocalDiskExplorerBackgroundApiMethodNameRetriever
    {
        private readonly ILambdaExprHelper<ILocalDiskExplorerBackgroundApi> lambdaExprHelper;

        public LocalDiskExplorerBackgroundApiMethodNameRetriever(
            ILambdaExprHelperFactory lambdaExprHelperFactory)
        {
            lambdaExprHelper = lambdaExprHelperFactory.GetHelper<ILocalDiskExplorerBackgroundApi>();

            OpenFolderInOSFileExplorer = GetMethodName(c => c.OpenFolderInOSFileExplorer(null));
            OpenFolderInTrmrkFileExplorer = GetMethodName(c => c.OpenFolderInTrmrkFileExplorer(null));
            OpenFileInOSDefaultApp = GetMethodName(c => c.OpenFileInOSDefaultApp(null));
            OpenFileInOSDefaultTextEditor = GetMethodName(c => c.OpenFileInOSDefaultTextEditor(null));
            OpenFileInOSTrmrkTextEditor = GetMethodName(c => c.OpenFileInOSTrmrkTextEditor(null));
        }

        public Lazy<string> OpenFolderInOSFileExplorer { get; }
        public Lazy<string> OpenFolderInTrmrkFileExplorer { get; }
        public Lazy<string> OpenFileInOSDefaultApp { get; }
        public Lazy<string> OpenFileInOSDefaultTextEditor { get; }
        public Lazy<string> OpenFileInOSTrmrkTextEditor { get; }

        private Lazy<string> GetMethodName<TPropValue>(Expression<Func<ILocalDiskExplorerBackgroundApi, TPropValue>> methodExpr)
        {
            var methodName = new Lazy<string>(() => lambdaExprHelper.MethodName(methodExpr));
            return methodName;
        }
    }

    public class LocalDiskExplorerBackgroundApiClientMethodNameRetriever : ILocalDiskExplorerBackgroundApiClientMethodNameRetriever
    {
        private readonly ILambdaExprHelper<ILocalDiskExplorerBackgroundApi> lambdaExprHelper;

        public LocalDiskExplorerBackgroundApiClientMethodNameRetriever(
            ILambdaExprHelperFactory lambdaExprHelperFactory)
        {
            lambdaExprHelper = lambdaExprHelperFactory.GetHelper<ILocalDiskExplorerBackgroundApi>();

            OpenFolderInOSFileExplorer = GetMethodName(c => c.OpenFolderInOSFileExplorer(null));
            OpenFolderInTrmrkFileExplorer = GetMethodName(c => c.OpenFolderInTrmrkFileExplorer(null));
            OpenFileInOSDefaultApp = GetMethodName(c => c.OpenFileInOSDefaultApp(null));
            OpenFileInOSDefaultTextEditor = GetMethodName(c => c.OpenFileInOSDefaultTextEditor(null));
            OpenFileInOSTrmrkTextEditor = GetMethodName(c => c.OpenFileInOSTrmrkTextEditor(null));
        }

        public Lazy<string> OpenFolderInOSFileExplorer { get; }
        public Lazy<string> OpenFolderInTrmrkFileExplorer { get; }
        public Lazy<string> OpenFileInOSDefaultApp { get; }
        public Lazy<string> OpenFileInOSDefaultTextEditor { get; }
        public Lazy<string> OpenFileInOSTrmrkTextEditor { get; }

        private Lazy<string> GetMethodName<TPropValue>(Expression<Func<ILocalDiskExplorerBackgroundApi, TPropValue>> methodExpr)
        {
            var methodName = new Lazy<string>(() => lambdaExprHelper.MethodName(methodExpr));
            return methodName;
        }
    }
}
