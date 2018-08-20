using FluentAssertions;
using System;
using System.Collections.Generic;
using Xunit;

namespace Office365.UserManagement.Core.Customers
{
	[Trait("Category", "Unit")]
	public class CustomerCspIdShould
	{
		[Theory]
		[InlineData(null)]
		[InlineData("")]
		[InlineData("  ")]
		public void FailToCreateWhenValueIsNotProvided(string value)
		{
			Action createCustomerCspId = () => new CustomerCspId(value);

			createCustomerCspId.Should().Throw<ArgumentException>();
		}

		[Theory]
		[MemberData(nameof(EqualityTestData))]
		public void BeEqualToAnotherCustomerCspIdWithTheSameValue(CustomerCspId first, CustomerCspId second)
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
					new CustomerCspId("ed7e5fc4-8df6-4efd-849b-93d5119cf626"),
					new CustomerCspId("ed7e5fc4-8df6-4efd-849b-93d5119cf626")
				}
			};
	}
}