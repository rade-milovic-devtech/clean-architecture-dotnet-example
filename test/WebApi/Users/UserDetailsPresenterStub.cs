using Microsoft.AspNetCore.Mvc;
using Office365.UserManagement.Core.Users;

namespace Office365.UserManagement.WebApi.Users
{
	public class UserDetailsPresenterStub : UserDetailsPresenter
	{
		public void SetNotFoundResult()
		{
			Result = new NotFoundResult();
		}

		public void SetOkResultResultWith(string email, string fullName)
		{
			Result = new OkObjectResult(
				new UserDetailsResponse
				{
					Email = email,
					FullName = fullName
				});
		}

		public override void Format(User data) {}
	}
}