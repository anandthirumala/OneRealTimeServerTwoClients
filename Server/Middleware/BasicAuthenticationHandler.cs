using System;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Text;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace Server.Middleware
{
    public class BasicAuthenticationHandler : AuthenticationHandler<AuthenticationSchemeOptions>
    {
        public BasicAuthenticationHandler
        (
            IOptionsMonitor<AuthenticationSchemeOptions> options,
            ILoggerFactory logger,
            UrlEncoder encoder,
            ISystemClock clock
        ) : base(options, logger, encoder, clock)
        {
        }

        protected override async Task<AuthenticateResult> HandleAuthenticateAsync()
        {
            if (!Request.Headers.ContainsKey("Authorization")) return AuthenticateResult.NoResult();

            (string username, bool authenticated) = await AuthenticateBranch();

            if(string.IsNullOrWhiteSpace(username)) return AuthenticateResult.Fail("Invalid Authorization Header");
            if (!authenticated) return AuthenticateResult.Fail("Invalid Username or Password");

            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, username),
                new Claim(ClaimTypes.Name, username)
            };

            var identity = new ClaimsIdentity(claims, Scheme.Name);
            var principal = new ClaimsPrincipal(identity);
            var ticket = new AuthenticationTicket(principal, Scheme.Name);

            return AuthenticateResult.Success(ticket);
        }

        private async Task<(string username, bool authenticated)> AuthenticateBranch()
        {
            try
            {
                AuthenticationHeaderValue authHeader = AuthenticationHeaderValue.Parse(Request.Headers["Authorization"]);
                if (string.IsNullOrWhiteSpace(authHeader.Parameter)) return (string.Empty, false);

                (string username, string password) = GetUsernameAndPassword(authHeader.Parameter);
                bool authenticated = await AuthenticateBranch(username, password);

                return (username, authenticated);
            }
            catch (Exception exception)
            {
                this.Logger.LogError(exception, exception.Message);
                return (string.Empty, false);
            }
        }

        private static async Task<bool> AuthenticateBranch(string username, string password)
        {
            //Query database/api and check for valid credentials here
            await Task.Delay(100);

            return username == "4093" && password == "C8814A75-B4C2-40D3-AAF0-6F05AD558378";
        }

        private static (string username, string password) GetUsernameAndPassword(string parameter)
        {
            byte[] credentialBytes = Convert.FromBase64String(parameter);
            string[] credentials = Encoding.UTF8.GetString(credentialBytes).Split(new[] {':'}, 2);

            return (credentials[0], credentials[1]);
        }
    }
}
