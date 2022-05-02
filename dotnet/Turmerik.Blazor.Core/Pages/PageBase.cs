using Blazored.LocalStorage;
using Blazored.SessionStorage;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.WebUtilities;
using Turmerik.AspNetCore.Infrastructure;
using Turmerik.AspNetCore.LocalSession;
using Turmerik.AspNetCore.Services;
using Turmerik.AspNetCore.Settings;
using Turmerik.Core.Cloneable;

namespace Turmerik.Blazor.Core.Pages
{
    public class PageBase : ComponentBase
    {
        protected ICloneableMapper Mapper { get; set; }
        protected INavManager NavManager { get; set; }
        protected Guid? LocalSessionGuid => NavManager?.LocalSessionGuid;

        protected void IfLocalSessionGuidHasValue(Action<Guid> action)
        {
            if (LocalSessionGuid.HasValue)
            {
                action(LocalSessionGuid.Value);
            }
        }

        protected TOut IfLocalSessionGuidHasValue<TOut>(Func<Guid, TOut> action)
        {
            TOut outVal;

            if (LocalSessionGuid.HasValue)
            {
                outVal = action(LocalSessionGuid.Value);
            }
            else
            {
                outVal = default(TOut);
            }

            return outVal;
        }

        protected void IfLocalSessionGuidHasValue<TIn>(Action<Guid, TIn> action, TIn inVal)
        {
            if (LocalSessionGuid.HasValue)
            {
                action(LocalSessionGuid.Value, inVal);
            }
        }

        protected TOut IfLocalSessionGuidHasValue<TIn, TOut>(Func<Guid, TIn, TOut> action, TIn inVal)
        {
            TOut outVal;

            if (LocalSessionGuid.HasValue)
            {
                outVal = action(LocalSessionGuid.Value, inVal);
            }
            else
            {
                outVal = default(TOut);
            }

            return outVal;
        }

        protected async Task IfLocalSessionGuidHasValueAsync(Func<Guid, Task> action)
        {
            if (LocalSessionGuid.HasValue)
            {
                await action(LocalSessionGuid.Value);
            }
        }

        protected async Task<TOut> IfLocalSessionGuidHasValueAsync<TOut>(Func<Guid, Task<TOut>> action)
        {
            TOut outVal;

            if (LocalSessionGuid.HasValue)
            {
                outVal = await action(LocalSessionGuid.Value);
            }
            else
            {
                outVal = default(TOut);
            }

            return outVal;
        }

        protected async Task IfLocalSessionGuidHasValueAsync<TIn>(Func<Guid, TIn, Task> action, TIn inVal)
        {
            if (LocalSessionGuid.HasValue)
            {
                await action(LocalSessionGuid.Value, inVal);
            }
        }

        protected async Task<TOut> IfLocalSessionGuidHasValueAsync<TIn, TOut>(Func<Guid, TIn, Task<TOut>> action, TIn inVal)
        {
            TOut outVal;

            if (LocalSessionGuid.HasValue)
            {
                outVal = await action(LocalSessionGuid.Value, inVal);
            }
            else
            {
                outVal = default(TOut);
            }

            return outVal;
        }
    }
}
