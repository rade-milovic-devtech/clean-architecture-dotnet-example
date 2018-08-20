namespace Office365.UserManagement.Core.Subscriptions
{
	public static class CspSubscriptionsBuilder
	{
		public static CspSubscriptions CspSubscriptionsOf(params CspSubscription[] cspSubscriptions) =>
			new CspSubscriptions(cspSubscriptions);
	}
}