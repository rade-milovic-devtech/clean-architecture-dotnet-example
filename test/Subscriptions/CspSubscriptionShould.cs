using FluentAssertions;
using Office365.UserManagement.Core.Subscriptions;
using System;
using System.Collections.Generic;
using Xunit;

using static Office365.UserManagement.Subscriptions.CspSubscriptionBuilder;
using static Office365.UserManagement.Subscriptions.LicenseQuantityBuilder;

namespace Office365.UserManagement.Subscriptions
{
	[Trait("Category", "Unit")]
	public class CspSubscriptionShould
	{
		[Fact]
		public void FailToCreateWhenIdIsNotProvided()
		{
			Action createCspSubscription = () => new CspSubscription(
				null,
				new LicenseQuantity(2),
				new LicenseQuantity(2),
				new LicenseQuantity(1),
				new LicenseQuantity(10));

			createCspSubscription.Should().Throw<ArgumentNullException>();
		}

		[Fact]
		public void FailToCreateWhenNumberOfAvailableLicensesIsNotProvided()
		{
			Action createCspSubscription = () => new CspSubscription(
				new SubscriptionCspId("4a8b014f-8f37-47e1-9f72-41727b4973cb"),
				null,
				new LicenseQuantity(2),
				new LicenseQuantity(1),
				new LicenseQuantity(10));

			createCspSubscription.Should().Throw<ArgumentNullException>();
		}

		[Fact]
		public void FailToCreateWhenNumberOfAssignedLicensesIsNotProvided()
		{
			Action createCspSubscription = () => new CspSubscription(
				new SubscriptionCspId("4a8b014f-8f37-47e1-9f72-41727b4973cb"),
				new LicenseQuantity(2),
				null,
				new LicenseQuantity(1),
				new LicenseQuantity(10));

			createCspSubscription.Should().Throw<ArgumentNullException>();
		}

		[Fact]
		public void FailToCreateWhenMinimumAllowedNumberOfAvailableLicensesIsNotProvided()
		{
			Action createCspSubscription = () => new CspSubscription(
				new SubscriptionCspId("4a8b014f-8f37-47e1-9f72-41727b4973cb"),
				new LicenseQuantity(2),
				new LicenseQuantity(2),
				null,
				new LicenseQuantity(10));

			createCspSubscription.Should().Throw<ArgumentNullException>();
		}

		[Fact]
		public void FailToCreateWhenMaximumAllowedNumberOfAvailableLicensesIsNotProvided()
		{
			Action createCspSubscription = () => new CspSubscription(
				new SubscriptionCspId("4a8b014f-8f37-47e1-9f72-41727b4973cb"),
				new LicenseQuantity(2),
				new LicenseQuantity(2),
				new LicenseQuantity(1),
				null);

			createCspSubscription.Should().Throw<ArgumentNullException>();
		}

		[Theory]
		[MemberData(nameof(EqualityTestData))]
		public void BeEqualToAnotherCspSubscriptionWithTheSameIdValue(CspSubscription first, CspSubscription second)
		{
			first.Should().Be(second);
			first.GetHashCode().Should().Be(second.GetHashCode());
			(first == second).Should().BeTrue();
		}

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

		public static IEnumerable<object[]> EqualityTestData =>
			new List<object[]>
			{
				new object[]
				{
					new CspSubscription(
						new SubscriptionCspId("4a8b014f-8f37-47e1-9f72-41727b4973cb"),
						new LicenseQuantity(2),
						new LicenseQuantity(2),
						new LicenseQuantity(1),
						new LicenseQuantity(10)),
					new CspSubscription(
						new SubscriptionCspId("4a8b014f-8f37-47e1-9f72-41727b4973cb"),
						new LicenseQuantity(4),
						new LicenseQuantity(5),
						new LicenseQuantity(3),
						new LicenseQuantity(100))
				}
			};
	}
}