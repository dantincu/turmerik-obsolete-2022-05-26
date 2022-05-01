using Microsoft.AspNetCore.Components.Web;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Turmerik.AspNetCore.Infrastructure
{
    public class TextEventArgsWrapper : EventArgsWrapperBase<EventArgs, string>
    {
        public TextEventArgsWrapper(
            EventArgs args,
            string text) : base(args, text)
        {
        }
    }
}
