using System.Collections.Generic;
using Office365.UserManagement.Core.Subscriptions;

namespace Office365.UserManagement.Subscriptions
{
	public static class SubscriptionCspIdBuilder
	{
		public static IEnumerable<SubscriptionCspId> SubscriptionIdsOf(params SubscriptionCspId[] subscriptionCspIds) => subscriptionCspIds;

		public static SubscriptionCspId SubscriptionIdOf(string value) => new SubscriptionCspId(value);
	}
}