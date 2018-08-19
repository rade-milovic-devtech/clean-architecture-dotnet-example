using FluentAssertions;
using Office365.UserManagement.Core.Subscriptions;
using System.Collections.Generic;
using Xunit;

using static Office365.UserManagement.Subscriptions.CspSubscriptionBuilder;

namespace Office365.UserManagement.Subscriptions
{
	public class CspSubscriptionsShould
	{
		[Fact]
		public void OnlyReturnCspSubscriptionsWithGivenSubscriptionIds()
		{
			var cspSubscriptions = CspSubscriptionsOf(
				ACspSubscription
					.WithId("4a8b014f-8f37-47e1-9f72-41727b4973cb"),
				ACspSubscription
					.WithId("7051f130-8a43-444c-9d41-dbe6ebdb0b59"));

			var filteredCspSubscriptions = cspSubscriptions.OnlyWith(
				SubscriptionIdsOf(SubscriptionIdOf("4a8b014f-8f37-47e1-9f72-41727b4973cb")));

			filteredCspSubscriptions.Items
				.Should().HaveCount(1)
				.And.Contain(
					ACspSubscription
						.WithId("4a8b014f-8f37-47e1-9f72-41727b4973cb"));
		}

		[Fact]
		public void OnlyReturnCspSubscriptionsWithAlignedAvailableLicenseNumbersWhichHaveTheirAvailableLicenseNumbersChanged()
		{
			var cspSubscriptions = CspSubscriptionsOf(
				ACspSubscription
					.WithId("4a8b014f-8f37-47e1-9f72-41727b4973cb")
					.WithAvailableLicensesOf(2)
					.WithAssignedLicensesOf(1),
				ACspSubscription
					.WithId("7051f130-8a43-444c-9d41-dbe6ebdb0b59")
					.WithAvailableLicensesOf(1)
					.WithAssignedLicensesOf(1));

			var cspSubsctriptionAvailableLicenseNumberAlignmentResults = cspSubscriptions.AlignNumberOfAvailableAndAssignedLicenses();

			cspSubsctriptionAvailableLicenseNumberAlignmentResults
				.Should().ContainSingle()
					.Which.SubscriptionId.Should().Be(SubscriptionIdOf("4a8b014f-8f37-47e1-9f72-41727b4973cb"));
		}

		private CspSubscriptions CspSubscriptionsOf(params CspSubscription[] cspSubscriptions) =>
			new CspSubscriptions(cspSubscriptions);

		private IEnumerable<SubscriptionCspId> SubscriptionIdsOf(params SubscriptionCspId[] subscriptionIds) => subscriptionIds;

		private SubscriptionCspId SubscriptionIdOf(string subscriptionId) => new SubscriptionCspId(subscriptionId);
	}
}