using Moq;
using Office365.UserManagement.Core.Users;

namespace Office365.UserManagement.WebApi.Users
{
	public class UserOperationsSimulator : IPerformUserOperations
	{
		private readonly Mock<IPerformUserOperations> userOperationsMock = new Mock<IPerformUserOperations>();

		public void GetUserDetails(GetUserDetailsCommand command)
		{
			userOperationsMock.Object.GetUserDetails(command);
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
	}
}