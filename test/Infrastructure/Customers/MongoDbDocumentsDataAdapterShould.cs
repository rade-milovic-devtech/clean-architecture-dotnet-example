using FluentAssertions;
using Office365.UserManagement.Core.Customers;
using Xunit;

using static Office365.UserManagement.Core.Customers.CustomerLicensingMode;

namespace Office365.UserManagement.Infrastructure.Customers
{
	[Trait("Category", "Unit")]
	public class MongoDbDocumentsDataAdapterShould
	{
		[Fact]
		public void ProperlyConvertCustomerDocumentToCustomerEntity()
		{
			var customerDocument = new CustomerDocument
			{
				Number = "1234",
				CspId = "4d76dc22-7649-4f84-bc6c-e1bf6921e31c",
				LicensingMode = "Automatic"
			};

			var customer = customerDocument.AsCustomer();

			customer.Number.Should().Be(new CustomerNumber("1234"));
			customer.CspId.Should().Be(new CustomerCspId("4d76dc22-7649-4f84-bc6c-e1bf6921e31c"));
			customer.LicensingMode.Should().Be(Automatic);
		}
	}
}