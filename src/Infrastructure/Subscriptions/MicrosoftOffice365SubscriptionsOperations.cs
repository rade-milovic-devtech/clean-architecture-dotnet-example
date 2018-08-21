using Office365.UserManagement.Core.Customers;
using Office365.UserManagement.Core.Subscriptions;
using System.Linq;

namespace Office365.UserManagement.Infrastructure.Subscriptions
{
	public class MicrosoftOffice365SubscriptionsOperations : IOperateOnMicrosoftOffice365Subscriptions
	{
		public CspSubscriptions GetSubscriptions(CustomerCspId customerId)
		{
			// Fake implementation. We would use the actual Microsoft Office365 API in here.

			return new CspSubscriptions(Enumerable.Empty<CspSubscription>());
		}

		public void ChangeSubscriptionQuantity(CustomerCspId customerId, SubscriptionCspId subscriptionId,
			LicenseQuantity newQuantity)
		{
			// Fake implementation. We would use the actual Microsoft Office365 API in here.
		}
	}
}