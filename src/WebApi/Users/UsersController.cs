using Microsoft.AspNetCore.Mvc;
using Office365.UserManagement.Core.Users;

namespace Office365.UserManagement.WebApi.Users
{
	[Route("api/customers/{number}/users")]
	[ApiController]
	public class UsersController : ControllerBase
	{
		private readonly IPerformUserOperations userOperations;
		private readonly UserDetailsPresenter userDetailsPresenter;

		public UsersController(
			IPerformUserOperations userOperations,
			UserDetailsPresenter userDetailsPresenter)
		{
			this.userOperations = userOperations;
			this.userDetailsPresenter = userDetailsPresenter;
		}

		// GET api/customers/1234/users/tester@testdomain.onmicrosoft.com
		[HttpGet("{name}")]
		public IActionResult GetOne(
			[FromRoute(Name = "number")] string customerNumber,
			[FromRoute(Name = "name")] string userName)
		{
			var command = new GetUserDetailsCommand
			{
				CustomerNumber = customerNumber,
				UserName = userName
			};
			userOperations.GetUserDetails(command);

			return userDetailsPresenter.Result;
		}

		// DELETE api/customers/1234/users/tester@testdomain.onmicrosoft.com
		[HttpDelete("{name}")]
		public IActionResult Delete(
			[FromRoute(Name = "number")] string customerNumber,
			[FromRoute(Name = "name")] string userName)
		{
			var command = new DeleteUserCommand
			{
				CustomerNumber = customerNumber,
				UserName = userName
			};
			userOperations.DeleteUser(command);

			return NoContent();
		}
	}
}