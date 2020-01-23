using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Clients.ActiveDirectory;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace FarmBeats.Partner.Management.Api
{
    public static class Extensions
    {
        public static async Task<AuthenticationResult> GetS2SAccessToken(this IConfigurationRoot config)
        {
            var clientId = config["ClientId"];
            var clientSecret = config["ClientSecret"];
            var resource = config["Resource"];
            var authority = config["Authority"];

            var clientCredential = new ClientCredential(clientId, clientSecret);
            AuthenticationContext context = new AuthenticationContext(authority, false);
            AuthenticationResult authenticationResult = await context.AcquireTokenAsync(resource, clientCredential);  // the client credentials
            return authenticationResult;
        }

        public static async Task<AuthenticationResult> GetS2SAccessToken(string clientId, string clientSecret, string resource, string authority)
        {
            var clientCredential = new ClientCredential(clientId, clientSecret);
            AuthenticationContext context = new AuthenticationContext(authority, false);
            AuthenticationResult authenticationResult = await context.AcquireTokenAsync(resource, clientCredential);  // the client credentials
            return authenticationResult;
        }
    }
}
