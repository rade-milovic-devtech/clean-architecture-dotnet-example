using Moq;

namespace Office365.UserManagement.Core.Customers
{
	public class CustomersInformationStoreSimulator
		: IStoreCustomersInformation
	{
		private readonly Mock<IStoreCustomersInformation> customersInformationStoreMock =
			new Mock<IStoreCustomersInformation>();

		private string customerNumber = string.Empty;

		public CustomersInformationStoreSimulator ForCustomerWithNumber(string customerNumber)
		{
			this.customerNumber = customerNumber;

			return this;
		}

		public CustomersInformationStoreSimulator ReturnsCustomerWith(
			string cspId, CustomerLicensingMode licensingMode)
		{
			customersInformationStoreMock.Setup(customerInformationStore =>
				customerInformationStore.Get(new CustomerNumber(customerNumber)))
					.Returns(new Customer(
						new CustomerNumber(customerNumber),
						new CustomerCspId(cspId),
						licensingMode));

			return this;
		}

		public Customer Get(CustomerNumber customerNumber) =>
			customersInformationStoreMock.Object.Get(customerNumber);
	}
}