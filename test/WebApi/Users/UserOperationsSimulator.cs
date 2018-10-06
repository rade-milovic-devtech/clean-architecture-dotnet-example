using Moq;
using Office365.UserManagement.Core.Users;

namespace Office365.UserManagement.WebApi.Users
{
	public class UserOperationsSimulator : IPerformUserOperations
	{
		private readonly Mock<IPerformUserOperations> userOperationsMock = new Mock<IPerformUserOperations>();

		private string customerNumber = string.Empty;
		private string userName = string.Empty;

		public UserOperationsSimulator ForCustomerWithNumber(string customerNumber)
		{
			this.customerNumber = customerNumber;

			return this;
		}

		public UserOperationsSimulator AndUser(string userName)
		{
			this.userName = userName;

			return this;
		}

		public void PopulatesPresenterDataWithNotFoundResult()
		{
			userOperationsMock.Setup(userOperations =>
				userOperations.GetUserDetails(
					It.Is<GetUserDetailsCommand>(command =>
						command.CustomerNumber == customerNumber
						&& command.UserName == userName),
					It.IsAny<IPresentUserDetails>()))
					.Callback<GetUserDetailsCommand, IPresentUserDetails>((_, presenter) =>
						presenter.Present(null));
		}

		public void PopulatesPresenterDataWithOkResultWith(string email, string firstName, string lastName)
		{
			userOperationsMock.Setup(userOperations =>
				userOperations.GetUserDetails(
					It.Is<GetUserDetailsCommand>(command =>
						command.CustomerNumber == customerNumber
						&& command.UserName == userName),
					It.IsAny<IPresentUserDetails>()))
					.Callback<GetUserDetailsCommand, IPresentUserDetails>((_, presenter) =>
						presenter.Present(new User(new UserName(userName), firstName, lastName)));
		}

		public void GetUserDetails(GetUserDetailsCommand command, IPresentUserDetails presenter)
		{
			userOperationsMock.Object.GetUserDetails(command, presenter);
		}

		public void DeleteUser(DeleteUserCommand command)
		{
			userOperationsMock.Object.DeleteUser(command);
		}

		public void HasDeletedAUserWith(string customerNumber, string userName)
		{
			userOperationsMock.Verify(userOperations =>
				userOperations.DeleteUser(
					It.Is<DeleteUserCommand>(command =>
						command.CustomerNumber == customerNumber
						&& command.UserName == userName)),
				Times.Once);
		}

		public void ClearAllInvocations()
		{
			userOperationsMock.Invocations.Clear();
		}
	}
}