using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;
using Office365.UserManagement.Core.Users;
using Office365.UserManagement.WebApi.Configuration;
using System;
using System.Net.Http;

namespace Office365.UserManagement.WebApi.Users
{
	public class WebApiTestsFixture : IDisposable
	{
		private readonly TestServer server;

		public WebApiTestsFixture()
		{
			var builder = new WebHostBuilder()
				.ConfigureServices(services =>
				{
					services.AddScoped<IPerformUserOperations>(_ =>
					{
						UserOperations = new UserOperationsSimulator();

						return UserOperations;
					});
				})
				.UseStartup<Startup>();
			server = new TestServer(builder);

			HttpClient = server.CreateClient();
		}

		public HttpClient HttpClient { get; }

		public UserOperationsSimulator UserOperations { get; private set; }

		public void Dispose()
		{
			HttpClient?.Dispose();
			server?.Dispose();
		}
	}
}