using Microsoft.AspNetCore.Authentication;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using SuperfonMobileAPI.Services;
using SuperfonWorks.Data;
using System;
using System.Security.Claims;
using System.Text;
using System.Text.Encodings.Web;
using System.Threading.Tasks;

namespace SuperfonMobileAPI.Shared
{
    public class BasicAuthenticationHandler : AuthenticationHandler<AuthenticationSchemeOptions>
    {
        private readonly SuperfonWorksContext _superfonWorksContext;
        private readonly SecurityService _securityService;
        public BasicAuthenticationHandler(
            IOptionsMonitor<AuthenticationSchemeOptions> options,
            ILoggerFactory logger,
            UrlEncoder encoder,
            ISystemClock clock,
            SuperfonWorksContext superfonWorksContext,
            SecurityService securityService
            ) : base(options, logger, encoder, clock)
        {
            _superfonWorksContext = superfonWorksContext;
            _securityService = securityService;
        }

        protected async override Task<AuthenticateResult> HandleAuthenticateAsync()
        {
            var authHeader = Request.Headers["Authorization"].ToString();
            if (authHeader != null && authHeader.StartsWith("basic", StringComparison.OrdinalIgnoreCase))
            {
                var token = authHeader.Substring("Basic ".Length).Trim();

                var credentialstring = Encoding.UTF8.GetString(Convert.FromBase64String(token));
                var credentials = credentialstring.Split(':');

                var user = await _superfonWorksContext.Users.SingleOrDefaultAsync(x => x.Username.ToLower() == credentials[0].ToLower());
                if (user != null)
                {
                    var passHash = _securityService.ComputeSha256Hash(credentials[1]);
                    if (user.PassHash != passHash || !user.IsActive)
                    {
                        Response.StatusCode = 401;
                        return await Task.FromResult(AuthenticateResult.Fail("Invalid Authorization Header"));
                    }

                    var claims = new[] { new Claim("UserId", user.UserId.ToString()) /*, new Claim(ClaimTypes.Role, "Admin") */};
                    var identity = new ClaimsIdentity(claims, "Basic");
                    var claimsPrincipal = new ClaimsPrincipal(identity);
                    return await Task.FromResult(AuthenticateResult.Success(new AuthenticationTicket(claimsPrincipal, Scheme.Name)));
                }

                Response.StatusCode = 401;
                return await Task.FromResult(AuthenticateResult.Fail("Invalid Authorization Header"));
            }
            else
            {
                Response.StatusCode = 401;
                return await Task.FromResult(AuthenticateResult.Fail("Invalid Authorization Header"));
            }
        }
    }
}
