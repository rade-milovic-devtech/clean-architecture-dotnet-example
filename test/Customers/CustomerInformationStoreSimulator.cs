using Moq;
using Office365.UserManagement.Core.Customers;

namespace Office365.UserManagement.Customers
{
	internal class CustomerInformationStoreSimulator
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

		public CustomerInformationStoreSimulator ReturnsCustomerWithCspId(string cspId)
		{
			customerInformationStoreMock.Setup(customerInformationStore =>
				customerInformationStore.Get(new CustomerNumber(customerNumber)))
					.Returns(new Customer(
						new CustomerNumber(customerNumber),
						new CustomerCspId(cspId)));

			return this;
		}

		public Customer Get(CustomerNumber customerNumber) =>
			customerInformationStoreMock.Object.Get(customerNumber);
	}
}