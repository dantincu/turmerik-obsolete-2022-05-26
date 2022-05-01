using Microsoft.AspNetCore.Components.Web;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Turmerik.AspNetCore.Infrastructure
{
    public class KeyboardEventArgsWrapper
    {
        public KeyboardEventArgsWrapper(
            KeyboardEventArgs args,
            string domElText)
        {
            Args = args ?? throw new ArgumentNullException(nameof(args));
            DomElText = domElText ?? throw new ArgumentNullException(nameof(domElText));
        }

        public KeyboardEventArgs Args { get; }
        public string DomElText { get; }
    }
}
