using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Primitives;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Turmerik.AspNetCore.Services
{
    public interface INavManager
    {
        NavigationManager Manager { get; }
        Uri AbsUri { get; }
        Dictionary<string, StringValues> QueryStrings { get; }
    }

    public class NavManager : INavManager
    {
        public NavManager(NavigationManager navManager)
        {
            this.Manager = navManager ?? throw new ArgumentNullException(nameof(navManager));
            AbsUri = navManager.ToAbsoluteUri(navManager.Uri);

            QueryStrings = QueryHelpers.ParseQuery(AbsUri.Query);
        }

        public NavigationManager Manager { get; }
        public Uri AbsUri { get; }
        public Dictionary<string, StringValues> QueryStrings { get; }
    }
}
