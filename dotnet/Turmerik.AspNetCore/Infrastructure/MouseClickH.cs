using Microsoft.AspNetCore.Components.Web;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Turmerik.Core.Helpers;

namespace Turmerik.AspNetCore.Infrastructure
{
    public static class MouseClickH
    {
        public async static Task InvokeMouseClickAsyncIfReq(
            this Func<MouseEventArgs, Task> eventHandler,
            MouseEventArgs args,
            MouseButtons mouseBtn)
        {
            await eventHandler.InvokeAsyncIfReq(args, a => a.Button.IsMouseButton(mouseBtn));
        }

        public async static Task InvokeMouseClickAsyncIfLeftBtn(
            this Func<MouseEventArgs, Task> eventHandler,
            MouseEventArgs args)
        {
            await eventHandler.InvokeMouseClickAsyncIfReq(args, MouseButtons.Left);
        }

        public async static Task InvokeMouseClickAsyncIfMiddleBtn(
            this Func<MouseEventArgs, Task> eventHandler,
            MouseEventArgs args)
        {
            await eventHandler.InvokeMouseClickAsyncIfReq(args, MouseButtons.Middle);
        }

        public async static Task InvokeMouseClickAsyncIfRightBtn(
            this Func<MouseEventArgs, Task> eventHandler,
            MouseEventArgs args)
        {
            await eventHandler.InvokeMouseClickAsyncIfReq(args, MouseButtons.Right);
        }
    }
}
