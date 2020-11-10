using Microsoft.AspNetCore.Mvc;
using System.Net.Http;
using System.Threading.Tasks;
using IdentityModel.Client;

namespace ApiTwo.Controllers
{
	public class HomeController : ControllerBase
	{
		private readonly IHttpClientFactory _clientFactory;

		public HomeController(IHttpClientFactory clientFactory)
		{
			_clientFactory = clientFactory;
		}

		[Route("/")]
		public async Task<IActionResult> Index()
		{
			var client = _clientFactory.CreateClient();
			var dicoveryDocument = await client.GetDiscoveryDocumentAsync("https://localhost:44356/");

			var token = await client.RequestClientCredentialsTokenAsync(new ClientCredentialsTokenRequest
			{
				Address = dicoveryDocument.TokenEndpoint,
				ClientId = "oauthClient",
				ClientSecret = "SuperSecretPassword",
				Scope = "ApiOne",
				GrantType = "client_credentials"
			});


			var apiClient = _clientFactory.CreateClient();

			apiClient.SetBearerToken(token.AccessToken);
			var response = await apiClient.GetAsync("http://localhost:59953/secret");

			var content = await response.Content.ReadAsStringAsync();
			return Ok(new 
			{ 
				access_token = token.AccessToken,
			message = content});
		}
	}
}
