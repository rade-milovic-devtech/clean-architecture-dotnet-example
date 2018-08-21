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
		public static void ConfigureAppServices(this IServiceCollection services)
		{
			// TODO: We should populate this from the configuration file.
			var configuration = new MongoDbConfiguration
			{
				ConnectionString = "mongodb://localhost:27017",
				DatabaseName = "office365",
				CustomersCollectionName = "customers"
			};
			services.AddScoped<IStoreCustomersInformation>(_ => new MongoDbCustomersInformationStore(configuration));
			services.AddScoped<IOperateOnMicrosoftOffice365Subscriptions, MicrosoftOffice365SubscriptionsOperations>();
			services.AddScoped<IOperateOnMicrosoftOffice365Users, MicrosoftOffice365UsersOperations>();
			services.AddScoped<IPerformUserOperations, UserOperations>();
		}
	}
}