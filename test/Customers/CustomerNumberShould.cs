using FluentAssertions;
using Office365.UserManagement.Core.Customers;
using System;
using System.Collections.Generic;
using Xunit;

namespace Office365.UserManagement.Customers
{
	[Trait("Category", "Unit")]
	public class CustomerNumberShould
	{
		[Theory]
		[InlineData(null)]
		[InlineData("")]
		[InlineData("  ")]
		public void FailToCreateWhenValueIsNotProvided(string value)
		{
			Action createCustomerNumber = () => new CustomerNumber(value);

			createCustomerNumber.Should().Throw<ArgumentException>();
		}

		[Theory]
		[MemberData(nameof(EqualityTestData))]
		public void BeEqualToAnotherCustomerNumberWithTheSameValue(CustomerNumber first, CustomerNumber second)
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
					new CustomerNumber("1234"),
					new CustomerNumber("1234")
				}
			};
	}
}