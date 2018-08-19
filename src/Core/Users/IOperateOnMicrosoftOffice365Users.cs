using Office365.UserManagement.Core.Customers;
using Office365.UserManagement.Core.Subscriptions;
using System.Collections.Generic;

namespace Office365.UserManagement.Core.Users
{
	public interface IOperateOnMicrosoftOffice365Users
	{
		IEnumerable<SubscriptionCspId> GetAssignedSubscriptionIds(CustomerCspId customerId, UserName userName);
		void DeleteUser(CustomerCspId customerId, UserName userName);
	}
}