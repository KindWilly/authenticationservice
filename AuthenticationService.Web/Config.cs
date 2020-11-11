using IdentityModel;
using IdentityServer4.Models;
using System.Collections.Generic;

namespace AuthenticationService.Web
{
    public static class Config
    {
        public static IEnumerable<IdentityResource> GetIdentityResources() =>
            new[]
            {
                new IdentityResources.OpenId(),
                new IdentityResources.Profile(),
                new IdentityResource
                {
                    Name = "role",
                    UserClaims = new List<string> { "role" }
                }
            };

        public static IEnumerable<ApiResource> GetApiResources() =>
            new[]
            {
                new ApiResource
                {
                    Name = "ApiOne",
                    DisplayName = "ApiOne #1",
                    Description = "Allow the application to access ApiOne #1 on your behalf",
                    Scopes = new List<string> { "ApiOne" },
                    ApiSecrets = new List<Secret> {new Secret("ScopeSecret".Sha256())},
                    UserClaims = new List<string> {"role"}
                },
                new ApiResource
                {
                    Name = "ApiTwo",
                    DisplayName = "ApiTwo #1",
                    Description = "Allow the application to access ApiTwo #1 on your behalf",
                    Scopes = new List<string> { "ApiOne" },
                    ApiSecrets = new List<Secret> {new Secret("ScopeSecret".Sha256())},
                    UserClaims = new List<string> {"role"}
                }
            };

        public static IEnumerable<ApiScope> GetApiScopes() =>
            new[]
            {
                new ApiScope("ApiOne"),
                new ApiScope("ApiTwo")
            };

        public static IEnumerable<Client> GetClients() =>
            new List<Client>
            {
                new Client
                {
                    ClientId = "oauthClient",
                    ClientSecrets = { new Secret("SuperSecretPassword".ToSha256()) },
                    AllowedGrantTypes = GrantTypes.ClientCredentials,
                    AllowedScopes = { "ApiOne" }
                },
                new Client
                {
                    ClientId = "client_id_mvc",
                    ClientSecrets = { new Secret("client_secret_mvc".ToSha256()) },
                    AllowedGrantTypes = GrantTypes.Code,
                    RedirectUris = { "https://localhost:44379/signin-oidc" },
                    AllowedScopes = { "ApiOne", "ApiTwo", "openid", "profile" }
                }
            };
    }
}