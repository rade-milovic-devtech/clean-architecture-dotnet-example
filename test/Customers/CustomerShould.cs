using FluentAssertions;
using Office365.UserManagement.Core.Customers;
using System;
using Xunit;

namespace Office365.UserManagement.Customers
{
	[Trait("Category", "Unit")]
	public class CustomerShould
	{
		[Fact]
		public void FailToCreateWhenCustomerNumberIsNotProvided()
		{
			Action createCustomer = () => new Customer(
				null,
				new CustomerCspId("ed7e5fc4-8df6-4efd-849b-93d5119cf626"),
				CustomerLicensingMode.Automatic);

			createCustomer.Should().Throw<ArgumentNullException>();
		}

		[Fact]
		public void FailToCreateWhenCustomerCspIdIsNotProvided()
		{
			Action createCustomer = () => new Customer(
				new CustomerNumber("1234"),
				null,
				CustomerLicensingMode.Automatic);

			createCustomer.Should().Throw<ArgumentNullException>();
		}
	}
}