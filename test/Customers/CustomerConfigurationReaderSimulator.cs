using Moq;
using Office365.UserManagement.Core.Customers;

namespace Office365.UserManagement.Customers
{
	internal class CustomerConfigurationReaderSimulator
		: IReadCustomerConfiguration
	{
		private string customerNumber = string.Empty;

		private readonly Mock<IReadCustomerConfiguration> customerConfigurationReaderMock =
			new Mock<IReadCustomerConfiguration>();

		public CustomerConfigurationReaderSimulator ForCustomerWithNumber(string customerNumber)
		{
			this.customerNumber = customerNumber;

			return this;
		}

		public CustomerConfigurationReaderSimulator ReturnsAutomaticLicensingMode()
		{
			customerConfigurationReaderMock.Setup(customerConfigurationReader =>
				customerConfigurationReader.GetLicensingMode(new CustomerNumber(customerNumber)))
					.Returns(CustomerLicensingMode.Automatic);

			return this;
		}

		public CustomerLicensingMode GetLicensingMode(CustomerNumber customerNumber) =>
			customerConfigurationReaderMock.Object.GetLicensingMode(customerNumber);
	}
}