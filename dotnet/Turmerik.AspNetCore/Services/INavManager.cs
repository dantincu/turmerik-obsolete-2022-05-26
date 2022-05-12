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
        string Url(string relUrl, Dictionary<string, string> queryString = null);
        void NavigateTo(string relUrl, bool forceRefresh = false, Dictionary<string, string> queryString = null);
    }

    public class NavManager : INavManager
    {
        public NavManager(NavigationManager navManager)
        {
            this.Manager = navManager ?? throw new ArgumentNullException(nameof(navManager));
        }

        public NavigationManager Manager { get; }
        public Uri AbsUri => Manager.ToAbsoluteUri(Manager.Uri);
        public IDictionary<string, StringValues> QueryStrings => QueryHelpers.ParseQuery(AbsUri.Query);
        public Guid? LocalSessionGuid => QueryStrings.GetNullableValue(
            QsKeys.LOCAL_SESSION_UUID,
            (StringValues str,
            out Guid val) => Guid.TryParse(
                str,
                out val));

        public string Url(string relUrl, Dictionary<string, string> queryString = null)
        {
            if (LocalSessionGuid.HasValue)
            {
                relUrl = QueryHelpers.AddQueryString(
                    relUrl,
                    QsKeys.LOCAL_SESSION_UUID,
                    LocalSessionGuid.Value.ToString("N"));
            }

            if (queryString != null)
            {
                relUrl = QueryHelpers.AddQueryString(relUrl, queryString);
            }

            return relUrl;
        }

        public void NavigateTo(string relUrl, bool forceRefresh = false, Dictionary<string, string> queryString = null)
        {
            string url = Url(relUrl, queryString);
            Manager.NavigateTo(url, forceRefresh);
        }
    }
}
