using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.JSInterop;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Turmerik.AspNetCore.Infrastructure;
using Turmerik.AspNetCore.Services;
using Turmerik.Core.Components;
using Turmerik.Core.Services.DriveItems;

namespace Turmerik.Blazor.Core.Pages.Components
{
    public class DriveItemsGridBase : ComponentCoreBase
    {
        protected IJSRuntime JSRuntime { get; set; }
        protected ITimeStampHelper TimeStampH { get; set; }
        protected IMainLayoutService MainLayoutService { get; set; }
        protected DateTime Now { get; set; } = DateTime.Now;
        protected bool SideBarLarge { get; set; }
        protected string ColDateTimeCssClass { get; set; }
        protected string CellDateTimeCssClass { get; set; }

        protected Func<DriveItem, Task> OnDriveItemClickAsync { get; set; }
        protected Func<DriveItem, Task> OnDriveItemOptionsClickAsync { get; set; }

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            await base.OnAfterRenderAsync(firstRender);

            await JSRuntime.InvokeVoidAsync(
                TrmrkJsH.Get(TrmrkJsH.InitDateTimeUserFriendlyLabels),
                this.UuidStr);
        }

        protected void OnSideBarSizeChanged(bool sideBarLarge)
        {
            SideBarLarge = sideBarLarge;
            StateHasChanged();
        }

        protected string FormatDateTimeExact(DateTime? nlblValue)
        {
            string strVal = string.Empty;

            if (nlblValue.HasValue)
            {
                var value = nlblValue.Value;

                if (value > DateTime.MinValue)
                {
                    strVal = TimeStampH.TmStmp(value, true, TimeStamp.Seconds);
                }
            }

            return strVal;
        }

        protected Func<MouseEventArgs, Task> OnDriveItemClickEventHandler(DriveItem driveItem)
        {
            Func<MouseEventArgs, Task> handler = async args =>
            {
                if (OnDriveItemClickAsync != null)
                {
                    await OnDriveItemClickAsync(driveItem);
                }
            };

            return handler;
        }

        protected Func<MouseEventArgs, Task> OnDriveItemOptionsClickEventHandler(DriveItem driveItem)
        {
            Func<MouseEventArgs, Task> handler = async args =>
            {
                if (OnDriveItemOptionsClickAsync != null)
                {
                    await OnDriveItemOptionsClickAsync(driveItem);
                }
            };

            return handler;
        }
    }
}
