using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Office365.UserManagement.Core.Customers;
using Office365.UserManagement.Core.Subscriptions;
using Office365.UserManagement.Core.Users;
using Office365.UserManagement.Infrastructure.Customers;
using Office365.UserManagement.Infrastructure.Subscriptions;
using Office365.UserManagement.Infrastructure.Users;

namespace Office365.UserManagement.WebApi.Configuration
{
	public static class ServicesConfiguration
	{
		public static void ConfigureAppServices(this IServiceCollection services, WebHostBuilderContext context)
		{
			var mongoDbConfigurationSection = context.Configuration.GetSection("MongoDb");
			var mongoDbConfiguration = new MongoDbConfiguration
			{
				ConnectionString = mongoDbConfigurationSection.GetValue<string>("ConnectionString"),
				DatabaseName = mongoDbConfigurationSection.GetValue<string>("DatabaseName"),
				CustomersCollectionName = mongoDbConfigurationSection.GetValue<string>("CustomersCollectionName")
			};
			services.AddSingleton<IStoreCustomersInformation>(_ => new MongoDbCustomersInformationStore(mongoDbConfiguration));
			services.AddSingleton<IOperateOnMicrosoftOffice365Subscriptions, MicrosoftOffice365SubscriptionsOperations>();
			services.AddSingleton<IOperateOnMicrosoftOffice365Users, MicrosoftOffice365UsersOperations>();
			services.AddScoped<IPerformUserOperations, UserOperations>();
		}
	}
}