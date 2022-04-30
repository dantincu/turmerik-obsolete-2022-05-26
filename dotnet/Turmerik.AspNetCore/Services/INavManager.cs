using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Primitives;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Turmerik.AspNetCore.Infrastructure;
using Turmerik.Core.Data;
using Turmerik.Core.Helpers;

namespace Turmerik.AspNetCore.Services
{
    public interface INavManager
    {
        NavigationManager Manager { get; }
        Uri AbsUri { get; }
        IDictionary<string, StringValues> QueryStrings { get; }
        Guid? LocalSessionGuid { get; }
        string Url(string relUrl);
    }

    public class NavManager : INavManager
    {
        public NavManager(NavigationManager navManager)
        {
            this.Manager = navManager ?? throw new ArgumentNullException(nameof(navManager));
            AbsUri = navManager.ToAbsoluteUri(navManager.Uri);

            QueryStrings = QueryHelpers.ParseQuery(AbsUri.Query);

            LocalSessionGuid = QueryStrings.GetNullableValue(
                QsKeys.LOCAL_SESSION_ID,
                (StringValues str,
                out Guid val) => Guid.TryParse(
                    str,
                    out val));
        }

        public NavigationManager Manager { get; }
        public Uri AbsUri { get; }
        public IDictionary<string, StringValues> QueryStrings { get; }
        public Guid? LocalSessionGuid { get; }

        public string Url(string relUrl)
        {
            if (LocalSessionGuid.HasValue)
            {
                relUrl = QueryHelpers.AddQueryString(
                    relUrl,
                    QsKeys.LOCAL_SESSION_ID,
                    LocalSessionGuid.Value.ToString());
            }

            return relUrl;
        }
    }
}
