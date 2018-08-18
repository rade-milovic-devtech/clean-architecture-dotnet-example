using Office365.UserManagement.Core.Users;
using Office365.UserManagement.Customers;
using Xunit;

namespace Office365.UserManagement.Users
{
	[Trait("Category", "Acceptance")]
	public class DeletingLicensedUser
	{
		private const string ACustomerNumber = "1234";
		private const string ACustomerCspId = "7bb2bbdf-b9d1-4a62-97c7-92134b351471";
		private const string AUserName = "testuser@testcustomer.onmicrosoft.com";

		private static DeleteUserCommand ADeleteUserCommandWith(
			string customerNumber, string userName) =>
				new DeleteUserCommand
				{
					CustomerNumber = customerNumber,
					UserName = userName
				};

		public class WhenCustomerUsesAutomaticLicensingMode
		{
			[Fact]
			public void ThenTheUserShouldBeDeleted()
			{
				var microsoftOffice365UserOperations = new MicrosoftOffice365UserOperationsSimulator();
				var userOperations = new UserOperations(
					new CustomerConfigurationReaderSimulator()
						.ForCustomerWithNumber(ACustomerNumber)
						.ReturnsAutomaticLicensingMode(),
					new CustomerInformationStoreSimulator()
						.ForCustomerWithNumber(ACustomerNumber)
						.ReturnsCspIdOf(ACustomerCspId),
					microsoftOffice365UserOperations);

				userOperations.DeleteUser(ADeleteUserCommandWith(ACustomerNumber, AUserName));

				microsoftOffice365UserOperations.HasDeletedAUserWith(ACustomerCspId, AUserName);
			}
		}
	}
}