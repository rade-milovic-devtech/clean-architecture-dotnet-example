using MongoDB.Driver;
using Office365.UserManagement.Core.Customers;

namespace Office365.UserManagement.Infrastructure.Customers
{
	public class MongoDbCustomersInformationStore : IStoreCustomersInformation
	{
		private readonly IMongoCollection<CustomerDocument> customersCollection;

		public MongoDbCustomersInformationStore(MongoDbConfiguration configuration)
		{
			var client = new MongoClient(configuration.ConnectionString);
			var database = client.GetDatabase(configuration.DatabaseName);

			customersCollection = database.GetCollection<CustomerDocument>(configuration.CustomersCollectionName);
		}

		public Customer Get(CustomerNumber customerNumber)
		{
			var customerDocument = customersCollection.Find(document => document.Number == customerNumber)
				.FirstOrDefault();

			return customerDocument.AsCustomer();
		}
	}
}