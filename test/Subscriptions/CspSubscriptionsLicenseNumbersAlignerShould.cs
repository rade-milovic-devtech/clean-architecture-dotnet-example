using System.Collections.Generic;
using FluentAssertions;
using Office365.UserManagement.Core.Customers;
using Office365.UserManagement.Core.Subscriptions;
using Xunit;

using static Office365.UserManagement.Subscriptions.CspSubscriptionBuilder;

namespace Office365.UserManagement.Subscriptions
{
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
				SubscriptionsCspIdsOf(SubscriptionCspIdOf("049c011e-4355-4962-a0fa-163a2d2cba63")));

			results.Should().BeEmpty();
		}

		[Fact]
		public void PerformNecessaryLicenseNumbersAlignmentsWhenUsedWithAutomaticCustomerLicensingMode()
		{
			var cspSubscriptionsLicenseNumbersAligner = new CspSubscriptionsLicenseNumbersAligner(
				CspSubscriptionsOf(
					ACspSubscription
						.WithId("049c011e-4355-4962-a0fa-163a2d2cba63")
						.WithAvailableLicensesOf(2)
						.WithAssignedLicensesOf(1),
					ACspSubscription
						.WithId("331b28e6-d595-4b36-bf30-758a9acbf023")),
				CustomerLicensingMode.Automatic);

			var results = cspSubscriptionsLicenseNumbersAligner.AlignLicenseNumbersForCspSubscriptionsWithIdsOf(
				SubscriptionsCspIdsOf(SubscriptionCspIdOf("049c011e-4355-4962-a0fa-163a2d2cba63")));

			results.Should().ContainSingle()
				.Which.SubscriptionId.Should().Be(SubscriptionCspIdOf("049c011e-4355-4962-a0fa-163a2d2cba63"));
		}

		public CspSubscriptions CspSubscriptionsOf(params CspSubscription[] cspSubscriptions) =>
			new CspSubscriptions(cspSubscriptions);

		public IEnumerable<SubscriptionCspId> SubscriptionsCspIdsOf(params SubscriptionCspId[] subscriptionCspIds) => subscriptionCspIds;

		public SubscriptionCspId SubscriptionCspIdOf(string value) => new SubscriptionCspId(value);
	}
}