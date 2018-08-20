using FluentAssertions;
using Xunit;

using static Office365.UserManagement.Subscriptions.CspSubscriptionsBuilder;
using static Office365.UserManagement.Subscriptions.CspSubscriptionBuilder;
using static Office365.UserManagement.Subscriptions.SubscriptionCspIdBuilder;

namespace Office365.UserManagement.Subscriptions
{
	[Trait("Category", "Unit")]
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
					.WithDifferentNumberOfAvailableAndAssignedLicenses(),
				ACspSubscription
					.WithId("7051f130-8a43-444c-9d41-dbe6ebdb0b59")
					.WithSameNumberOfAvailableAndAssignedLicenses());

			var cspSubsctriptionAvailableLicenseNumberAlignmentResults = cspSubscriptions.AlignNumberOfAvailableAndAssignedLicenses();

			cspSubsctriptionAvailableLicenseNumberAlignmentResults
				.Should().ContainSingle()
					.Which.SubscriptionId.Should().Be(SubscriptionIdOf("4a8b014f-8f37-47e1-9f72-41727b4973cb"));
		}
	}
}