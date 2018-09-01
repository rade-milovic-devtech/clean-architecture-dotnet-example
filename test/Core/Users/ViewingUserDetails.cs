using Office365.UserManagement.Core.Customers;
using Office365.UserManagement.Core.Subscriptions;
using Xunit;

namespace Office365.UserManagement.Core.Users
{
	[Trait("Category", "Acceptance")]
	public class ViewingUserDetails
	{
		private const string ACustomerNumber = "1234";
		private const string ACustomerCspId = "7bb2bbdf-b9d1-4a62-97c7-92134b351471";
		private const string AUserName = "testuser@testcustomer.onmicrosoft.com";
		private const string AUserFirstName = "FirstName";
		private const string AUserLastName = "LastName";

		[Fact]
		public void PresentsUserContactInformation()
		{
			var userDetailsPresenter = new UserDetailsPresenterSimulator();
			var userOperations = new UserOperations(
				new CustomersInformationStoreSimulator()
					.ForCustomerWithNumber(ACustomerNumber)
					.ReturnsCustomerWith(ACustomerCspId),
				new MicrosoftOffice365UsersOperationsSimulator(),
				new MicrosoftOffice365SubscriptionsOperationsSimulator(),
				userDetailsPresenter);

			userOperations.GetUserDetails(AGetUserDetaisCommandWith(ACustomerNumber, AUserName));

			userDetailsPresenter.HasFormattedUserDataWith(
				AUserName, AUserFirstName, AUserLastName);
		}

		private GetUserDetailsCommand AGetUserDetaisCommandWith(string customerNumber, string userName) =>
			new GetUserDetailsCommand
			{
				CustomerNumber = customerNumber,
				UserName = userName
			};
	}
}