using Office365.UserManagement.Core.Customers;

namespace Office365.UserManagement.Core.Subscriptions
{
	public interface IOperateOnMicrosoftOffice365Subscriptions
	{
		CspSubscriptions GetSubscriptions(CustomerCspId customerId);
		void ChangeSubscriptionQuantity(
			CustomerCspId customerId, SubscriptionCspId subscriptionId, LicenseQuantity newQuantity);
	}
}