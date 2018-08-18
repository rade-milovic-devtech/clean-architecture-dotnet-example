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

		public CustomerInformationStoreSimulator ReturnsCspIdOf(string customerCspId)
		{
			customerInformationStoreMock.Setup(customerInformationStore =>
				customerInformationStore.GetCspIdFor(new CustomerNumber(customerNumber)))
					.Returns(new CustomerCspId(customerCspId));

			return this;
		}

		public CustomerCspId GetCspIdFor(CustomerNumber customerNumber) =>
			customerInformationStoreMock.Object.GetCspIdFor(customerNumber);
	}
}