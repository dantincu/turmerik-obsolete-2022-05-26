﻿using Microsoft.Extensions.Logging;
using System;
using Turmerik.AspNetCore.AppStartup;
using Turmerik.AspNetCore.Services;
using Turmerik.AspNetCore.UserSession;

namespace Turmerik.OneDriveExplorer.Blazor.Server.App.AppStartup
{
    public class StartupHelper : MsIdentityStartupHelperBase
    {
        public StartupHelper(
            Func<ILogger<MainApplicationLog>> loggerFactory,
            Func<ITrmrkUserSessionsManager> userSessionManagerFactory) : base(
                loggerFactory,
                userSessionManagerFactory)
        {
        }
    }
}