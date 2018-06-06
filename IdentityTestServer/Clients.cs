using IdentityServer4.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IdentityTestServer
{
    public static class Clients
    {
        private static readonly string[] apis = new string[] { "gidb"};
        private static readonly string[] users = new string[]{ "bjo" , "admin"};
        private static readonly string[] roles = new string[] { "admin","processor","verifier","readonly" };
        public static IEnumerable<Client> GetClients()
        {
            return users.SelectMany(u =>
            {
                IEnumerable<string> userPseudonyms = roles.Select(r => $"{u}+{r}");
                IEnumerable<string> apiRoles = apis.SelectMany(a => roles.Select(r => $"{a}.{r}"));
                IEnumerable<Client> clients = userPseudonyms.Select(p => CreateClient(p, apiRoles));
                return clients;
            });
        }

        private static Client CreateClient(string client, IEnumerable<string> roles)
        {
            return new Client
            {
                ClientId = client,

                // no interactive user, use the clientid/secret for authentication
                AllowedGrantTypes = GrantTypes.ClientCredentials,

                // secret for authentication
                ClientSecrets =
                    {
                        new Secret("secret".Sha256())
                    },

                // scopes that client has access to
                AllowedScopes = roles.ToList()
            };
        }
    }
}

