using Office365.UserManagement.Core.Customers;
using System;

namespace Office365.UserManagement.Infrastructure.Customers
{
	public static class MongoDbDocumentsDataAdapter
	{
		public static Customer AsCustomer(this CustomerDocument customerDocument) =>
			new Customer(
				new CustomerNumber(customerDocument.Number),
				new CustomerCspId(customerDocument.CspId),
				(CustomerLicensingMode) Enum.Parse(typeof(CustomerLicensingMode), customerDocument.LicensingMode, true));
	}
}