using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Turmerik.AspNetCore.Hubs
{
    public abstract class HubBase : HubCoreBase
    {
        private const string CURRENT_SESSION_UUID_KEY = "CurrentSessionUuid";

        protected virtual string CurrentSessionUuidKey => CURRENT_SESSION_UUID_KEY;

        protected override Task SendToCurrentUserAsync(string method, CancellationToken cancellationToken, params object[] args)
        {
            throw new NotImplementedException();
        }

        protected override Task SendToCurrentUserAsync(string method, params object[] args)
        {
            throw new NotImplementedException();
        }

        protected Guid? GetCurrentSessionUuid(bool throwIfNull = true)
        {
            Guid? currentSessionGuid = null;
            object currentSessionGuidObj;

            if (Context.Items.TryGetValue(CurrentSessionUuidKey, out currentSessionGuidObj))
            {
                string currentSessionGuidStr = currentSessionGuidObj?.ToString();

                if (!string.IsNullOrWhiteSpace(currentSessionGuidStr))
                {
                    Guid currentSessionGuidValue;

                    if (Guid.TryParse(currentSessionGuidStr, out currentSessionGuidValue))
                    {
                        currentSessionGuid = currentSessionGuidValue;
                    }
                }
            }

            if (!currentSessionGuid.HasValue && throwIfNull)
            {
                throw new InvalidOperationException("Current session guid was not sent");
            }

            return currentSessionGuid;
        }
    }
}
