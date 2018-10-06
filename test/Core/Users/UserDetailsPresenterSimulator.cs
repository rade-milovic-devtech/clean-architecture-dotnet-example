using Moq;

namespace Office365.UserManagement.Core.Users
{
	public class UserDetailsPresenterSimulator : IPresentUserDetails
	{
		private readonly Mock<IPresentUserDetails> userDetailsPresenterMock = new Mock<IPresentUserDetails>();

		public void Present(User data)
		{
			userDetailsPresenterMock.Object.Present(data);
		}

		public void HasPresentedUserDataWith(
			string userName, string firstName, string lastName)
		{
			userDetailsPresenterMock.Verify(userDetailsPresenter =>
				userDetailsPresenter.Present(
					It.Is<User>(user =>
						user.UserName == new UserName(userName)
						&& user.FirstName == firstName
						&& user.LastName == lastName)),
				Times.Once);
		}
	}
}