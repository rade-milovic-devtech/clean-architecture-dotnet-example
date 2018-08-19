using Office365.UserManagement.Core.Customers;
using System.Collections.Generic;

namespace Office365.UserManagement.Core.Subscriptions
{
	public interface IOperateOnMicrosoftOffice365Subscriptions
	{
		IEnumerable<Subscription> GetSubscriptions(CustomerCspId customerCspId);
		void ChangeSubscriptionQuantity(
			CustomerCspId customerCspId, SubscriptionCspId subscriptionCspId, LicenseQuantity newQuantity);
	}
}