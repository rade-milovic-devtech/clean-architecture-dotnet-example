using FluentAssertions;
using Office365.UserManagement.Core.Users;
using System;
using System.Collections.Generic;
using Xunit;

namespace Office365.UserManagement.Users
{
	[Trait("Category", "Unit")]
	public class UserNameShould
	{
		[Theory]
		[InlineData(null)]
		[InlineData("")]
		[InlineData("  ")]
		public void FailToCreateWhenValueIsNotProvided(string value)
		{
			Action createUserName = () => new UserName(value);

			createUserName.Should().Throw<ArgumentException>();
		}

		[Theory]
		[MemberData(nameof(EqualityTestData))]
		public void BeEqualToAnotherUserNameWithTheSameValue(UserName first, UserName second)
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
					new UserName("tester@testdomain.com"),
					new UserName("tester@testdomain.com")
				}
			};
	}
}