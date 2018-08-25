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
			var userOperations = new UserOperationsSimulator();

			var builder = new WebHostBuilder()
				.ConfigureServices(services =>
				{
					services.AddScoped<IPerformUserOperations>(_ => userOperations);
				})
				.UseStartup<Startup>();
			server = new TestServer(builder);

			HttpClient = server.CreateClient();

			UserOperations = userOperations;
		}

		public HttpClient HttpClient { get; }

		public UserOperationsSimulator UserOperations { get; }

		public void Dispose()
		{
			HttpClient?.Dispose();
			server?.Dispose();
		}
	}
}