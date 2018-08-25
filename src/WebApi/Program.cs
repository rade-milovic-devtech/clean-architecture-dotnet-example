using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Office365.UserManagement.WebApi.Configuration;

namespace Office365.UserManagement.WebApi
{
	public class Program
	{
		public static void Main(string[] args)
		{
			CreateWebHostBuilder(args)
				.Build()
				.Run();
		}

		public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
			WebHost.CreateDefaultBuilder(args)
				.ConfigureServices((context, services) => services.ConfigureAppServices(context))
				.UseStartup<Startup>();
	}
}