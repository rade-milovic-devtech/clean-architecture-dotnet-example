using FluentAssertions;
using Office365.UserManagement.Core.Subscriptions;
using System;
using System.Collections.Generic;
using Xunit;

namespace Office365.UserManagement.Subscriptions
{
	[Trait("Category", "Unit")]
	public class SubscriptionCspIdShould
	{
		[Theory]
		[InlineData(null)]
		[InlineData("")]
		[InlineData("  ")]
		public void FailToCreateWhenValueIsNotProvided(string value)
		{
			Action createSubscriptionCspId = () => new SubscriptionCspId(value);

			createSubscriptionCspId.Should().Throw<ArgumentException>();
		}

		[Theory]
		[MemberData(nameof(EqualityTestData))]
		public void BeEqualToAnotherSubscriptionCspIdWithTheSameValue(SubscriptionCspId first, SubscriptionCspId second)
		{
			first.Should().Be(second);
			first.GetHashCode().Should().Be(second.GetHashCode());
			(first == second).Should().BeTrue();
		}

		public static IEnumerable<object[]> EqualityTestData =>
			new List<object[]>
			{
				new object[]
				{
					new SubscriptionCspId("ed7e5fc4-8df6-4efd-849b-93d5119cf626"),
					new SubscriptionCspId("ed7e5fc4-8df6-4efd-849b-93d5119cf626")
				}
			};
	}
}