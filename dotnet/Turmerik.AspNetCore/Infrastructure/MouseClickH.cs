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
            MouseButtons mouseBtn,
            bool invoke = true)
        {
            await eventHandler.InvokeAsyncIfReq(args, a => invoke && a.Button.IsMouseButton(mouseBtn));
        }

        public async static Task InvokeMouseClickAsyncIfLeftBtn(
            this Func<MouseEventArgs, Task> eventHandler,
            MouseEventArgs args,
            bool invoke = true)
        {
            await eventHandler.InvokeMouseClickAsyncIfReq(args, MouseButtons.Left, invoke);
        }

        public async static Task InvokeMouseClickAsyncIfMiddleBtn(
            this Func<MouseEventArgs, Task> eventHandler,
            MouseEventArgs args,
            bool invoke = true)
        {
            await eventHandler.InvokeMouseClickAsyncIfReq(args, MouseButtons.Middle, invoke);
        }

        public async static Task InvokeMouseClickAsyncIfRightBtn(
            this Func<MouseEventArgs, Task> eventHandler,
            MouseEventArgs args,
            bool invoke = true)
        {
            await eventHandler.InvokeMouseClickAsyncIfReq(args, MouseButtons.Right, invoke);
        }

        public async static Task InvokeTextEventAsyncIfReq(
            this Func<TextEventArgsWrapper, Task> eventHandler,
            TextEventArgsWrapper evtArgs,
            bool invoke = true)
        {
            if (eventHandler != null && invoke)
            {
                await eventHandler.Invoke(evtArgs);
            }
        }

        public async static Task InvokeTextEventAsyncIfReq(
            this Func<TextEventArgsWrapper, Task> eventHandler,
            string text,
            EventArgs evtArgs,
            bool invoke = true)
        {
            var args = new TextEventArgsWrapper(evtArgs, text);
            await eventHandler.InvokeTextEventAsyncIfReq(args, invoke);
        }
    }
}
