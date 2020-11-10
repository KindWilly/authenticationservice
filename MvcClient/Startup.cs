using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace MvcClient
{
	public class Startup
	{
		public void ConfigureServices(IServiceCollection services)
		{
			services.AddAuthentication(option =>
			{
				option.DefaultScheme = "Cookie";
				option.DefaultChallengeScheme = "oidc";
			})
				.AddCookie("Cookie")
				.AddOpenIdConnect("oidc", options =>
				{
					options.Authority = "https://localhost:44356/";
					options.ClientId = "client_id_mvc";
					options.ClientSecret = "client_secret_mvc";
					options.SaveTokens = true;
					options.ResponseType = "code";
				});
			services.AddControllersWithViews();
		}

		public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
		{
			if (env.IsDevelopment())
			{
				app.UseDeveloperExceptionPage();
			}

			app.UseRouting();

			app.UseAuthentication()
				.UseAuthorization();

			app.UseEndpoints(endpoints =>
			{
				endpoints.MapDefaultControllerRoute();
			});
		}
	}
}
