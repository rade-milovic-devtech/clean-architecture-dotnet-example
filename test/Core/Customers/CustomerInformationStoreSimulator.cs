using Moq;

namespace Office365.UserManagement.Core.Customers
{
	public class CustomerInformationStoreSimulator
		: IStoreCustomerInformation
	{
		private readonly Mock<IStoreCustomerInformation> customerInformationStoreMock =
			new Mock<IStoreCustomerInformation>();

		private string customerNumber = string.Empty;

		public CustomerInformationStoreSimulator ForCustomerWithNumber(string customerNumber)
		{
			this.customerNumber = customerNumber;

			return this;
		}

		public CustomerInformationStoreSimulator ReturnsCustomerWith(
			string cspId, CustomerLicensingMode licensingMode)
		{
			customerInformationStoreMock.Setup(customerInformationStore =>
				customerInformationStore.Get(new CustomerNumber(customerNumber)))
					.Returns(new Customer(
						new CustomerNumber(customerNumber),
						new CustomerCspId(cspId),
						licensingMode));

			return this;
		}

		public Customer Get(CustomerNumber customerNumber) =>
			customerInformationStoreMock.Object.Get(customerNumber);
	}
}