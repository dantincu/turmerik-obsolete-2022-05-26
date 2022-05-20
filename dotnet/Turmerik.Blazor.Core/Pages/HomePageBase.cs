using Microsoft.JSInterop;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Turmerik.AspNetCore.Infrastructure;

namespace Turmerik.Blazor.Core.Pages
{
    public abstract class HomePageBase : PageBase
    {
        protected IJSRuntime JSRuntime { get; set; }

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            await base.OnAfterRenderAsync(firstRender);

            await JSRuntime.InvokeVoidAsync(
                TrmrkJsH.Get(TrmrkJsH.AddCssClass),
                null, CssClassH.CssClsSel(
                    CssClassH.DefaultLink), "active");
        }
    }
}
