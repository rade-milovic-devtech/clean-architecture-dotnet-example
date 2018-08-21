using FluentAssertions;
using MongoDB.Bson;
using MongoDB.Driver;
using Office365.UserManagement.Core.Customers;
using Xunit;

using static Office365.UserManagement.Core.Customers.CustomerLicensingMode;

namespace Office365.UserManagement.Infrastructure.Customers
{
	[Trait("Category", "Integration")]
	public class MongoDbCustomersInformationStoreTests : IClassFixture<MongoDbTestsFixture>
	{
		private const string ACustomerNumber = "1234";
		private const string ACustomerCspId = "4d76dc22-7649-4f84-bc6c-e1bf6921e31c";
		private const string ACustomerLicensingMode = "Automatic";

		private readonly IMongoCollection<BsonDocument> customersCollection;
		private readonly MongoDbCustomersInformationStore customersInformationStore;

		public MongoDbCustomersInformationStoreTests(MongoDbTestsFixture fixture)
		{
			customersCollection = fixture.CustomersCollection;
			customersInformationStore = new MongoDbCustomersInformationStore(fixture.Configuration);
		}

		[Fact]
		public void CustomerIsFoundByCustomerNumberWhenDocumentIsPresentInTheDatabase()
		{
			InsertSampleCustomerWith(ACustomerNumber, ACustomerCspId, ACustomerLicensingMode);

			var customer = customersInformationStore.Get(CustomerNumberOf(ACustomerNumber));

			customer.Number.Should().Be(CustomerNumberOf(ACustomerNumber));
			customer.CspId.Should().Be(CustomerCspIdOf(ACustomerCspId));
			customer.LicensingMode.Should().Be(Automatic);
		}

		private void InsertSampleCustomerWith(string number, string cspId, string licensingMode)
		{
			var sampleCustomerDocument = BsonDocument.Parse
			($@"{{
					""Number"": ""{number}"",
					""CspId"": ""{cspId}"",
					""LicensingMode"": ""{licensingMode}""
				}}");

			customersCollection.InsertOne(sampleCustomerDocument);
		}

		private CustomerNumber CustomerNumberOf(string value) => new CustomerNumber(value);

		private CustomerCspId CustomerCspIdOf(string value) => new CustomerCspId(value);
	}
}