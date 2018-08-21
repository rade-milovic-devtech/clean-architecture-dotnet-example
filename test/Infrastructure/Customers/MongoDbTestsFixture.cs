using Mongo2Go;
using MongoDB.Bson;
using MongoDB.Driver;
using System;

namespace Office365.UserManagement.Infrastructure.Customers
{
	public class MongoDbTestsFixture : IDisposable
	{
		private const string DatabaseName = "integration-test";
		private const string CustomersCollectionName = "customers";

		private readonly MongoDbRunner runner;

		public MongoDbTestsFixture()
		{
			runner = MongoDbRunner.Start();

			Configuration = new MongoDbConfiguration
			{
				ConnectionString = runner.ConnectionString,
				DatabaseName = DatabaseName,
				CustomersCollectionName = CustomersCollectionName
			};

			var client = new MongoClient(runner.ConnectionString);
			var database = client.GetDatabase(DatabaseName);

			CustomersCollection = database.GetCollection<BsonDocument>(CustomersCollectionName);
		}

		public MongoDbConfiguration Configuration { get; }

		public IMongoCollection<BsonDocument> CustomersCollection { get; }

		public void Dispose()
		{
			runner?.Dispose();
		}
	}
}