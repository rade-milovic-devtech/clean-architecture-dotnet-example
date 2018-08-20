using Office365.UserManagement.Core.Subscriptions;

namespace Office365.UserManagement.Subscriptions
{
	public static class CspSubscriptionsBuilder
	{
		public static CspSubscriptions CspSubscriptionsOf(params CspSubscription[] cspSubscriptions) =>
			new CspSubscriptions(cspSubscriptions);
	}
}