namespace Office365.UserManagement.Infrastructure.Customers
{
	public class MongoDbConfiguration
	{
		public string ConnectionString { get; set; }
		public string DatabaseName { get; set; }
		public string CustomersCollectionName { get; set; }
	}
}