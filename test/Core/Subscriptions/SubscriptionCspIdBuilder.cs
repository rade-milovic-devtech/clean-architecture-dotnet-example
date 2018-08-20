using System.Collections.Generic;

namespace Office365.UserManagement.Core.Subscriptions
{
	public static class SubscriptionCspIdBuilder
	{
		public static IEnumerable<SubscriptionCspId> SubscriptionIdsOf(params SubscriptionCspId[] subscriptionCspIds) => subscriptionCspIds;

		public static SubscriptionCspId SubscriptionIdOf(string value) => new SubscriptionCspId(value);
	}
}