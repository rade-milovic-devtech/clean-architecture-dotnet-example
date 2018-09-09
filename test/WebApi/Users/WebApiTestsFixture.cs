using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using Office365.UserManagement.Core.Users;
using Office365.UserManagement.WebApi.Configuration;
using System.Net.Http;

namespace Office365.UserManagement.WebApi.Users
{
	public class WebApiTestsFixture : WebApplicationFactory<Startup>
	{
		public WebApiTestsFixture()
		{
			HttpClient = CreateDefaultClient();
		}

		protected override void ConfigureWebHost(IWebHostBuilder builder)
		{
			builder.ConfigureServices(services =>
			{
				var userDetailsPresenter = new UserDetailsPresenterStub();
				UserOperations = new UserOperationsSimulator(userDetailsPresenter);

				services.AddScoped<UserDetailsPresenter>(provider => userDetailsPresenter);
				services.AddScoped<IPerformUserOperations>(provider => UserOperations);
			});

			base.ConfigureWebHost(builder);
		}

		public HttpClient HttpClient { get; }

		public UserOperationsSimulator UserOperations { get; private set; }
	}
}