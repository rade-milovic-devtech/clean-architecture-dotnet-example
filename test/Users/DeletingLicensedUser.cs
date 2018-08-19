using Office365.UserManagement.Core.Users;
using Office365.UserManagement.Customers;
using Office365.UserManagement.Subscriptions;
using Xunit;

using static Office365.UserManagement.Subscriptions.SubscriptionBuilder;

namespace Office365.UserManagement.Users
{
	[Trait("Category", "Acceptance")]
	public class DeletingLicensedUser
	{
		private const string ACustomerNumber = "1234";
		private const string ACustomerCspId = "7bb2bbdf-b9d1-4a62-97c7-92134b351471";
		private const string AUserName = "testuser@testcustomer.onmicrosoft.com";
		private const string ASubscriptionCspId = "f89f817e-cc3f-48e5-8349-5fba8a8fbc69";
		private const int NumberOfAvailableLicenses = 5;
		private const int NumberOfAssignedLicenses = 1;

		private static DeleteUserCommand ADeleteUserCommandWith(
			string customerNumber, string userName) =>
				new DeleteUserCommand
				{
					CustomerNumber = customerNumber,
					UserName = userName
				};

		public class WhenCustomerUsesAutomaticLicensingMode
		{
			private readonly MicrosoftOffice365UsersOperationsSimulator microsoftOffice365UsersOperations;
			private readonly MicrosoftOffice365SubscriptionsOperationsSimulator microsoftOffice365SubscriptionsOperations;

			public WhenCustomerUsesAutomaticLicensingMode()
			{
				microsoftOffice365UsersOperations = new MicrosoftOffice365UsersOperationsSimulator()
					.ForCustomerWithCspId(ACustomerCspId)
					.AndUser(AUserName)
					.ReturnsSubscriptionIds(ASubscriptionCspId);
				microsoftOffice365SubscriptionsOperations = new MicrosoftOffice365SubscriptionsOperationsSimulator()
					.ForCustomerWithCspId(ACustomerCspId)
					.ReturnsSubscriptions(
						ASubscription
							.WithId(ASubscriptionCspId)
							.WithAvailableLicensesOf(NumberOfAvailableLicenses)
							.WithAssignedLicensesOf(NumberOfAssignedLicenses));

				var userOperations = new UserOperations(
					new CustomerConfigurationReaderSimulator()
						.ForCustomerWithNumber(ACustomerNumber)
						.ReturnsAutomaticLicensingMode(),
					new CustomerInformationStoreSimulator()
						.ForCustomerWithNumber(ACustomerNumber)
						.ReturnsCustomerWithCspId(ACustomerCspId),
					microsoftOffice365UsersOperations,
					microsoftOffice365SubscriptionsOperations);

				userOperations.DeleteUser(ADeleteUserCommandWith(ACustomerNumber, AUserName));
			}

			[Fact]
			public void ThenTheUserShouldBeDeleted()
			{
				microsoftOffice365UsersOperations.HasDeletedAUserWith(ACustomerCspId, AUserName);
			}

			[Fact]
			public void ThenTheQuantityOfTheAffectedSubscriptionsShouldBeAlignedWithTheNumberOfAssignedLicenses()
			{
				microsoftOffice365SubscriptionsOperations.HasChangedSubscriptionQuantityFor(
					ACustomerCspId, ASubscriptionCspId, NumberOfAssignedLicenses);
			}
		}
	}
}