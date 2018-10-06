using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Xunit;

using static Office365.UserManagement.Core.Users.UserBuilder;

namespace Office365.UserManagement.WebApi.Users
{
	[Trait("Category", "Unit")]
	public class UserDetailsPresenterShould
	{
		[Fact]
		public void HaveNotFoundResultByDefault()
		{
			var presenter = new UserDetailsPresenter();

			presenter.Result.Should()
				.BeOfType<NotFoundResult>();
		}

		[Fact]
		public void HaveNotFoundResultWhenNullValueIsPassedForFormatting()
		{
			var presenter = new UserDetailsPresenter();

			presenter.Present(null);

			presenter.Result.Should()
				.BeOfType<NotFoundResult>();
		}

		[Fact]
		public void HaveOkResultWithCorrectContentWhenUserIsPassedForFormatting()
		{
			var presenter = new UserDetailsPresenter();

			presenter.Present(
				AUser.WithUserName("testuser@testcustomer.onmicrosoft.com")
					.WithFirstName("FirstName")
					.WithLastName("LastName"));

			presenter.Result.Should()
				.BeOfType<OkObjectResult>()
					.Which.Value.Should()
						.BeEquivalentTo(
							new UserDetailsResponse
							{
								Email = "testuser@testcustomer.onmicrosoft.com",
								FullName = "FirstName LastName"
							});
		}
	}
}