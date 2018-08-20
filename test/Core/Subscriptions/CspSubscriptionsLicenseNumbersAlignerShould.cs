using FluentAssertions;
using Office365.UserManagement.Core.Customers;
using Xunit;

using static Office365.UserManagement.Core.Subscriptions.CspSubscriptionsBuilder;
using static Office365.UserManagement.Core.Subscriptions.CspSubscriptionBuilder;
using static Office365.UserManagement.Core.Subscriptions.SubscriptionCspIdBuilder;

namespace Office365.UserManagement.Core.Subscriptions
{
	[Trait("Category", "Unit")]
	public class CspSubscriptionsLicenseNumbersAlignerShould
	{
		[Fact]
		public void NotPerformAnyLicenseNumbersAlignmentsWhenUsedWithManualCustomerLicensingMode()
		{
			var cspSubscriptionsLicenseNumbersAligner = new CspSubscriptionsLicenseNumbersAligner(
				CspSubscriptionsOf(
					ACspSubscription
						.WithId("049c011e-4355-4962-a0fa-163a2d2cba63")),
				CustomerLicensingMode.Manual);

			var results = cspSubscriptionsLicenseNumbersAligner.AlignLicenseNumbersForCspSubscriptionsWithIdsOf(
				SubscriptionIdsOf(SubscriptionIdOf("049c011e-4355-4962-a0fa-163a2d2cba63")));

			results.Should().BeEmpty();
		}

		[Fact]
		public void PerformNecessaryLicenseNumbersAlignmentsWhenUsedWithAutomaticCustomerLicensingMode()
		{
			var cspSubscriptionsLicenseNumbersAligner = new CspSubscriptionsLicenseNumbersAligner(
				CspSubscriptionsOf(
					ACspSubscription
						.WithId("049c011e-4355-4962-a0fa-163a2d2cba63")
						.WithDifferentNumberOfAvailableAndAssignedLicenses(),
					ACspSubscription
						.WithId("331b28e6-d595-4b36-bf30-758a9acbf023")),
				CustomerLicensingMode.Automatic);

			var results = cspSubscriptionsLicenseNumbersAligner.AlignLicenseNumbersForCspSubscriptionsWithIdsOf(
				SubscriptionIdsOf(SubscriptionIdOf("049c011e-4355-4962-a0fa-163a2d2cba63")));

			results.Should().ContainSingle()
				.Which.SubscriptionId.Should().Be(SubscriptionIdOf("049c011e-4355-4962-a0fa-163a2d2cba63"));
		}
	}
}