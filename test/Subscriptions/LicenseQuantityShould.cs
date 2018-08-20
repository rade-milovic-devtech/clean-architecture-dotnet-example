using FluentAssertions;
using Office365.UserManagement.Core.Subscriptions;
using System;
using System.Collections.Generic;
using Xunit;

using static Office365.UserManagement.Subscriptions.LicenseQuantityBuilder;

namespace Office365.UserManagement.Subscriptions
{
	[Trait("Category", "Unit")]
	public class LicenseQuantityShould
	{
		[Fact]
		public void FailToCreateWhenValueIsLowerThanZero()
		{
			Action createLicenseQuantity = () => new LicenseQuantity(-1);

			createLicenseQuantity.Should().Throw<ArgumentException>();
		}

		[Theory]
		[MemberData(nameof(EqualityTestData))]
		public void BeEqualToAnotherLicenseQuantityWithTheSameValue(LicenseQuantity first, LicenseQuantity second)
		{
			first.Should().Be(second);
			first.GetHashCode().Should().Be(second.GetHashCode());
			(first == second).Should().BeTrue();
		}

		[Fact]
		public void BeLessThanOtherLicenseQuantityWithAHigherValue()
		{
			(LicenseQuantityOf(1) < LicenseQuantityOf(2)).Should().BeTrue();
		}

		[Fact]
		public void BeMoreThanOtherLicenseQuantityWithALowerValue()
		{
			(LicenseQuantityOf(2) > LicenseQuantityOf(1)).Should().BeTrue();
		}

		public static IEnumerable<object[]> EqualityTestData =>
			new List<object[]>
			{
				new object[]
				{
					new LicenseQuantity(1),
					new LicenseQuantity(1)
				}
			};
	}
}