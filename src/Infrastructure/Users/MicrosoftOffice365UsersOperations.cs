using Office365.UserManagement.Core.Customers;
using Office365.UserManagement.Core.Subscriptions;
using Office365.UserManagement.Core.Users;
using System.Collections.Generic;
using System.Linq;

namespace Office365.UserManagement.Infrastructure.Users
{
	public class MicrosoftOffice365UsersOperations : IOperateOnMicrosoftOffice365Users
	{
		public User GetUserDetails(CustomerCspId customerId, UserName userName)
		{
			// Fake implementation. We would use the actual Microsoft Office365 API in here.

			return new User(
				new UserName("testuser@testcustomer.onmicrosoft.com"),
				"FirstName",
				"LastName");
		}

		public IEnumerable<SubscriptionCspId> GetAssignedSubscriptionIds(CustomerCspId customerId, UserName userName)
		{
			// Fake implementation. We would use the actual Microsoft Office365 API in here.

			return Enumerable.Empty<SubscriptionCspId>();
		}

		public void DeleteUser(CustomerCspId customerId, UserName userName)
		{
			// Fake implementation. We would use the actual Microsoft Office365 API in here.
		}
	}
}