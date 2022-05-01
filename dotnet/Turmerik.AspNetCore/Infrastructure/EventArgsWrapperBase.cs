using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Turmerik.AspNetCore.Infrastructure
{
    public abstract class EventArgsWrapperBase<TEventArgs, TValue>
        where TEventArgs : EventArgs
    {
        public EventArgsWrapperBase(
            TEventArgs eventArgs,
            TValue value)
        {
            EventArgs = eventArgs ?? throw new ArgumentNullException(nameof(eventArgs));
            Value = value;
        }

        public TEventArgs EventArgs { get; }
        public TValue Value { get; }
    }
}
