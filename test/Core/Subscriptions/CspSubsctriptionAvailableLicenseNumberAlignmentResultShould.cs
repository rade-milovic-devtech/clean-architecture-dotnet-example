using FluentAssertions;
using Xunit;

using static Office365.UserManagement.Core.Subscriptions.SubscriptionCspIdBuilder;
using static Office365.UserManagement.Core.Subscriptions.LicenseQuantityBuilder;

namespace Office365.UserManagement.Core.Subscriptions
{
	[Trait("Category", "Unit")]
	public class CspSubsctriptionAvailableLicenseNumberAlignmentResultShould
	{
		[Fact]
		public void RequireUpdateOfAvailableLicensesForSubscriptionWhenOriginalNumberOfAvailableLicensesDiffersFromTheNewOne()
		{
			var result = new CspSubsctriptionAvailableLicenseNumberAlignmentResult(
				SubscriptionIdOf("4a8b014f-8f37-47e1-9f72-41727b4973cb"),
				LicenseQuantityOf(3),
				LicenseQuantityOf(2));

			result.RequiresUpdateOfAvailableLicensesForSubscription
				.Should().BeTrue();
		}

		[Fact]
		public void NotRequireUpdateOfAvailableLicensesForSubscriptionWhenOriginalAndNewNumberOfAvailableLicensesAreTheSame()
		{
			var result = new CspSubsctriptionAvailableLicenseNumberAlignmentResult(
				SubscriptionIdOf("4a8b014f-8f37-47e1-9f72-41727b4973cb"),
				LicenseQuantityOf(2),
				LicenseQuantityOf(2));

			result.RequiresUpdateOfAvailableLicensesForSubscription
				.Should().BeFalse();
		}
	}
}