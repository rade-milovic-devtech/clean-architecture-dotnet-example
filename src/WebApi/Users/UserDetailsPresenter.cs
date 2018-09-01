using Microsoft.AspNetCore.Mvc;
using Office365.UserManagement.Core.Users;

namespace Office365.UserManagement.WebApi.Users
{
	public class UserDetailsPresenter : IFormatUserDetailsForPresentation
	{
		public virtual void Format(User data)
		{
			if (data == null) return;

			Result = new OkObjectResult(AsResponse(data));
		}

		public IActionResult Result { get; protected set; } = new NotFoundResult();

		private UserDetailsResponse AsResponse(User user) =>
			new UserDetailsResponse
			{
				Email = user.UserName,
				FullName = $"{user.FirstName} {user.LastName}"
			};
	}
}