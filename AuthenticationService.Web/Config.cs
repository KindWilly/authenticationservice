using IdentityModel;
using IdentityServer4.Models;
using IdentityServer4.Test;
using System.Collections.Generic;
using System.Security.Claims;

namespace AuthenticationService.Web
{
	public static class Config
	{
		public static IEnumerable<IdentityResource> GetIdentityResources() =>
			new[]
			{
				new IdentityResources.OpenId(),
				new IdentityResources.Profile(),
				new IdentityResources.Email(),
				new IdentityResource
				{
					Name = "role",
					UserClaims = new List<string> {"role"}
				}
			};

		public static IEnumerable<ApiResource> GetApiResources() =>
			new []
			{
				new ApiResource
				{
					Name = "ApiOne",
					DisplayName = "ApiOne #1",
					Description = "Allow the application to access API #1 on your behalf",
					Scopes = new List<string> { "ApiOne" },
					ApiSecrets = new List<Secret> {new Secret("ScopeSecret".Sha256())},
					UserClaims = new List<string> {"role"}
				}
			};

		public static IEnumerable<ApiScope> GetApiScopes() =>
			new[]
			{
				new ApiScope("ApiOne")
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
				}
			};

		public static List<TestUser> Get()
		{
			return new List<TestUser> {
			new TestUser {
				SubjectId = "5BE86359-073C-434B-AD2D-A3932222DABE",
				Username = "scott",
				Password = "password",
				Claims = new List<Claim> {
					new Claim(JwtClaimTypes.Email, "scott@scottbrady91.com"),
					new Claim(JwtClaimTypes.Role, "admin")
				}
			}
		};
		}
	}
}
