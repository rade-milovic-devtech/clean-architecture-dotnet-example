using Moq;

namespace Office365.UserManagement.Core.Users
{
	public class UserDetailsPresenterSimulator
		: IFormatUserDetailsForPresentation
	{
		private readonly Mock<IFormatUserDetailsForPresentation> userDetailsPresenterMock =
			new Mock<IFormatUserDetailsForPresentation>();

		public void Format(User data)
		{
			userDetailsPresenterMock.Object.Format(data);
		}

		public void HasFormattedUserDataWith(
			string userName, string firstName, string lastName)
		{
			userDetailsPresenterMock.Verify(userDetailsPresenter =>
				userDetailsPresenter.Format(
					It.Is<User>(user =>
						user.UserName == new UserName(userName)
						&& user.FirstName == firstName
						&& user.LastName == lastName)),
				Times.Once);
		}
	}
}