using FluentAssertions;
using Office365.UserManagement.Core.Subscriptions;
using Xunit;

using static Office365.UserManagement.Subscriptions.CspSubscriptionBuilder;

namespace Office365.UserManagement.Subscriptions
{
	public class CspSubscriptionShould
	{
		[Fact]
		public void SetTheNumberOfAvailableLicensesToTheNumberOfAssignedLicensesWhenTheyDiffer()
		{
			var cspSubscription = ACspSubscription
				.WithId("4a8b014f-8f37-47e1-9f72-41727b4973cb")
				.WithAvailableLicensesOf(2)
				.WithAssignedLicensesOf(1)
				.Build();

			var result = cspSubscription.AlignNumberOfAvailableAndAssignedLicenses();

			result.NewNumberOfAvailableLicenses
				.Should().Be(LicenseQuantityOf(1));
		}

		[Fact]
		public void KeepsTheNumberOfAvailableLicensesWhenItIsTheSameAsTheNumberOfAssignedLicenses()
		{
			var cspSubscription = ACspSubscription
				.WithId("4a8b014f-8f37-47e1-9f72-41727b4973cb")
				.WithAvailableLicensesOf(2)
				.WithAssignedLicensesOf(2)
				.Build();

			var result = cspSubscription.AlignNumberOfAvailableAndAssignedLicenses();

			result.NewNumberOfAvailableLicenses
				.Should().Be(LicenseQuantityOf(2));
		}

		[Fact]
		public void SetTheNumberOfAvailableLicensesToTheMinimumAllowedNumberWhenTheNumberOfAssignedLicensesIsLowerThanMinimumAllowedAvailableLicenses()
		{
			var cspSubscription = ACspSubscription
				.WithId("4a8b014f-8f37-47e1-9f72-41727b4973cb")
				.WithAvailableLicensesOf(4)
				.WithAssignedLicensesOf(0)
				.WithMinAllowedLicensesOf(3)
				.Build();

			var result = cspSubscription.AlignNumberOfAvailableAndAssignedLicenses();

			result.NewNumberOfAvailableLicenses
				.Should().Be(LicenseQuantityOf(3));
		}

		[Fact]
		public void SetTheNumberOfAvailableLicensesToTheMaximumAllowedNumberWhenTheNumberOfAssignedLicensesIsHigherThanMaximumAllowedAvailableLicenses()
		{
			var cspSubscription = ACspSubscription
				.WithId("4a8b014f-8f37-47e1-9f72-41727b4973cb")
				.WithAvailableLicensesOf(2)
				.WithAssignedLicensesOf(5)
				.WithMaxAllowedLicensesOf(4)
				.Build();

			var result = cspSubscription.AlignNumberOfAvailableAndAssignedLicenses();

			result.NewNumberOfAvailableLicenses
				.Should().Be(LicenseQuantityOf(4));
		}

		private LicenseQuantity LicenseQuantityOf(int value) => new LicenseQuantity(value);
	}
}