using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Text.Encodings.Web;
using System.Threading.Tasks;

namespace Turmerik.AspNetCore.Authentication
{
    public class AnonymousAuthenticationHandler : AuthenticationHandler<AuthenticationSchemeOptions>
    {
        public const string POLICY = "anonymous";
        public const string SCHEME = "anonymous";
        public const string AUTHENTICATION_TYPE = "anonymous";
        public const string AUTHENTICATED_NAME = "anonymous";

        public AnonymousAuthenticationHandler(
            IOptionsMonitor<AuthenticationSchemeOptions> options,
            ILoggerFactory logger,
            UrlEncoder encoder,
            ISystemClock clock) : base(
                options,
                logger,
                encoder,
                clock)
        {
        }

        protected override async Task<AuthenticateResult> HandleAuthenticateAsync()
        {
            var result = new AnonymousAuthenticationResult();
            return result;
        }
    }

    public class AnonymousAuthenticationResult : AuthenticateResult
    {
        public AnonymousAuthenticationResult()
        {
            Ticket = new AuthenticationTicket(
                new System.Security.Claims.ClaimsPrincipal(
                    new AnonymousIdentity()),
                AnonymousAuthenticationHandler.POLICY);
        }
    }

    public class AnonymousAuthorizationRequirement : IAuthorizationRequirement
    {
    }

    public class AnonymousIdentity : IIdentity
    {
        public string? AuthenticationType => AnonymousAuthenticationHandler.AUTHENTICATION_TYPE;
        public bool IsAuthenticated => true;
        public string? Name => AnonymousAuthenticationHandler.AUTHENTICATED_NAME;
    }
}
