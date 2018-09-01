using Office365.UserManagement.Core.Customers;
using Office365.UserManagement.Core.Subscriptions;
using System.Collections.Generic;

namespace Office365.UserManagement.Core.Users
{
	public interface IOperateOnMicrosoftOffice365Users
	{
		User GetUserDetails(CustomerCspId customerId, UserName userName);
		IEnumerable<SubscriptionCspId> GetAssignedSubscriptionIds(CustomerCspId customerId, UserName userName);
		void DeleteUser(CustomerCspId customerId, UserName userName);
	}
}