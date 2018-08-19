using FluentAssertions;
using Office365.UserManagement.Core.Subscriptions;
using Xunit;

namespace Office365.UserManagement.Subscriptions
{
	public class CspSubsctriptionAvailableLicenseNumberAlignmentResultShould
	{
		[Fact]
		public void RequireUpdateOfAvailableLicensesForSubscriptionWhenOriginalNumberOfAvailableLicensesDiffersFromTheNewOne()
		{
			var result = new CspSubsctriptionAvailableLicenseNumberAlignmentResult(
				new SubscriptionCspId("4a8b014f-8f37-47e1-9f72-41727b4973cb"),
				new LicenseQuantity(3),
				new LicenseQuantity(2));

			result.RequiresUpdateOfAvailableLicensesForSubscription
				.Should().BeTrue();
		}

		[Fact]
		public void NotRequireUpdateOfAvailableLicensesForSubscriptionWhenOriginalAndNewNumberOfAvailableLicensesAreTheSame()
		{
			var result = new CspSubsctriptionAvailableLicenseNumberAlignmentResult(
				new SubscriptionCspId("4a8b014f-8f37-47e1-9f72-41727b4973cb"),
				new LicenseQuantity(2),
				new LicenseQuantity(2));

			result.RequiresUpdateOfAvailableLicensesForSubscription
				.Should().BeFalse();
		}
	}
}